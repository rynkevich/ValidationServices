using Xunit;
using ValidationServices.Fluent.Validators;
using ValidationServices.Fluent.Validators.Absence;
using ValidationServices.Fluent.UnitTests.TestEntities.Validators;

namespace ValidationServices.Fluent.UnitTests.Validators.Absence
{
    public class NotNullValidatorTest
    {
        private readonly AbsenceValidatorsTestEntity testEntity;

        public NotNullValidatorTest()
        {
            this.testEntity = new AbsenceValidatorsTestEntity();
        }

        [Fact]
        public void NullIsInvalidTest()
        {
            Assert.False(new NotNullValidator().Validate(new PropertyValidatorContext(
                this.testEntity, this.testEntity.NullObject)).IsValid);
        }

        [Fact]
        public void NotNullIsValidTest()
        {
            Assert.True(new NotNullValidator().Validate(new PropertyValidatorContext(
                this.testEntity, this.testEntity.NotEmptyString)).IsValid);
        }
    }
}
