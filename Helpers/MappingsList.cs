using DevTeam.QueryMappings.Base;
using DevTeam.QueryMappings.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DevTeam.QueryMappings.Helpers
{
    public static class MappingsList
    {
        private static readonly List<Mapping> Mappings;

        static MappingsList()
        {
            Mappings = new List<Mapping>();
        }

        public static IMapping<TFrom, TTo> Get<TFrom, TTo>()
        {
            return (IMapping<TFrom, TTo>) Mappings.FirstOrDefault(m => m.Is<TFrom, TTo>());
        }

        public static void Add(Mapping mapping)
        {
            Mappings.Add(mapping);
        }

        public static void Add<TFrom, TTo>(Expression<Func<TFrom, TTo>> expression, string name = null)
        {
            var mapping = new ExpressionMapping<TFrom, TTo>(expression, name);
            Add(mapping);
        }

        public static void Add<TFrom, TTo>(string name, Expression<Func<TFrom, TTo>> expression)
        {
            Add(expression, name);
        }

        public static void Add<TFrom, TTo, TContext>(Expression<Func<IQueryable<TFrom>, TContext, IQueryable<TTo>>> expression, string name = null)
        {
            var mapping = new QueryMapping<TFrom, TTo, TContext>(expression, name);
            Add(mapping);
        }

        public static void Add<TFrom, TTo, TContext>(string name, Expression<Func<IQueryable<TFrom>, TContext, IQueryable<TTo>>> expression)
        {
            Add(expression, name);
        }
    }
}
