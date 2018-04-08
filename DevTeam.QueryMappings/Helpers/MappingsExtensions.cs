using DevTeam.QueryMappings.Base;
using DevTeam.QueryMappings.Mappings;
using System;
using System.Linq;

namespace DevTeam.QueryMappings.Helpers
{
    public static class QueryMappingsExtensions
    {
        public static IQueryable<TModel> AsQuery<TEntity, TModel>(this IQueryable<TEntity> query, string name = null)
        {
            var mapping = MappingsList.Get<TEntity, TModel>(name);

            try
            {
                var expressionMapping = (ExpressionMapping<TEntity, TModel>) mapping;
                return expressionMapping.Apply(query);
            }
            catch (InvalidCastException castException)
            {
                if (mapping is )

                throw new MappingException("asdada", castException);
            }
            catch (Exception exception)
            {
                throw new MappingException("asdasd", exception);
            }
        }

        public static IQueryable<TModel> AsQuery<TEntity, TModel, TArgs>(this IQueryable<TEntity> query, TArgs args, string name = null)
            where TArgs : class
        {
            var mapping = (ParameterizedMapping<TEntity, TModel, TArgs>) MappingsList.Get<TEntity, TModel>(name);
            return mapping.Apply(query, args);
        }

        public static IQueryable<TModel> AsQuery<TEntity, TModel, TContext>(this IQueryable<TEntity> query, object contextKey, string name = null)
        {
            var context = ContextResolver<TContext>.Resolve(contextKey);
            var mapping = (QueryMapping<TEntity, TModel, TContext>) MappingsList.Get<TEntity, TModel>(name);
            return mapping.Apply(query, context);
        }

        public static IQueryable<TModel> AsQuery<TEntity, TModel, TArgs, TContext>(this IQueryable<TEntity> query, TArgs args, Type contextType, string name = null)
        {
            var context = ContextResolver<TContext>.Resolve(contextType);
            var mapping = (ParameterizedQueryMapping<TEntity, TModel, TArgs, TContext>) MappingsList.Get<TEntity, TModel>(name);
            return mapping.Apply(query, args, context);
        }
    }
}