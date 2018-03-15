using DevTeam.QueryMappings.Base;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace DevTeam.QueryMappings.Mappings
{
    public class QueryMapping<TFrom, TTo, TContext> : Mapping, IMapping<TFrom, TTo>
    {
        private readonly Type _contextType;

        public QueryMapping(Expression<Func<IQueryable<TFrom>, TContext, IQueryable<TTo>>> mapping, string name = null)
            : base(typeof(TFrom), typeof(TTo), name)
        {
            _mapping = mapping.Compile();
            _contextType = typeof(TContext);
        }

        private readonly Func<IQueryable<TFrom>, TContext, IQueryable<TTo>> _mapping;

        public IQueryable<TTo> Apply(IQueryable<TFrom> query)
        {
            return _mapping.Invoke(query, context);
        }
    }
}
