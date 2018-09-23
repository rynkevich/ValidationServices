using System;
using ValidationServices.Logger;
using ValidationServices.Results;
using ValidationServices.Service.Exceptions;

namespace ValidationServices.Service
{
    /// <summary>
    /// Decorator class for ValidationServices that logs details after each validation act.
    /// </summary>
    public sealed class LoggingValidationService : BaseValidationService
    {
        /// <summary>
        /// Actual service that performs validation.
        /// </summary>
        private readonly BaseValidationService _service;

        /// <summary>
        /// Utility to log validation details.
        /// </summary>
        private readonly ILogger _logger;
        
        /// <summary>
        /// Required log level for details.
        /// </summary>
        private readonly LogLevel _logLevel;

        /// <summary>
        /// Gets or sets a flag indicating whether the validation should be accomplished recursively.
        /// </summary>
        public override bool IsRecursiveValidation {
            get => this._service.IsRecursiveValidation;
            set => this._service.IsRecursiveValidation = value;
        }

        /// <param name="service">The service that performs validation</param>
        /// <param name="logger">The utility to log validation details</param>
        /// <param name="logLevel">The log level for details</param>
        public LoggingValidationService(BaseValidationService service, ILogger logger, 
            LogLevel logLevel = LogLevel.WARN)
        {
            this._service = service;
            this.IsRecursiveValidation = service.IsRecursiveValidation;
            this._logger = logger;
            this._logLevel = logLevel;
        }

        /// <summary>
        /// Override of <see cref="BaseValidationService.Validate{T}(T, string)"/>
        /// </summary>
        /// <remarks>
        /// Validation is performed by <see cref="BaseValidationService"/> that is used to construct instance of this class.
        /// Details for every invalid object, scanned with this method, will be logged.
        /// </remarks>
        /// <typeparam name="T">The type of object to be validated</typeparam>
        /// <param name="objectToValidate">The object to validate</param>
        /// <param name="objectName">The object name. 
        /// Used to print full qualified property names to <see cref="ServiceConclusion.Details"/></param>
        /// <returns>
        /// <see cref="ServiceConclusion"/> with <c>IsValid</c> flag set to <c>true</c> 
        /// if the <paramref name="objectToValidate"/> is acceptable. Otherwise, the flag is set to <c>false</c> and
        /// <see cref="ServiceConclusion.Details"/> contains a report on problems.
        /// </returns>
        /// <exception cref="LoggingFailedException">Thrown if <see cref="ILogger.Log(string, Logger.LogLevel)"/> fails</exception>
        public override ServiceConclusion Validate<T>(T objectToValidate, string objectName = "")
        {
            ServiceConclusion conclusion = this._service.Validate(objectToValidate, objectName);

            if (conclusion.Details != null)
            {
                try
                {
                    foreach (var detail in conclusion.Details)
                    {
                        this._logger.Log(detail, this._logLevel);
                    }
                }
                catch (Exception ex)
                {
                    throw new LoggingFailedException(Resources.Service.LoggingFailedExceptionCantLog, ex);
                }
            }

            return conclusion;
        }
    }
}
