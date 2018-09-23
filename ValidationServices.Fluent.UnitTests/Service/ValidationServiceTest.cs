using Xunit;
using System;
using ValidationServices.Fluent.Service;
using ValidationServices.Fluent.UnitTests.TestEntities;
using ValidationServices.Fluent.Validators.Length;
using ValidationServices.Fluent.Validators.Comparison;
using ValidationServices.Fluent.Validators.Absence;

namespace ValidationServices.Fluent.UnitTests.Service
{
    public class ValidationServiceTest
    {
        private readonly ServiceTestEntity testEntity;

        public ValidationServiceTest()
        {
            this.testEntity = new ServiceTestEntity();
        }

        [Fact]
        public void OnNullObjectToValidateThrowsArgumentNullExceptionTest()
        {
            Assert.Throws<ArgumentNullException>(() => new CustomValidationService().Validate<object>(null));
        }

        [Fact]
        public void OnRepeatingRuleForCallForSamePropertyThrowsInvalidOperationExceptionTest()
        {
            ValidationServiceBuilder serviceBuilder = new ValidationServiceBuilder();
            serviceBuilder.RuleFor((ServiceTestEntity entity) => entity.EightCharString).MaxLength(10);
            Assert.Throws<InvalidOperationException>(() => serviceBuilder.RuleFor(
                (ServiceTestEntity entity) => entity.EightCharString).MinLength(5));
        }

        [Fact]
        public void OnTryValidateNullExpressionThrowsArgumentExceptionTest()
        {
            ValidationServiceBuilder serviceBuilder = new ValidationServiceBuilder();
            Assert.Throws<ArgumentException>(
                () => serviceBuilder.RuleFor<ServiceTestEntity, object>(entity => null).Null());
        }

        [Fact]
        public void OnTryValidateFieldThrowsArgumentExceptionTest()
        {
            ValidationServiceBuilder serviceBuilder = new ValidationServiceBuilder();
            Assert.Throws<ArgumentException>(() => serviceBuilder.RuleFor(
                (ServiceTestEntity entity) => entity.StringField).NotNull());
        }

        [Fact]
        public void OnTryValidateMethodResultThrowsArgumentExceptionTest()
        {
            ValidationServiceBuilder serviceBuilder = new ValidationServiceBuilder();
            Assert.Throws<ArgumentException>(() => serviceBuilder.RuleFor(
                (ServiceTestEntity entity) => entity.GetFive()).Equal(5));
        }
    }
}
