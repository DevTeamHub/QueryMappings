using DevTeam.QueryMappings.Base;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace DevTeam.QueryMappings.Mappings
{
    public class ParameterizedMapping<TFrom, TTo, TArgs> : Mapping
        where TArgs: class
    {
        public ParameterizedMapping(Func<TArgs, Expression<Func<TFrom, TTo>>> mapping, string name = null)
            : base(typeof(TFrom), typeof(TTo), MappingType.Parameterized, name)
        {
            _mapping = mapping;
        }

        private readonly Func<TArgs, Expression<Func<TFrom, TTo>>> _mapping;

        public IQueryable<TTo> Apply(IQueryable<TFrom> query, TArgs args)
        {
            var expression = _mapping.Invoke(args);
            return query.Select(expression);
        }
    }
}
