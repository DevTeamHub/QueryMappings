using DevTeam.QueryMappings.Base;
using DevTeam.QueryMappings.Properties;
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
                throw new MappingException(string.Format(Resources.ContextResolverIsntRegisteredException, typeof(TContext).Name));

            return _contextResolver.Invoke(contextKey);
        }
    }
}
