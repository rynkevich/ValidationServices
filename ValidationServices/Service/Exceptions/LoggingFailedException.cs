using System;

namespace ValidationServices.Service.Exceptions
{
    /// <summary>
    /// Exception that is thrown when logging can not be performed.
    /// </summary>
    public class LoggingFailedException : Exception
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public LoggingFailedException()
        {
        }

        /// <summary>
        /// Constructor with error message.
        /// </summary>
        /// <param name="message">The error message</param>
        public LoggingFailedException(string message) : base(message)
        {
        }

        /// <summary>
        /// Constructor with error message and inner exception.
        /// </summary>
        /// <param name="message">The error message</param>
        /// <param name="inner">The inner exception</param>
        public LoggingFailedException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
