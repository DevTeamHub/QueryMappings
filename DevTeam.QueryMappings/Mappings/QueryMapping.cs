using DevTeam.QueryMappings.Base;
using System;
using System.Linq;

namespace DevTeam.QueryMappings.Mappings
{
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

        public IQueryable<TTo> Apply(IQueryable<TFrom> query, TContext context)
        {
            return _mapping.Invoke(query, context);
        }
    }
}
