using System;
using ValidationService.Logger;
using ValidationService.Results;
using ValidationService.Service.Exceptions;

namespace ValidationService.Service
{
    /// <summary>
    /// Decorator class for ValidationService that logs details after each validation act.
    /// </summary>
    public sealed class LoggingValidationService : BaseValidationService
    {
        /// <summary>
        /// Actual service that performs validation.
        /// </summary>
        private readonly BaseValidationService service;

        /// <summary>
        /// Utility to log validation details.
        /// </summary>
        private readonly ILogger logger;
        
        /// <summary>
        /// Required log level for details.
        /// </summary>
        private readonly LogLevel logLevel;

        /// <summary>
        /// Gets or sets a flag indicating whether the validation should be accomplished recursively.
        /// </summary>
        public override bool IsRecursiveValidation {
            get => this.service.IsRecursiveValidation;
            set => this.service.IsRecursiveValidation = value;
        }

        /// <param name="service">The service that performs validation</param>
        /// <param name="logger">The utility to log validation details</param>
        /// <param name="logLevel">The log level for details</param>
        public LoggingValidationService(BaseValidationService service, ILogger logger, 
            LogLevel logLevel = LogLevel.WARN)
        {
            this.service = service;
            this.IsRecursiveValidation = service.IsRecursiveValidation;
            this.logger = logger;
            this.logLevel = logLevel;
        }

        /// <summary>
        /// Override of <see cref="BaseValidationService.Validate{T}(T, string)"/>
        /// </summary>
        /// <remarks>
        /// Validation is performed by <see cref="BaseValidationService"/> that is used to construct instance of this class.
        /// Details for every invalid object, scanned with this method, will be logged.
        /// </remarks>
        /// <typeparam name="T">The type of object to be validated</typeparam>
        /// <param name="obj">The object to validate</param>
        /// <param name="objName">The object name. 
        /// Used to print full qualified property names to <see cref="GeneralConclusion.Details"/></param>
        /// <returns>
        /// <see cref="GeneralConclusion"/> with <c>IsValid</c> flag set to <c>true</c> 
        /// if the <paramref name="obj"/> is acceptable. Otherwise, the flag is set to <c>false</c> and
        /// <see cref="GeneralConclusion.Details"/> contains a report on problems.
        /// </returns>
        /// <exception cref="LoggingFailedException">Thrown if <see cref="ILogger.Log(string, Logger.LogLevel)"/> fails</exception>
        public override GeneralConclusion Validate<T>(T obj, string objName = "")
        {
            GeneralConclusion conclusion = this.service.Validate(obj, objName);

            if (conclusion.Details != null)
            {
                try
                {
                    foreach (string detail in conclusion.Details)
                    {
                        this.logger.Log(detail, this.logLevel);
                    }
                }
                catch (Exception ex)
                {
                    throw new LoggingFailedException("Can not call Log method of configured logger", ex);
                }
            }

            return conclusion;
        }
    }
}
