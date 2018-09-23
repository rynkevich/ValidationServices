using Xunit;
using Moq;
using System.IO;
using ValidationServices.Service;
using ValidationServices.Logger;
using ValidationServices.UnitTests.TestEntities;
using ValidationServices.Service.Exceptions;

namespace ValidationServices.UnitTests
{
    public class LoggingValidationServiceTests
    {
        [Fact]
        public void ServiceLogsDetailsOnValidateTests()
        {
            var loggerMock = new Mock<ILogger>();

            var service = new ValidationService(true);
            var loggingService = new LoggingValidationService(service, loggerMock.Object, LogLevel.WARN);

            var invalidObject = new ValidationServiceTestEntity(
                digit: 25, negativeInteger: 5, oneCharString: "abc",
                requiredObject: null, notEmptyString: null, someObject: null);
            var conclusion = loggingService.Validate(invalidObject);

            loggerMock.Verify(validator => validator.Log(It.IsAny<string>(), LogLevel.WARN), 
                Times.Exactly(conclusion.Details.Count));
        }

        [Fact]
        public void OnLoggerErrorThrowsLoggingFailedExceptionTest()
        {
            var loggerMock = new Mock<ILogger>();
            loggerMock.Setup(validator => validator.Log(It.IsAny<string>(), It.IsAny<LogLevel>())).Throws<IOException>();

            var service = new ValidationService(true);
            var loggingService = new LoggingValidationService(service, loggerMock.Object, LogLevel.WARN);

            var invalidObject = new ValidationServiceTestEntity(
                digit: 25, negativeInteger: 5, oneCharString: "abc",
                requiredObject: null, notEmptyString: null, someObject: null);

            Assert.Throws<LoggingFailedException>(() => loggingService.Validate(invalidObject));
        }
    }
}
