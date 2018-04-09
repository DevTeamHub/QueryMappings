using DevTeam.QueryMappings.Base;
using System;
using System.Linq;

namespace DevTeam.QueryMappings.Mappings
{
    public class ParameterizedQueryMapping<TFrom, TTo, TArgs, TContext>: Mapping
    {
        private readonly Type _contextType;

        public ParameterizedQueryMapping(Func<TArgs, Func<IQueryable<TFrom>, TContext, IQueryable<TTo>>> mapping, string name = null)
            : base(typeof(TFrom), typeof(TTo), MappingType.ParemeterizedQuery, name)
        {
            _mapping = mapping;
            _contextType = typeof(TContext);
        }

        private readonly Func<TArgs, Func<IQueryable<TFrom>, TContext, IQueryable<TTo>>> _mapping;

        public IQueryable<TTo> Apply(IQueryable<TFrom> query, TArgs args, TContext context)
        {
            var expression = _mapping.Invoke(args);
            return expression.Invoke(query, context);
        }
    }
}
