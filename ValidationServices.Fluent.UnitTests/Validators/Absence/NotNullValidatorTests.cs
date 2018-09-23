using Xunit;
using ValidationServices.Fluent.Validators;
using ValidationServices.Fluent.Validators.Absence;
using ValidationServices.Fluent.UnitTests.TestEntities;

namespace ValidationServices.Fluent.UnitTests.Validators.Absence
{
    public class NotNullValidatorTests
    {
        private readonly ValidatorsTestEntity _testEntity;

        public NotNullValidatorTests()
        {
            this._testEntity = new ValidatorsTestEntity();
        }

        [Fact]
        public void NullIsInvalidTest()
        {
            Assert.False(new NotNullValidator().Validate(new PropertyValidatorContext(
                this._testEntity, this._testEntity.NullObject)).IsValid);
        }

        [Fact]
        public void NotNullIsValidTest()
        {
            Assert.True(new NotNullValidator().Validate(new PropertyValidatorContext(
                this._testEntity, this._testEntity.NotEmptyString)).IsValid);
        }
    }
}
