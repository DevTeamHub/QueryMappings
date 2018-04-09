using DevTeam.QueryMappings.Base;
using DevTeam.QueryMappings.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DevTeam.QueryMappings.Helpers
{
    public static class MappingsConfiguration
    {
        public static void Register(params Assembly[] assemblies)
        {
            try
            {
                GetDefined(assemblies).ForEach(Setup);
            }
            catch (Exception exception)
            {
                throw new MappingException(Resources.GeneralInitializationException, exception);
            }
        }

        private static List<IMappingsStorage> GetDefined(params Assembly[] assemblies)
        {
            var mappingsTypes = assemblies.SelectMany(x => x.GetTypes()).Where(t => typeof(IMappingsStorage).IsAssignableFrom(t));
            return mappingsTypes.Select(CreateStorageInstance).Cast<IMappingsStorage>().ToList();
        }

        private static object CreateStorageInstance(Type storageType)
        {
            try
            {
                return Activator.CreateInstance(storageType);
            }
            catch (MissingMethodException mmmException)
            {
                var exceptionMessage = string.Format(Resources.NoEmptyConstructorInitializationException, storageType.FullName);
                throw new MappingException(exceptionMessage, mmmException);
            }
            catch (Exception exception)
            {
                throw new MappingException(Resources.GeneralMappingStorageException, exception);
            }
        }

        private static void Setup(IMappingsStorage mappingsStorage)
        {
            try
            {
                mappingsStorage.Setup();
            }
            catch (Exception exception)
            {
                var exceptionMessage = string.Format(Resources.MappingStorageSetupException, mappingsStorage.GetType().FullName);
                throw new MappingException(exceptionMessage, exception);
            }
        }
    }
}
