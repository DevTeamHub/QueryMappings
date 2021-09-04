using DevTeam.QueryMappings.Base;
using DevTeam.QueryMappings.Helpers;
using DevTeam.QueryMappings.Mappings;
using DevTeam.QueryMappings.Properties;
using DevTeam.QueryMappings.Services.Interfaces;
using System;
using System.Linq;

namespace DevTeam.QueryMappings.Services.Implementations
{
    /// <summary>
    /// Extensions that allows easier to Apply mappings on <see cref="IQueryable"/> instances.
    /// </summary>
    public class QueryMappingService: IQueryMappingService
    {
        private readonly IMappingsList _mappingsList;

        /// <summary>
        /// Constructor, depends on <see cref="IMappingsList" />
        /// </summary>
        public QueryMappingService(IMappingsList mappingsList)
        {
            _mappingsList = mappingsList;
        }

        /// <summary>
        /// Searches for the mapping in the Storage and applies mapping to the provided <see cref="IQueryable{T}"/>.
        /// </summary>
        /// <typeparam name="TEntity">Source type of mapping.</typeparam>
        /// <typeparam name="TModel">Destination type of mapping.</typeparam>
        /// <param name="query">Instance of <see cref="IQueryable{T}"/> to apply mapping to.</param>
        /// <param name="name">Name of the mapping, if we want to search for mapping registered with some specific name. Should be null if we want to find mapping without name.</param>
        /// <returns>Result of mapping. Instance of <see cref="IQueryable{T}"/> object with applied mapping.</returns>
        /// <exception cref="MappingException">Thrown if we are using incorrect version of AsQuery method or if mapping wasn't found.</exception>
        public virtual IQueryable<TModel> AsQuery<TEntity, TModel>(IQueryable<TEntity> query, string name = null)
        {
            return AsQuery<TEntity, TModel>(query, MappingType.Expression, name);
        }

        /// <summary>
        /// Searches for the mapping in the Storage and applies mapping to the provided <see cref="IQueryable{T}"/>.
        /// </summary>
        /// <typeparam name="TEntity">Source type of mapping.</typeparam>
        /// <typeparam name="TModel">Destination type of mapping.</typeparam>
        /// <param name="query">Instance of <see cref="IQueryable{T}"/> to apply mapping to.</param>
        /// <param name="mappingType">Type of mapping that we will be searching for.</param>
        /// <param name="name">Name of the mapping, if we want to search for mapping registered with some specific name. Should be null if we want to find mapping without name.</param>
        /// <returns>Result of mapping. Instance of <see cref="IQueryable{T}"/> object with applied mapping.</returns>
        /// <exception cref="MappingException">Thrown if we are using incorrect version of AsQuery method or if mapping wasn't found.</exception>
        protected virtual IQueryable<TModel> AsQuery<TEntity, TModel>(IQueryable<TEntity> query, MappingType mappingType, string name = null)
        {
            return ApplyMapping<TEntity, TModel>(
                mapping => AsQuery<TEntity, TModel>(query, mapping), 
                mappingType, 
                name
            );
        }

        /// <summary>
        /// Applies the mapping to the provided <see cref="IQueryable{T}"/>.
        /// </summary>
        /// <typeparam name="TEntity">Source type of mapping.</typeparam>
        /// <typeparam name="TModel">Destination type of mapping.</typeparam>
        /// <param name="query">Instance of <see cref="IQueryable{T}"/> to apply mapping to.</param>
        /// <param name="mapping"><see cref="ExpressionMapping{TFrom, TTo}"/> mapping.</param>
        /// <returns>Result of mapping. Instance of <see cref="IQueryable{T}"/> object with applied mapping.</returns>
        /// <exception cref="MappingException">Thrown if we are using incorrect version of AsQuery method or if mapping wasn't found.</exception>
        protected virtual IQueryable<TModel> AsQuery<TEntity, TModel>(IQueryable<TEntity> query, Mapping mapping)
        {
            var expressionMapping = (ExpressionMapping<TEntity, TModel>)mapping;
            return expressionMapping.Apply(query);
        }

        /// <summary>
        /// Searches for the mapping in the Storage and applies mapping to the provided <see cref="IQueryable{T}"/>.
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
        public virtual IQueryable<TModel> AsQuery<TEntity, TModel, TArgs>(IQueryable<TEntity> query, TArgs args, string name = null)
            where TArgs : class
        {
            return AsQuery<TEntity, TModel, TArgs>(query, MappingType.Parameterized, args, name);
        }

