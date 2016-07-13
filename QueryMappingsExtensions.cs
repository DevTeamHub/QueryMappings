using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DevTeam.QueryMappings
{
    public static class QueryMappingsExtensions
    {
        #region IQueryable

        public static TModel AsModel<TModel>(this object query)
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
            var mapping = QueryMappings.Get<TEntity, TModel>();
            var selectExpression = mapping.Get();
            return query.Select(selectExpression);
        }

        public static TEntity AsEntity<TModel, TEntity>(this TModel model)
            where TEntity: class, new()
        {
            var mapping = QueryMappings.Get<TModel, TEntity>();
            var func = mapping.Get().Compile();
            return func(model);
        }

        public static List<TEntity> AsEntityList<TModel, TEntity>(this IEnumerable<TModel> model)
            where TEntity : class, new()
        {
            var mapping = QueryMappings.Get<TModel, TEntity>();
            var func = mapping.Get().Compile();
            return model.Select(func).ToList();
        }

        public static TEntity Update<TEntity, TModel>(this TEntity entity, TModel model)
            where TEntity: class, new()
        {
            QueryMappings.Update(model, entity);
            return entity;
        }

        #endregion 
        
        #region Objects

        public static TModel Map<TModel>(this object model)
            where TModel : class
        {
            return model.Invoke<TModel>("AsResult") as TModel;
        }

        public static IEnumerable<TModel> Map<TModel>(this IEnumerable<object> model)
            where TModel : class
        {
            return model.Invoke<TModel>("AsResultList") as IEnumerable<TModel>;
        }
        
        public static object Invoke<TModel>(this object model, string name)
            where TModel : class
        {
            var method = typeof(QueryMappingsExtensions)
                .GetMethods(BindingFlags.Static | BindingFlags.NonPublic)
                .Single(x => x.Name == name && x.IsGenericMethodDefinition);

            var modelType = QueryMappings.GetType<TModel>();

            var genericMethod = method.MakeGenericMethod(modelType, typeof(TModel));
            return genericMethod.Invoke(null, new[] { model });
        }

        private static object Invoke<TModel>(this IEnumerable<object> model, string name)
            where TModel : class
        {
            var method = typeof(QueryMappingsExtensions)
                .GetMethods(BindingFlags.Static | BindingFlags.NonPublic)
                .Single(x => x.Name == name && x.IsGenericMethodDefinition);

            var modelType = QueryMappings.GetType<TModel>();

            var genericMethod = method.MakeGenericMethod(modelType, typeof(TModel));
            return genericMethod.Invoke(null, new[] { model });
        }
        
        private static TModel AsResult<TResult, TModel>(this TResult model)
            where TModel: class
            where TResult: class
        {
            return model.AsEnumerable<TResult, TModel>().SingleOrDefault();
        }
        
        private static List<TModel> AsResultList<TResult, TModel>(this List<TResult> list)
            where TModel : class
            where TResult : class
        {
            var mapping = QueryMappings.Get<TResult, TModel>();
            var selectFunc = mapping.Get().Compile();
            return list.Select(selectFunc).ToList();
        }
        
        public static IEnumerable<TModel> AsEnumerable<TResult, TModel>(this TResult model)
            where TModel : class
            where TResult : class
        {
            var mapping = QueryMappings.Get<TResult, TModel>();
            var selectFunc = mapping.Get().Compile();
            return model.AsEnumerable().Select(selectFunc);
        }

        public static IEnumerable<TModel> AsEnumerable<TModel>(this TModel model)
        {
            yield return model;
        }

        #endregion
         
    }
}