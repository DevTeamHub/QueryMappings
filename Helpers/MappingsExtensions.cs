using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DevTeam.QueryMappings.Helpers
{
    public static class QueryMappingsExtensions
    {
        public static TModel AsModel<TModel>(this IQueryable query)
            where TModel: class
        {
		    return query.Invoke<TModel>("AsModel") as TModel;
        }

        public static IEnumerable<TModel> AsModelList<TModel>(this IQueryable query)
            where TModel : class
        {
            return query.Invoke<TModel>("AsModelList") as IEnumerable<TModel>;
        }

        private static object Invoke<TModel>(this IQueryable query, string name)
        {
            var method = typeof(QueryMappingsExtensions)
                .GetMethods(BindingFlags.Static | BindingFlags.NonPublic)
                .Single(x => x.Name == name && x.IsGenericMethodDefinition);

            var genericMethod = method.MakeGenericMethod(query.ElementType, typeof(TModel));
            return genericMethod.Invoke(null, new[] { query });
        }

        private static TModel AsModel<TEntity, TModel>(this IQueryable<TEntity> query)
        {
            return AsQuery<TEntity, TModel>(query).SingleOrDefault();
        }

        private static IEnumerable<TModel> AsModelList<TEntity, TModel>(this IQueryable<TEntity> query)
        {
            return AsQuery<TEntity, TModel>(query).ToList();
        }

        public static IQueryable<TModel> AsQuery<TEntity, TModel>(this IQueryable<TEntity> query)
        {
            var mapping = MappingsList.Get<TEntity, TModel>();
            return mapping.Apply(query);
        }
    }
}