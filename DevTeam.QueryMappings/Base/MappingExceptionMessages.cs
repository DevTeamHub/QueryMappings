namespace DevTeam.QueryMappings.Base
{
    public static class MappingExceptionMessages
    {
        public const string GeneralInitializationException = "Exception has happened during mappings initialization. Please see inner exception to find more details.";
        public const string NoEmptyConstructorInitializationException = "Couldn't find empty constructor for IMappingStorage implementation: {0}";
        public const string GeneralMappingStorageException = "General exception has happened on attempt to create IMappingStorage instance of type {0}";
        public const string MappingStorageSetupException = "Exception has happened inside of Setup method invokation on IMappingStorage of type {0}";
        public const string MoreThanOneMappingFoundException = "More than one mapping from {0} to {1} was found. If you have more than one mapping for the same model you can use Named Mappings (see documentation) or create different models for different mappings";
        public const string NameIsNullWhenSearchForNamedMappingException = "A few mappings from {0} to {1} were found, but 'name' argument wasn't passed into method. Can't choose correct one. Please pass 'name' argument explicitly";
        public const string MappingNotFoundException = "Mapping from {0} to {1} was not found.";
    }
}