        /// <summary>
        /// Searches for the mapping in the Storage and applies mapping to the provided <see cref="IQueryable{T}"/>.
        /// Passes parameters into mapping that can be used inside of mapping expression.
        /// </summary>
        /// <typeparam name="TEntity">Source type of mapping.</typeparam>
        /// <typeparam name="TModel">Destination type of mapping.</typeparam>
        /// <typeparam name="TArgs">Type of arguments that we pass into mapping expression.</typeparam>
        /// <param name="query">Instance of <see cref="IQueryable{T}"/> to apply mapping to.</param>
        /// <param name="mappingType">Type of mapping that we will be searching for.</param>
        /// <param name="args">Arguments that we want to pass into mapping to use them inside of mapping expression.</param>
        /// <param name="name">Name of the mapping, if we want to search for mapping registered with some specific name. Should be null if we want to find mapping without name.</param>
        /// <returns>Result of mapping. Instance of <see cref="IQueryable{T}"/> object with applied mapping.</returns>
        /// <exception cref="MappingException">Thrown if args are null or if we are using incorrect version of AsQuery method or if mapping wasn't found.</exception>
        protected IQueryable<TModel> AsQuery<TEntity, TModel, TArgs>(IQueryable<TEntity> query, MappingType mappingType, TArgs args, string name = null)
            where TArgs : class
        {
            if (args == null)
                throw new MappingException(Resources.ArgumentsAreRequiredException);

            return ApplyMapping<TEntity, TModel>(
                mapping => AsQuery<TEntity, TModel, TArgs>(query, args, mapping), 
                mappingType, 
                name
            );
        }

        /// <summary>
        /// Applies the mapping to the provided <see cref="IQueryable{T}"/>.
        /// Passes parameters into mapping that can be used inside of mapping expression.
        /// </summary>
        /// <typeparam name="TEntity">Source type of mapping.</typeparam>
        /// <typeparam name="TModel">Destination type of mapping.</typeparam>
        /// <typeparam name="TArgs">Arguments type used in the mapping.</typeparam>
        /// <param name="query">Instance of <see cref="IQueryable{T}"/> to apply mapping to.</param>
        /// <param name="args">Arguments that we want to pass into mapping to use them inside of mapping expression.</param>
        /// <param name="mapping"><see cref="ExpressionMapping{TFrom, TTo}"/> mapping.</param>
        /// <returns>Result of mapping. Instance of <see cref="IQueryable{T}"/> object with applied mapping.</returns>
        /// <exception cref="MappingException">Thrown if we are using incorrect version of AsQuery method or if mapping wasn't found.</exception>
        protected virtual IQueryable<TModel> AsQuery<TEntity, TModel, TArgs>(IQueryable<TEntity> query, TArgs args, Mapping mapping)
            where TArgs : class
        {
            mapping.ValidateArguments<TArgs>();
            var parameterizedMapping = (ParameterizedMapping<TEntity, TModel, TArgs>)mapping;
            return parameterizedMapping.Apply(query, args);
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
        protected internal IQueryable<TModel> ApplyMapping<TEntity, TModel>(Func<Mapping, IQueryable<TModel>> applyFunction, MappingType mappingType, string name = null)
        {
            var mapping = _mappingsList.Get<TEntity, TModel>(name);

            try
            {
                return applyFunction(mapping);
            }
            catch (MappingException)
            {
                throw;
            }
            catch (InvalidCastException castException)
            {
                var exception = MappingException.HandleMappingError<TEntity, TModel>(mappingType, mapping.MappingType, castException);
                throw exception;
            }
            catch (Exception exception)
            {
                throw new MappingException(Resources.ApplyMappingException, exception);
            }
        }
    }

    /// <summary>
    /// Extension of the <see cref="QueryMappingService" /> that allows to provide Database Context instances into the mappings. 
    /// </summary>
    /// <typeparam name="TContext">Type of Database Context that we want to inject inside of the mapping.</typeparam>
    public class QueryMappingService<TContext>: QueryMappingService, IQueryMappingService<TContext>
    {
        private readonly TContext _context;

        /// <summary>
        /// Constructor, depends on <see cref="IMappingsList" />
        /// </summary>
        public QueryMappingService(TContext context, IMappingsList mappingsList)
            : base(mappingsList)
        {
            _context = context;
        }

        /// <summary>
        /// Searches for the mapping in the Storage and applies mapping to the provided <see cref="IQueryable{T}"/>.
        /// Injects Database Context of TContext type into mapping if it depends on the Context.
        /// </summary>
        /// <typeparam name="TEntity">Source type of mapping.</typeparam>
        /// <typeparam name="TModel">Destination type of mapping.</typeparam>
        /// <param name="query">Instance of <see cref="IQueryable{T}"/> to apply mapping to.</param>
        /// <param name="name">Name of the mapping, if we want to search for mapping registered with some specific name. Should be null if we want to find mapping without name.</param>
        /// <returns>Result of mapping. Instance of <see cref="IQueryable{T}"/> object with applied mapping.</returns>
        /// <exception cref="MappingException">Thrown if we are using incorrect version of AsQuery method or if mapping wasn't found.</exception>
        public override IQueryable<TModel> AsQuery<TEntity, TModel>(IQueryable<TEntity> query, string name = null)
        {
            return AsQuery<TEntity, TModel>(query, MappingType.Query, name);
        }

