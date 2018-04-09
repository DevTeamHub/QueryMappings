using DevTeam.QueryMappings.Base;
using System;
using System.Linq;

namespace DevTeam.QueryMappings.Mappings
{
    /// <summary>
    /// Describes mapping from one type to another with expression and Entity Framework Context that is injected inside of expression. 
    /// </summary>
    /// <typeparam name="TFrom">Source type of mapping.</typeparam>
    /// <typeparam name="TTo">Destination type of mapping.</typeparam>
    /// <typeparam name="TContext">Entity Framework Context type.</typeparam>
    public class QueryMapping<TFrom, TTo, TContext> : Mapping
    {
        private readonly Type _contextType;

        public QueryMapping(Func<IQueryable<TFrom>, TContext, IQueryable<TTo>> mapping, string name = null)
            : base(typeof(TFrom), typeof(TTo), MappingType.Query, name)
        {
            _mapping = mapping;
            _contextType = typeof(TContext);
        }

        private readonly Func<IQueryable<TFrom>, TContext, IQueryable<TTo>> _mapping;

        /// <summary>
        /// Applies expression on <see cref="IQueryable{T}"/> instance.
        /// EF Context will be injected inside of the expression.
        /// </summary>
        /// <param name="query"><see cref="IQueryable{T}"/> instance.</param>
        /// <param name="context">Injected EF Context.</param>
        /// <returns>New <see cref="IQueryable{T}"/> instance with applied expression.</returns>
        public IQueryable<TTo> Apply(IQueryable<TFrom> query, TContext context)
        {
            return _mapping.Invoke(query, context);
        }
    }
}
