using System;
using System.Runtime.Serialization;

namespace DevTeam.QueryMappings.Base
{
    [Serializable]
    public class MappingNotFoundException: ApplicationException
    {
        public Type From { get; set; }

        public Type To { get; set; }

        public MappingNotFoundException()
        { }

        public MappingNotFoundException(string message)
            : base(message)
        { }

        public MappingNotFoundException(string message, Exception inner)
            : base(message, inner)
        { }

        protected MappingNotFoundException(SerializationInfo info, StreamingContext context)
            :base (info, context)
        { }
    }
}
