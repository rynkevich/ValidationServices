namespace ValidationService.Logger
{
    /// <summary>
    /// NLog Proxy.
    /// </summary>
    public class NLogLogger : ILogger
    {
        /// <summary>
        /// Actual logger.
        /// </summary>
        private readonly NLog.Logger logger;

        /// <param name="utility">The actual logger</param>
        public NLogLogger(NLog.Logger utility)
        {
            this.logger = utility;
        }

        /// <summary>
        /// Log using "Debug" log level.
        /// </summary>
        /// <param name="message">The message to log</param>
        public void Debug(string message)
        {
            this.logger.Debug(message);
        }

        /// <summary>
        /// Log using "Info" log level.
        /// </summary>
        /// <param name="message">The message to log</param>
        public void Info(string message)
        {
            this.logger.Info(message);
        }

        /// <summary>
        /// Log using "Warn" log level.
        /// </summary>
        /// <param name="message">The message to log</param>
        public void Warn(string message)
        {
            this.logger.Warn(message);
        }

        /// <summary>
        /// Log using "Error" log level.
        /// </summary>
        /// <param name="message">The message to log</param>
        public void Error(string message)
        {
            this.logger.Error(message);
        }

        /// <summary>
        /// Log using "Fatal" log level.
        /// </summary>
        /// <param name="message">The message to log</param>
        public void Fatal(string message)
        {
            this.logger.Fatal(message);
        }
    }
}
