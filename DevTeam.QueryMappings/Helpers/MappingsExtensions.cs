using DevTeam.QueryMappings.Base;
using DevTeam.QueryMappings.Mappings;
using DevTeam.QueryMappings.Properties;
using System;
using System.Linq;

namespace DevTeam.QueryMappings.Helpers
{
    public static class QueryMappingsExtensions
    {
        public static IQueryable<TModel> AsQuery<TEntity, TModel>(this IQueryable<TEntity> query, string name = null)
        {
            return ApplyMapping<TEntity, TModel>(mapping => 
            {
                var expressionMapping = (ExpressionMapping<TEntity, TModel>)mapping;
                return expressionMapping.Apply(query);
            });
        }

        public static IQueryable<TModel> AsQuery<TEntity, TModel, TArgs>(this IQueryable<TEntity> query, TArgs args, string name = null)
            where TArgs : class
        {
            if (args == null)
                throw new MappingException(Resources.ArgumentsAreRequiredException);

            return ApplyMapping<TEntity, TModel>(mapping =>
            {
                var parameterizedMapping = (ParameterizedMapping<TEntity, TModel, TArgs>)mapping;
                return parameterizedMapping.Apply(query, args);
            });
        }

        public static IQueryable<TModel> AsQuery<TEntity, TModel, TContext>(this IQueryable<TEntity> query, object contextKey, string name = null)
        {
            return ApplyMapping<TEntity, TModel>(mapping =>
            {
                var context = ContextResolver<TContext>.Resolve(contextKey);
                var queryMapping = (QueryMapping<TEntity, TModel, TContext>)mapping;
                return queryMapping.Apply(query, context);
            });
        }

        public static IQueryable<TModel> AsQuery<TEntity, TModel, TArgs, TContext>(this IQueryable<TEntity> query, TArgs args, object contextKey, string name = null)
        {
            if (args == null)
                throw new MappingException(Resources.ArgumentsAreRequiredException);

            return ApplyMapping<TEntity, TModel>(mapping =>
            {
                var context = ContextResolver<TContext>.Resolve(contextKey);
                var queryParameterizedMapping = (ParameterizedQueryMapping<TEntity, TModel, TArgs, TContext>)mapping;
                return queryParameterizedMapping.Apply(query, args, context);
            });
        }

        private static IQueryable<TModel> ApplyMapping<TEntity, TModel>(Func<Mapping, IQueryable<TModel>> applyFunction, string name = null)
        {
            var mapping = MappingsList.Get<TEntity, TModel>(name);

            try
            {
                return applyFunction(mapping);
            }
            catch (InvalidCastException castException)
            {
                throw HandleIncorrectMappingType<TEntity, TModel>(MappingType.Expression, mapping.MappingType, castException);
            }
            catch (Exception exception)
            {
                throw new MappingException(Resources.ApplyMappingException, exception);
            }
        }

        private static MappingException HandleIncorrectMappingType<TFrom, TTo>(MappingType requestedType, MappingType actualType, Exception innerException)
        {
            const string with = "with";
            const string without = "without";

            var isArgsNeeded = actualType.HasFlag(MappingType.Parameterized) ? with : without;
            var isContextNeeded = actualType.HasFlag(MappingType.Query) ? with : without;
            var isArgsRequested = requestedType.HasFlag(MappingType.Parameterized) ? with : without;
            var isContextRequested = requestedType.HasFlag(MappingType.Query) ? with : without;

            var exceptionMessage = $"Mapping from type {typeof(TFrom).Name} to type {typeof(TTo).Name} was found, but you are using incorrect version of AsQuery method for this type of mapping. Founded mapping was registered {} arguments and {} EF context, but this AsQuery method version should be used {} arguments and {} EF context";

            return new MappingException(exceptionMessage, innerException);
        }
    }
}