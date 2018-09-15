using Xunit;
using Moq;
using ValidationService.Service;
using ValidationService.Logger;
using ValidationServiceTests.TestEntities;
using ValidationService.Results;
using ValidationService.Service.Exceptions;

namespace ValidationServiceTests
{
    public class LoggingValidationServiceTest
    {
        [Fact]
        public void ServiceLogsDetailsOnValidateTest()
        {
            Mock<ILogger> loggerMock = new Mock<ILogger>();

            BaseValidationService service = new ValidationService.Service.ValidationService(true);
            LoggingValidationService loggingService = new LoggingValidationService(service, loggerMock.Object, LogLevel.WARN);

            ValidationServiceTestEntity invalidObject = new ValidationServiceTestEntity(
                digit: 25, negativeInteger: 5, oneCharString: "abc",
                requiredObject: null, notEmptyString: null, someObject: null);
            GeneralConclusion conclusion =  loggingService.Validate(invalidObject);

            loggerMock.Verify(validator => validator.Log(It.IsAny<string>(), LogLevel.WARN), 
                Times.Exactly(conclusion.Details.Count));
        }

        [Fact]
        public void OnLoggerErrorThrowsLoggingFailedExceptionTest()
        {
            Mock<ILogger> loggerMock = new Mock<ILogger>();
            loggerMock.Setup(validator => validator.Log(It.IsAny<string>(), It.IsAny<LogLevel>())).Throws<LoggingFailedException>();

            BaseValidationService service = new ValidationService.Service.ValidationService(true);
            LoggingValidationService loggingService = new LoggingValidationService(service, loggerMock.Object, LogLevel.WARN);

            ValidationServiceTestEntity invalidObject = new ValidationServiceTestEntity(
                digit: 25, negativeInteger: 5, oneCharString: "abc",
                requiredObject: null, notEmptyString: null, someObject: null);

            Assert.Throws<LoggingFailedException>(() => loggingService.Validate(invalidObject));
        }
    }
}
