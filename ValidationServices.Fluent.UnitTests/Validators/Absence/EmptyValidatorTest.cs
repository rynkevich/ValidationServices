using Xunit;
using System.Collections.Generic;
using ValidationServices.Fluent.Validators;
using ValidationServices.Fluent.Validators.Absence;
using ValidationServices.Fluent.UnitTests.TestEntities;

namespace ValidationServices.Fluent.UnitTests.Validators.Absence
{
    public class EmptyValidatorTest
    {
        private readonly ValidatorsTestEntity testEntity;

        public EmptyValidatorTest()
        {
            this.testEntity = new ValidatorsTestEntity();
        }

        [Fact]
        public void NullIsValidTest()
        {
            var validator = new EmptyValidator(default(object));
            Assert.True(validator.Validate(new PropertyValidatorContext(
                this.testEntity, this.testEntity.NullObject)).IsValid);
        }

        [Fact]
        public void EmptyStringIsValidTest()
        {
            var validator = new EmptyValidator(default(string));
            Assert.True(validator.Validate(new PropertyValidatorContext(
                this.testEntity, this.testEntity.EmptyString)).IsValid);
        }

        [Fact]
        public void WhitespaceStringIsValidTest()
        {
            var validator = new EmptyValidator(default(string));
            Assert.True(validator.Validate(new PropertyValidatorContext(
                this.testEntity, this.testEntity.WhitespaceString)).IsValid);
        }

        [Fact]
        public void EmptyCollectionIsValidTest()
        {
            var validator = new EmptyValidator(default(List<int>));
            Assert.True(validator.Validate(new PropertyValidatorContext(
                this.testEntity, this.testEntity.EmptyList)).IsValid);
        }

        [Fact]
        public void NotEmptyStringIsInvalidTest()
        {
            var validator = new EmptyValidator(default(string));
            Assert.False(validator.Validate(new PropertyValidatorContext(
                this.testEntity, this.testEntity.NotEmptyString)).IsValid);
        }

        [Fact]
        public void NotEmptyCollectionIsInvalidTest()
        {
            var validator = new EmptyValidator(default(List<int>));
            Assert.False(validator.Validate(new PropertyValidatorContext(
                this.testEntity, this.testEntity.NotEmptyList)).IsValid);
        }
    }
}
