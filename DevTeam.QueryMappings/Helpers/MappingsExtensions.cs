using DevTeam.QueryMappings.Base;
using DevTeam.QueryMappings.Mappings;
using DevTeam.QueryMappings.Properties;
using System;
using System.Linq;

namespace DevTeam.QueryMappings.Helpers
{
    /// <summary>
    /// Extensions that allows easier to Apply mappings on <see cref="IQueryable"/> instances.
    /// </summary>
    public static class MappingsExtensions
    {
        /// <summary>
        /// Searches for <see cref="ExpressionMapping{TFrom, TTo}"/> in the Storage and applies mapping on <see cref="IQueryable{T}"/>.
        /// </summary>
        /// <typeparam name="TEntity">Source type of mapping.</typeparam>
        /// <typeparam name="TModel">Destination type of mapping.</typeparam>
        /// <param name="query">Instance of <see cref="IQueryable{T}"/> to apply mapping to.</param>
        /// <param name="name">Name of the mapping, if we want to search for mapping registered with some specific name. Should be null if we want to find mapping without name.</param>
        /// <returns>Result of mapping. Instance of <see cref="IQueryable{T}"/> object with applied mapping.</returns>
        /// <exception cref="MappingException">Thrown if we are using incorrect version of AsQuery method or if mapping wasn't found.</exception>
        public static IQueryable<TModel> AsQuery<TEntity, TModel>(this IQueryable<TEntity> query, string name = null)
        {
            return ApplyMapping<TEntity, TModel>(mapping => 
            {
                var expressionMapping = (ExpressionMapping<TEntity, TModel>)mapping;
                return expressionMapping.Apply(query);
            }, MappingType.Expression, name);
        }

        /// <summary>
        /// Searches for <see cref="ParameterizedMapping{TFrom, TTo, TArgs}"/> in the Storage and applies mapping on <see cref="IQueryable{T}"/>.
        /// Passes parameters into mapping that can be used inside of mapping expression.
        /// </summary>
        /// <typeparam name="TEntity">Source type of mapping.</typeparam>
        /// <typeparam name="TModel">Destination type of mapping.</typeparam>
        /// <typeparam name="TArgs">Type of arguments that we pass into mapping expression.</typeparam>
        /// <param name="query">Instance of <see cref="IQueryable{T}"/> to apply mapping to.</param>
        /// <param name="args">Arguments that we want to pass into mapping to use them inside of mapping expression.</param>
        /// <param name="name">Name of the mapping, if we want to search for mapping registered with some specific name. Should be null if we want to find mapping without name.</param>
        /// <returns>Result of mapping. Instance of <see cref="IQueryable{T}"/> object with applied mapping.</returns>
        /// <exception cref="MappingException">Thrown if args are null or if we are using incorrect version of AsQuery method or if mapping wasn't found.</exception>
        public static IQueryable<TModel> AsQuery<TEntity, TModel, TArgs>(this IQueryable<TEntity> query, TArgs args, string name = null)
            where TArgs : class
        {
            if (args == null)
                throw new MappingException(Resources.ArgumentsAreRequiredException);

            return ApplyMapping<TEntity, TModel>(mapping =>
            {
                var parameterizedMapping = (ParameterizedMapping<TEntity, TModel, TArgs>)mapping;
                return parameterizedMapping.Apply(query, args);
            }, MappingType.Parameterized, name);
        }

        /// <summary>
        /// Searches for <see cref="QueryMapping{TFrom, TTo, TContext}"/> in the Storage and applies mapping on <see cref="IQueryable{T}"/>.
        /// Injects EF Context inside of mapping, so Context can be used inside of mapping expression.
        /// </summary>
        /// <typeparam name="TEntity">Source type of mapping.</typeparam>
        /// <typeparam name="TModel">Destination type of mapping.</typeparam>
        /// <typeparam name="TContext">Type of EF Context that we want to inject inside of mapping.</typeparam>
        /// <param name="query">Instance of <see cref="IQueryable{T}"/> to apply mapping to.</param>
        /// <param name="contextKey">Obligatory parameter. Key that will be used to resolve context. Helps to choose correct Context instance when we have more than one EF Context in application.</param>
        /// <param name="name">Name of the mapping, if we want to search for mapping registered with some specific name. Should be null if we want to find mapping without name.</param>
        /// <returns>Result of mapping. Instance of <see cref="IQueryable{T}"/> object with applied mapping.</returns>
        /// <exception cref="MappingException">Thrown if context key is null or if we are using incorrect version of AsQuery method or if mapping wasn't found.</exception>
        public static IQueryable<TModel> AsQuery<TEntity, TModel, TContext>(this IQueryable<TEntity> query, object contextKey, string name = null)
        {
            if (contextKey == null)
                throw new MappingException(Resources.ContextKeyIsRequredException);

            return ApplyMapping<TEntity, TModel>(mapping =>
            {
                var context = ContextResolver<TContext>.Resolve(contextKey);
                var queryMapping = (QueryMapping<TEntity, TModel, TContext>)mapping;
                return queryMapping.Apply(query, context);
            }, MappingType.Query, name);
        }

