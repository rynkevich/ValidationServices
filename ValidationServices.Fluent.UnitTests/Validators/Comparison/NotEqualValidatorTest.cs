using Xunit;
using ValidationServices.Fluent.Validators;
using ValidationServices.Fluent.Validators.Comparison;
using ValidationServices.Fluent.UnitTests.TestEntities;

namespace ValidationServices.Fluent.UnitTests.Validators.Comparison
{
    public class NotEqualValidatorTest
    {
        private readonly ValidatorsTestEntity testEntity;

        public NotEqualValidatorTest()
        {
            this.testEntity = new ValidatorsTestEntity();
        }

        [Fact]
        public void EqualValuesAreInvalidTest()
        {
            Assert.False(new NotEqualValidator(8).Validate(
                new PropertyValidatorContext(this.testEntity, this.testEntity.Eight)).IsValid);
        }

        [Fact]
        public void NotEqualValuesAreValidTest()
        {
            Assert.True(new NotEqualValidator(5).Validate(
                new PropertyValidatorContext(this.testEntity, this.testEntity.Eight)).IsValid);
        }
    }   
}