        /// <summary>
        /// Applies the mapping to the provided <see cref="IQueryable{T}"/>. If the mapping requires a Context as a dependency, it will also provides 
        /// the context as last argument. 
        /// </summary>
        /// <typeparam name="TEntity">Source type of mapping.</typeparam>
        /// <typeparam name="TModel">Destination type of mapping.</typeparam>
        /// <param name="query">Instance of <see cref="IQueryable{T}"/> to apply mapping to.</param>
        /// <param name="mapping"><see cref="ExpressionMapping{TFrom, TTo}"/> mapping.</param>
        /// <returns>Result of mapping. Instance of <see cref="IQueryable{T}"/> object with applied mapping.</returns>
        /// <exception cref="MappingException">Thrown if we are using incorrect version of AsQuery method or if mapping wasn't found.</exception>
        protected override IQueryable<TModel> AsQuery<TEntity, TModel>(IQueryable<TEntity> query, Mapping mapping)
        {
            if (mapping.MappingType.HasFlag(MappingType.Query))
            {
                mapping.ValidateContext<TContext>();
                var queryMapping = (QueryMapping<TEntity, TModel, TContext>)mapping;
                return queryMapping.Apply(query, _context);
            }

            return base.AsQuery<TEntity, TModel>(query, mapping);
        }

        /// <summary>
        /// Searches for the mapping in the Storage and applies mapping to the provided <see cref="IQueryable{T}"/>.
        /// Passes parameters into mapping that can be used inside of mapping expression.
        /// Injects Database Context of TContext type into mapping if it depends on the Context.
        /// </summary>
        /// <typeparam name="TEntity">Source type of mapping.</typeparam>
        /// <typeparam name="TModel">Destination type of mapping.</typeparam>
        /// <typeparam name="TArgs">Type of arguments that we pass into mapping expression.</typeparam>
        /// <param name="query">Instance of <see cref="IQueryable{T}"/> to apply mapping to.</param>
        /// <param name="args">Arguments that we want to pass into mapping to use them inside of mapping expression.</param>
        /// <param name="name">Name of the mapping, if we want to search for mapping registered with some specific name. Should be null if we want to find mapping without name.</param>
        /// <returns>Result of mapping. Instance of <see cref="IQueryable{T}"/> object with applied mapping.</returns>
        /// <exception cref="MappingException">Thrown if args are null or if we are using incorrect version of AsQuery method or if mapping wasn't found.</exception>
        public override IQueryable<TModel> AsQuery<TEntity, TModel, TArgs>(IQueryable<TEntity> query, TArgs args, string name = null)
            where TArgs : class
        {
            return AsQuery<TEntity, TModel, TArgs>(query, MappingType.ParemeterizedQuery, args, name);
        }

        /// <summary>
        /// Applies the mapping to the provided <see cref="IQueryable{T}"/>. If the mapping requires a Context as a dependency, it will also provides 
        /// the context as last argument. 
        /// Passes parameters into mapping that can be used inside of mapping expression.
        /// </summary>
        /// <typeparam name="TEntity">Source type of mapping.</typeparam>
        /// <typeparam name="TModel">Destination type of mapping.</typeparam>
        /// <typeparam name="TArgs">Arguments type used in the mapping.</typeparam>
        /// <param name="query">Instance of <see cref="IQueryable{T}"/> to apply mapping to.</param>
        /// <param name="args">Arguments that we want to pass into mapping to use them inside of mapping expression.</param>
        /// <param name="mapping"><see cref="ExpressionMapping{TFrom, TTo}"/> mapping.</param>
        /// <returns>Result of mapping. Instance of <see cref="IQueryable{T}"/> object with applied mapping.</returns>
        /// <exception cref="MappingException">Thrown if we are using incorrect version of AsQuery method or if mapping wasn't found.</exception>
        protected override IQueryable<TModel> AsQuery<TEntity, TModel, TArgs>(IQueryable<TEntity> query, TArgs args, Mapping mapping)
            where TArgs : class
        {
            if (mapping.MappingType == MappingType.ParemeterizedQuery)
            {
                mapping.ValidateArguments<TArgs>();
                mapping.ValidateContext<TContext>();
                var parameterizedMapping = (ParameterizedQueryMapping<TEntity, TModel, TArgs, TContext>)mapping;
                return parameterizedMapping.Apply(query, args, _context);
            }

            return base.AsQuery<TEntity, TModel, TArgs>(query, args, mapping);
        }
    }
}
