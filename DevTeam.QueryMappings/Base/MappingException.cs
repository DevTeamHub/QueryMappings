using System;
using System.Runtime.Serialization;

namespace DevTeam.QueryMappings.Base
{
    /// <summary>
    /// Special exception that is used to describe every exception that happens inside of QueryMappings library.
    /// Implements standart Exception pattern.
    /// </summary>
    [Serializable]
    public class MappingException: ApplicationException
    {
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
