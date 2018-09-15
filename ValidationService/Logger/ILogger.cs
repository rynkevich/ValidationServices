namespace ValidationService.Logger
{
    /// <summary>
    /// Logger interface.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Log using "Debug" log level.
        /// </summary>
        /// <param name="message">The message to log</param>
        void Debug(string message);

        /// <summary>
        /// Log using "Info" log level.
        /// </summary>
        /// <param name="message">The message to log</param>
        void Info(string message);

        /// <summary>
        /// Log using "Warn" log level.
        /// </summary>
        /// <param name="message">The message to log</param>
        void Warn(string message);

        /// <summary>
        /// Log using "Error" log level.
        /// </summary>
        /// <param name="message">The message to log</param>
        void Error(string message);

        /// <summary>
        /// Log using "Fatal" log level.
        /// </summary>
        /// <param name="message">The message to log</param>
        void Fatal(string message);
    }
}
