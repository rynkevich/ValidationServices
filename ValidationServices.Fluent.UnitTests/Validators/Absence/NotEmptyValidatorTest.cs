using Xunit;
using System.Collections.Generic;
using ValidationServices.Fluent.Validators;
using ValidationServices.Fluent.Validators.Absence;
using ValidationServices.Fluent.UnitTests.TestEntities.Validators;

namespace ValidationServices.Fluent.UnitTests.Validators.Absence
{
    public class NotEmptyValidatorTest
    {
        private readonly AbsenceValidatorsTestEntity testEntity;

        public NotEmptyValidatorTest()
        {
            this.testEntity = new AbsenceValidatorsTestEntity();
        }

        [Fact]
        public void NullIsInvalidTest()
        {
            var validator = new NotEmptyValidator(default(object));
            Assert.False(validator.Validate(new PropertyValidatorContext(
                this.testEntity, this.testEntity.NullObject)).IsValid);
        }

        [Fact]
        public void EmptyStringIsInvalidTest()
        {
            var validator = new NotEmptyValidator(default(string));
            Assert.False(validator.Validate(new PropertyValidatorContext(
                this.testEntity, this.testEntity.EmptyString)).IsValid);
        }

        [Fact]
        public void WhitespaceStringIsInvalidTest()
        {
            var validator = new NotEmptyValidator(default(string));
            Assert.False(validator.Validate(new PropertyValidatorContext(
                this.testEntity, this.testEntity.WhitespaceString)).IsValid);
        }

        [Fact]
        public void EmptyCollectionIsInvalidTest()
        {
            var validator = new NotEmptyValidator(default(List<int>));
            Assert.False(validator.Validate(new PropertyValidatorContext(
                this.testEntity, this.testEntity.EmptyList)).IsValid);
        }

        [Fact]
        public void NotEmptyStringIsValidTest()
        {
            var validator = new NotEmptyValidator(default(string));
            Assert.True(validator.Validate(new PropertyValidatorContext(
                this.testEntity, this.testEntity.NotEmptyString)).IsValid);
        }

        [Fact]
        public void NotEmptyCollectionIsValidTest()
        {
            var validator = new NotEmptyValidator(default(List<int>));
            Assert.True(validator.Validate(new PropertyValidatorContext(
                this.testEntity, this.testEntity.NotEmptyList)).IsValid);
        }
    }
}
