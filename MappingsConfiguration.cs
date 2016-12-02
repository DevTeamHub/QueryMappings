using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DevTeam.QueryMappings
{
    public static class MappingsConfiguration
    {
        public static void Register(params Assembly[] assemblies)
        {
            GetDefined(assemblies).ForEach(m => m.Setup());
        }

        private static List<IMappingsStorage> GetDefined(params Assembly[] assemblies)
        {
            var mappingsTypes = assemblies.SelectMany(x => x.GetTypes()).Where(t => typeof(IMappingsStorage).IsAssignableFrom(t));
            return mappingsTypes.Select(Activator.CreateInstance).Cast<IMappingsStorage>().ToList();
        }
    }
}
