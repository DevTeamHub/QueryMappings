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

        public static Mapping Get<TFrom, TTo>(string name = null)
        {
            return Mappings.FirstOrDefault(m => m.Is<TFrom, TTo>(name));
        }

        private static void Add(Mapping mapping)
        {
            Mappings.Add(mapping);
        }

        public static void Add<TFrom, TTo>(Expression<Func<TFrom, TTo>> expression)
        {
            Add(null, expression);
        }

        public static void Add<TFrom, TTo>(string name, Expression<Func<TFrom, TTo>> expression)
        {
            var mapping = new ExpressionMapping<TFrom, TTo>(expression, name);
            Add(mapping);
        }

        public static void Add<TFrom, TTo, TArgs>(Func<TArgs, Expression<Func<TFrom, TTo>>> expression)
            where TArgs : class
        {
            Add(null, expression);
        }

        public static void Add<TFrom, TTo, TArgs>(string name, Func<TArgs, Expression<Func<TFrom, TTo>>> expression)
            where TArgs : class
        {
            var mapping = new ParameterizedMapping<TFrom, TTo, TArgs>(expression, name);
            Add(mapping);
        }

        public static void Add<TFrom, TTo, TContext>(Func<IQueryable<TFrom>, TContext, IQueryable<TTo>> expression)
        {
            Add(null, expression);
        }

        public static void Add<TFrom, TTo, TContext>(string name, Func<IQueryable<TFrom>, TContext, IQueryable<TTo>> expression)
        {
            var mapping = new QueryMapping<TFrom, TTo, TContext>(expression, name);
            Add(mapping);
        }

        public static void Add<TFrom, TTo, TArgs, TContext>(Func<TArgs, Func<IQueryable<TFrom>, TContext, IQueryable<TTo>>> expression)
        {
            Add(null, expression);
        }

        public static void Add<TFrom, TTo, TArgs, TContext>(string name, Func<TArgs, Func<IQueryable<TFrom>, TContext, IQueryable<TTo>>> expression)
        {
            var mapping = new ParameterizedQueryMapping<TFrom, TTo, TArgs, TContext>(expression, name);
            Add(mapping);
        }
    }
}
