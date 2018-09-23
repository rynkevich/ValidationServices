using Xunit;
using System.Collections.Generic;
using ValidationServices.Fluent.Validators;
using ValidationServices.Fluent.Validators.Absence;
using ValidationServices.Fluent.UnitTests.TestEntities;

namespace ValidationServices.Fluent.UnitTests.Validators.Absence
{
    public class NotEmptyValidatorTests
    {
        private readonly ValidatorsTestEntity _testEntity;

        public NotEmptyValidatorTests()
        {
            this._testEntity = new ValidatorsTestEntity();
        }

        [Fact]
        public void NullIsInvalidTest()
        {
            var validator = new NotEmptyValidator(default(object));
            Assert.False(validator.Validate(new PropertyValidatorContext(
                this._testEntity, this._testEntity.NullObject)).IsValid);
        }

        [Fact]
        public void EmptyStringIsInvalidTest()
        {
            var validator = new NotEmptyValidator(default(string));
            Assert.False(validator.Validate(new PropertyValidatorContext(
                this._testEntity, this._testEntity.EmptyString)).IsValid);
        }

        [Fact]
        public void WhitespaceStringIsInvalidTest()
        {
            var validator = new NotEmptyValidator(default(string));
            Assert.False(validator.Validate(new PropertyValidatorContext(
                this._testEntity, this._testEntity.WhitespaceString)).IsValid);
        }

        [Fact]
        public void EmptyCollectionIsInvalidTest()
        {
            var validator = new NotEmptyValidator(default(List<int>));
            Assert.False(validator.Validate(new PropertyValidatorContext(
                this._testEntity, this._testEntity.EmptyList)).IsValid);
        }

        [Fact]
        public void NotEmptyStringIsValidTest()
        {
            var validator = new NotEmptyValidator(default(string));
            Assert.True(validator.Validate(new PropertyValidatorContext(
                this._testEntity, this._testEntity.NotEmptyString)).IsValid);
        }

        [Fact]
        public void NotEmptyCollectionIsValidTest()
        {
            var validator = new NotEmptyValidator(default(List<int>));
            Assert.True(validator.Validate(new PropertyValidatorContext(
                this._testEntity, this._testEntity.NotEmptyList)).IsValid);
        }
    }
}