        /// <summary>
        /// Searches for <see cref="ParameterizedQueryMapping{TFrom, TTo, TArgs, TContext}"/> in the Storage and applies mapping on <see cref="IQueryable{T}"/>.
        /// Passes parameters into mapping that can be used inside of mapping expression.
        /// Injects EF Context inside of mapping, so Context can be used inside of mapping expression.
        /// </summary>
        /// <typeparam name="TEntity">Source type of mapping.</typeparam>
        /// <typeparam name="TModel">Destination type of mapping.</typeparam>
        /// <typeparam name="TArgs">Type of arguments that we pass into mapping expression.</typeparam>
        /// <typeparam name="TContext">Type of EF Context that we want to inject inside of mapping.</typeparam>
        /// <param name="query">Instance of <see cref="IQueryable{T}"/> to apply mapping to.</param>
        /// <param name="args">Arguments that we want to pass into mapping to use them inside of mapping expression.</param>
        /// <param name="contextKey">Obligatory parameter. Key that will be used to resolve context. Helps to choose correct Context instance when we have more than one EF Context in application.</param>
        /// <param name="name">Name of the mapping, if we want to search for mapping registered with some specific name. Should be null if we want to find mapping without name.</param>
        /// <returns>Result of mapping. Instance of <see cref="IQueryable{T}"/> object with applied mapping.</returns>
        /// <exception cref="MappingException">Thrown if context key is null or args are null or if we are using incorrect version of AsQuery method or if mapping wasn't found.</exception>
        public static IQueryable<TModel> AsQuery<TEntity, TModel, TArgs, TContext>(this IQueryable<TEntity> query, TArgs args, object contextKey, string name = null)
        {
            if (args == null)
                throw new MappingException(Resources.ArgumentsAreRequiredException);

            if (contextKey == null)
                throw new MappingException(Resources.ContextKeyIsRequredException);

            return ApplyMapping<TEntity, TModel>(mapping =>
            {
                var context = ContextResolver<TContext>.Resolve(contextKey);
                var queryParameterizedMapping = (ParameterizedQueryMapping<TEntity, TModel, TArgs, TContext>)mapping;
                return queryParameterizedMapping.Apply(query, args, context);
            }, MappingType.ParemeterizedQuery, name);
        }

        /// <summary>
        /// Wrapper that helps handle exceptions for every AsQuery overload.
        /// </summary>
        /// <typeparam name="TEntity">Source type of mapping.</typeparam>
        /// <typeparam name="TModel">Destination type of mapping.</typeparam>
        /// <param name="applyFunction">AsQuery function with main logic.</param>
        /// <param name="mappingType">Type of mapping to search for.</param>
        /// <param name="name">Name of the mapping, if we want to search for mapping registered with some specific name. Should be null if we want to find mapping without name.</param>
        /// <returns>Result of mapping. Instance of <see cref="IQueryable{T}"/> object with applied mapping.</returns>
        /// <exception cref="MappingException">Thrown if we are using incorrect version of AsQuery method or if mapping wasn't found.</exception>
        private static IQueryable<TModel> ApplyMapping<TEntity, TModel>(Func<Mapping, IQueryable<TModel>> applyFunction, MappingType mappingType, string name = null)
        {
            var mapping = MappingsList.Get<TEntity, TModel>(name);

            try
            {
                return applyFunction(mapping);
            }
            catch (InvalidCastException castException)
            {
                throw HandleIncorrectMappingType<TEntity, TModel>(mappingType, mapping.MappingType, castException);
            }
            catch (Exception exception)
            {
                throw new MappingException(Resources.ApplyMappingException, exception);
            }
        }

        /// <summary>
        /// Helps to build descriptive message if we are using incorrect version of AsQuery method.
        /// </summary>
        /// <typeparam name="TFrom">Source type of mapping.</typeparam>
        /// <typeparam name="TTo">Destination type of mapping.</typeparam>
        /// <param name="requestedType">Type of mapping that was found in storage instead of expected type.</param>
        /// <param name="actualType">Expected type of mapping that we tried to find.</param>
        /// <param name="innerException">Exception that has happened during incorrect conversion of mapping.</param>
        /// <returns>Descriptive exception with detailed information about exception.</returns>
        private static MappingException HandleIncorrectMappingType<TFrom, TTo>(MappingType requestedType, MappingType actualType, Exception innerException)
        {
            const string with = "with";
            const string without = "without";

            var isArgsNeeded = actualType.HasFlag(MappingType.Parameterized) ? with : without;
            var isContextNeeded = actualType.HasFlag(MappingType.Query) ? with : without;
            var isArgsRequested = requestedType.HasFlag(MappingType.Parameterized) ? with : without;
            var isContextRequested = requestedType.HasFlag(MappingType.Query) ? with : without;

            var exceptionMessage = string.Format(Resources.InvalidCastMappingException, typeof(TFrom).Name, typeof(TTo).Name, isArgsNeeded, isContextNeeded, isArgsRequested, isContextRequested);

            return new MappingException(exceptionMessage, innerException);
        }
    }
}