using DevTeam.QueryMappings.Base;
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
            if (_contextResolver == null)
                throw new MappingException("Context Resolver wasn't registered. To use Query Mappings you need to register context resolver function.");

            return _contextResolver.Invoke(contextKey);
        }
    }
}
