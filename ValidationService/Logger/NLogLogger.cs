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
        /// Log a message.
        /// </summary>
        /// <param name="message">The message to log</param>
        /// <param name="logLevel">The required log level</param>
        public void Log(string message, LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.INFO:
                    this.logger.Info(message);
                    break;
                case LogLevel.WARN:
                    this.logger.Warn(message);
                    break;
                case LogLevel.ERROR:
                    this.logger.Error(message);
                    break;
                case LogLevel.FATAL:
                    this.logger.Fatal(message);
                    break;
                default:
                    this.logger.Debug(message);
                    break;
            }
        }
    }
}
