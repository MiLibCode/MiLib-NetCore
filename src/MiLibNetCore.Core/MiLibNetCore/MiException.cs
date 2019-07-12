using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace MiLibNetCore
{
    /// <summary>
    /// Base exception type for those are thrown by Abp system for Abp specific exceptions.
    /// </summary>
    public class MiException : Exception
    {
        /// <summary>
        /// Creates a new <see cref="MiException"/> object.
        /// </summary>
        public MiException()
        {

        }

        /// <summary>
        /// Creates a new <see cref="MiException"/> object.
        /// </summary>
        /// <param name="message">Exception message</param>
        public MiException(string message)
            : base(message)
        {

        }

        /// <summary>
        /// Creates a new <see cref="MiException"/> object.
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="innerException">Inner exception</param>
        public MiException(string message, Exception innerException)
            : base(message, innerException)
        {

        }

        /// <summary>
        /// Constructor for serializing.
        /// </summary>
        public MiException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }
    }
}
