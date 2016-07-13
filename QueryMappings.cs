using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DevTeam.QueryMappings
{
    public static class QueryMappings
    {
        private static readonly List<QueryMapping> Mappings;

        static QueryMappings()
        {
            Mappings = new List<QueryMapping>();
        }

        public static Type GetType<TTo>()
        {
            try
            {
                var mapping = Mappings.Single(m => m.IsTo<TTo>());
                return mapping.From;
            }
            catch (InvalidOperationException e)
            {
                return null;
            }
        }

        public static QueryMapping<TFrom, TTo> Get<TFrom, TTo>()
        {
            return (QueryMapping<TFrom, TTo>)Mappings.FirstOrDefault(m => m.Is<TFrom, TTo>(MappingType.Data));
        }

        public static TTo Update<TFrom, TTo>(TFrom model, TTo entity)
        {
            var updateMapping = (UpdateMapping<TFrom, TTo>)Mappings.FirstOrDefault(m => m.Is<TFrom, TTo>(MappingType.Update));
            entity = updateMapping.Update(model, entity);
            return entity;
        }

        public static Expression<Func<TFrom, TTo>> Expression<TFrom, TTo>()
        {
            return Get<TFrom, TTo>().Get();
        }

        public static Func<TFrom, TTo> Func<TFrom, TTo>()
        {
            return Expression<TFrom, TTo>().Compile();
        }

        public static void Add<TFrom, TTo>(Expression<Func<TFrom, TTo>> expression)
        {
            var mapping = new QueryMapping<TFrom, TTo>(expression);
            Mappings.Add(mapping);
        }

        public static void Add<TFrom, TTo>(Func<TFrom, TTo, TTo> updateFunc)
        {
            var mapping = new UpdateMapping<TFrom, TTo>(updateFunc);
            Mappings.Add(mapping);
        }
    }
}
