using Xunit;
using ValidationServices.Fluent.Validators;
using ValidationServices.Fluent.Validators.Absence;
using ValidationServices.Fluent.UnitTests.TestEntities;

namespace ValidationServices.Fluent.UnitTests.Validators.Absence
{
    public class NullValidatorTests
    {
        private readonly ValidatorsTestEntity _testEntity;

        public NullValidatorTests()
        {
            this._testEntity = new ValidatorsTestEntity();
        }

        [Fact]
        public void NullIsValidTest()
        {
            Assert.True(new NullValidator().Validate(new PropertyValidatorContext(
                this._testEntity, this._testEntity.NullObject)).IsValid);
        }

        [Fact]
        public void NullIsInvalidTest()
        {
            Assert.False(new NullValidator().Validate(new PropertyValidatorContext(
                this._testEntity, this._testEntity.NotEmptyString)).IsValid);
        }
    }
}
