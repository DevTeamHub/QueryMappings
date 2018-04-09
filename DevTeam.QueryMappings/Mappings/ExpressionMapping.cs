using DevTeam.QueryMappings.Base;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace DevTeam.QueryMappings.Mappings
{
    public class ExpressionMapping<TFrom, TTo> : Mapping
    {
        public ExpressionMapping(Expression<Func<TFrom, TTo>> mapping, string name = null)
            : base(typeof(TFrom), typeof(TTo), MappingType.Expression, name)
        {
            _mapping = mapping;
        }

        private readonly Expression<Func<TFrom, TTo>> _mapping;

        public IQueryable<TTo> Apply(IQueryable<TFrom> query)
        {
            return query.Select(_mapping);
        }
    }
}
