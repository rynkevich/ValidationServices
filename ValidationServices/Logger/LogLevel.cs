namespace ValidationServices.Logger
{
    /// <summary>
    /// Enum that contains possible log levels for ILogger. 
    /// </summary>
    public enum LogLevel
    {
        /// <summary>
        /// Debugging purposes level.
        /// </summary>
        DEBUG,

        /// <summary>
        /// Information messages level.
        /// </summary>
        INFO,

        /// <summary>
        /// Warning messages level (non-critical issues).
        /// </summary>
        WARN,

        /// <summary>
        /// Error/exception messages level.
        /// </summary>
        ERROR,

        /// <summary>
        /// Critical error messages level.
        /// </summary>
        FATAL
    }
}
