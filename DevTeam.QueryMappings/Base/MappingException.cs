using System;
using System.Runtime.Serialization;

namespace DevTeam.QueryMappings.Base
{
    [Serializable]
    public class MappingException: ApplicationException
    {
        public Type From { get; set; }

        public Type To { get; set; }

        public MappingException()
        { }

        public MappingException(string message)
            : base(message)
        { }

        public MappingException(string message, Exception inner)
            : base(message, inner)
        { }

        protected MappingException(SerializationInfo info, StreamingContext context)
            :base (info, context)
        { }
    }
}
