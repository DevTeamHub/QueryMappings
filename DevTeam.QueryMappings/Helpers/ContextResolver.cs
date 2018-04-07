using System;

namespace DevTeam.QueryMappings.Helpers
{
    public static class ContextResolver<TContext>
    {
        private static Func<object, TContext> _contextResolver;

        public static void RegisterResolver(Func<object, TContext> contextResolver)
        {
            _contextResolver = contextResolver;
        }

        public static TContext Resolve(object contextKey = null)
        {
            return _contextResolver.Invoke(contextKey);
        }
    }
}
