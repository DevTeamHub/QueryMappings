using DevTeam.QueryMappings.Base;
using DevTeam.QueryMappings.Properties;
using System;

namespace DevTeam.QueryMappings.Helpers
{
    /// <summary>
    /// Used with Query and ParameterizedQuery Mappings to set up logic how we can get Entity Framework context.
    /// EF Context will be injected inside of mapping with help of this class.
    /// </summary>
    /// <typeparam name="TContext">Interface that EF context implements. It can be also type of EF Context itself.</typeparam>
    public static class ContextResolver<TContext>
    {
        /// <summary>
        /// Context resolver function.
        /// </summary>
        private static Func<object, TContext> _contextResolver;

        /// <summary>
        /// Should be used at the start of application to save function responsible for EF Context resolving.
        /// </summary>
        /// <param name="contextResolver">Function that knows how to resolve EF context.</param>
        public static void RegisterResolver(Func<object, TContext> contextResolver)
        {
            _contextResolver = contextResolver;
        }

        /// <summary>
        /// Resolves EF Context when we need to inject it into mappings.
        /// </summary>
        /// <exception cref="MappingException">Thrown if context resolver function wasn't registered before.</exception>
        /// <param name="contextKey">EF Context key. This is obligatory parameter. Allows to have more than one instance of context.</param>
        /// <returns>Instance of EF Context.</returns>
        public static TContext Resolve(object contextKey)
        {
            if (_contextResolver == null)
                throw new MappingException(string.Format(Resources.ContextResolverIsntRegisteredException, typeof(TContext).Name));

            return _contextResolver.Invoke(contextKey);
        }
    }
}
