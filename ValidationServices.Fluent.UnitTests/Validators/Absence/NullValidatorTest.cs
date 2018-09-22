using Xunit;
using ValidationServices.Fluent.Validators;
using ValidationServices.Fluent.Validators.Absence;
using ValidationServices.Fluent.UnitTests.TestEntities.Validators;

namespace ValidationServices.Fluent.UnitTests.Validators.Absence
{
    public class NullValidatorTest
    {
        private readonly AbsenceValidatorsTestEntity testEntity;

        public NullValidatorTest()
        {
            this.testEntity = new AbsenceValidatorsTestEntity();
        }

        [Fact]
        public void NullIsValidTest()
        {
            Assert.True(new NullValidator().Validate(new PropertyValidatorContext(
                this.testEntity, this.testEntity.NullObject)).IsValid);
        }

        [Fact]
        public void NullIsInvalidTest()
        {
            Assert.False(new NullValidator().Validate(new PropertyValidatorContext(
                this.testEntity, this.testEntity.NotEmptyString)).IsValid);
        }
    }
}
