namespace ValidationServices.Logger
{
    /// <summary>
    /// NLog Proxy.
    /// </summary>
    public class NLogLogger : ILogger
    {
        /// <summary>
        /// Actual logger.
        /// </summary>
        private readonly NLog.Logger _logger;

        /// <param name="utility">The actual logger</param>
        public NLogLogger(NLog.Logger utility)
        {
            this._logger = utility;
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
                    this._logger.Info(message);
                    break;
                case LogLevel.WARN:
                    this._logger.Warn(message);
                    break;
                case LogLevel.ERROR:
                    this._logger.Error(message);
                    break;
                case LogLevel.FATAL:
                    this._logger.Fatal(message);
                    break;
                default:
                    this._logger.Debug(message);
                    break;
            }
        }
    }
}
