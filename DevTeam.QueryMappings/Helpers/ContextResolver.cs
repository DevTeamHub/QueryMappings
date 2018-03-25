using System;

namespace DevTeam.QueryMappings.Helpers
{
    public static class ContextResolver<TContext>
    {
        private static Func<Type, TContext> _contextResolver;

        public static void RegisterResolver(Func<Type, TContext> contextResolver)
        {
            _contextResolver = contextResolver;
        }

        public static TContext Resolve(Type contextType)
        {
            return _contextResolver.Invoke(contextType);
        }
    }
}
