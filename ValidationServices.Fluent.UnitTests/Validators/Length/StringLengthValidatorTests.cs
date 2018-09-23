using Xunit;
using System;
using ValidationServices.Fluent.Internal;
using ValidationServices.Fluent.Validators;
using ValidationServices.Fluent.Validators.Length;
using ValidationServices.Fluent.UnitTests.TestEntities;

namespace ValidationServices.Fluent.UnitTests.Validators.Length
{
    public class StringLengthValidatorTests
    {
        private readonly ValidatorsTestEntity _testEntity;

        public StringLengthValidatorTests()
        {
            this._testEntity = new ValidatorsTestEntity();
        }

        [Fact]
        public void OnNegativeMinThrowsArgumentOutOfRangeExceptionTest()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new StringLengthValidator(-4, 1));
        }

        [Fact]
        public void OnNegativeMaxThrowsArgumentOutOfRangeExceptionTest()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new StringLengthValidator(1, -4));
        }

        [Fact]
        public void OnMinExceedsMaxThrowsArgumentOutOfRangeExceptionTest()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new StringLengthValidator(5, 1));
        }

        [Fact]
        public void OnNullMinFuncThrowsArgumentNullExceptionTest()
        {
            Func<ValidatorsTestEntity, int> maxFunc = (ValidatorsTestEntity entity) => entity.Ten;
            Assert.Throws<ArgumentNullException>(() => new StringLengthValidator(null, 
                maxFunc.CoerceToNonGeneric()));
        }

        [Fact]
        public void OnNullMaxFuncThrowsArgumentNullExceptionTest()
        {
            Func<ValidatorsTestEntity, int> minFunc = (ValidatorsTestEntity entity) => entity.Five;
            Assert.Throws<ArgumentNullException>(() => new StringLengthValidator(null,
                minFunc.CoerceToNonGeneric()));
        }

        [Fact]
        public void NullStringIsInvalidTest()
        {
            Assert.False(new StringLengthValidator(0, 10).Validate(
                new PropertyValidatorContext(this._testEntity, this._testEntity.NullString)).IsValid);
        }

        [Fact]
        public void MinMaxConstraintedStringIsValidTest()
        {
            Assert.True(new StringLengthValidator(0, 10).Validate(
                new PropertyValidatorContext(this._testEntity, this._testEntity.EightCharString)).IsValid);
        }

        [Fact]
        public void MinMaxConstraintViolatingStringIsInvalidTest()
        {
            Assert.False(new StringLengthValidator(10, 15).Validate(
                new PropertyValidatorContext(this._testEntity, this._testEntity.EightCharString)).IsValid);
        }

        [Fact]
        public void MinFuncMaxFuncConstraintedStringIsValidTest()
        {
            Func<ValidatorsTestEntity, int> minFunc = (ValidatorsTestEntity entity) => entity.Five;
            Func<ValidatorsTestEntity, int> maxFunc = (ValidatorsTestEntity entity) => entity.Ten;
            Assert.True(new StringLengthValidator(
                minFunc.CoerceToNonGeneric(), maxFunc.CoerceToNonGeneric()).Validate(
                new PropertyValidatorContext(this._testEntity, this._testEntity.EightCharString)).IsValid);
        }

        [Fact]
        public void MinFuncMaxFuncConstraintViolatingStringIsInvalidTest()
        {
            Func<ValidatorsTestEntity, int> minFunc = (ValidatorsTestEntity entity) => entity.Nine;
            Func<ValidatorsTestEntity, int> maxFunc = (ValidatorsTestEntity entity) => entity.Ten;
            Assert.False(new StringLengthValidator(
                minFunc.CoerceToNonGeneric(), maxFunc.CoerceToNonGeneric()).Validate(
                new PropertyValidatorContext(this._testEntity, this._testEntity.EightCharString)).IsValid);
        }
    }
}
