namespace ValidationService.Logger
{
    /// <summary>
    /// Logger interface.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Log using specified log level.
        /// </summary>
        /// <param name="message">The message to log</param>
        /// <param name="logLevel">The required log level</param>
        void Log(string message, LogLevel logLevel);
    }
}
