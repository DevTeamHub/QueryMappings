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
        /// <summary>
        /// Creates instance of MappingException
        /// </summary>
        public MappingException()
        { }

        /// <summary>
        /// Creates instance of MappingException
        /// </summary>
        /// <param name="message">Custom exception message.</param>
        public MappingException(string message)
            : base(message)
        { }

        /// <summary>
        /// Creates instance of MappingException
        /// </summary>
        /// <param name="message">Custom exception message.</param>
        /// <param name="inner">Exception that will be presented as Inner Exception.</param>
        public MappingException(string message, Exception inner)
            : base(message, inner)
        { }

        /// <summary>
        /// Creates instance of MappingException
        /// </summary>
        /// <param name="info">Serialization Info</param>
        /// <param name="context">Streaming context</param>
        protected MappingException(SerializationInfo info, StreamingContext context)
            :base (info, context)
        { }
    }
}
