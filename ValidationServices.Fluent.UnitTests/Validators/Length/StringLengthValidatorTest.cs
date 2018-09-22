using Xunit;
using System;
using ValidationServices.Fluent.Internal;
using ValidationServices.Fluent.Validators;
using ValidationServices.Fluent.Validators.Length;
using ValidationServices.Fluent.UnitTests.TestEntities.Validators;

namespace ValidationServices.Fluent.UnitTests.Validators.Length
{
    public class StringLengthValidatorTest
    {
        private readonly StringLengthValidatorsTestEntity testEntity;

        public StringLengthValidatorTest()
        {
            this.testEntity = new StringLengthValidatorsTestEntity();
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
            Func<StringLengthValidatorsTestEntity, int> maxFunc =
                (StringLengthValidatorsTestEntity entity) => entity.Ten;
            Assert.Throws<ArgumentNullException>(() => new StringLengthValidator(null, 
                maxFunc.CoerceToNonGeneric()));
        }

        [Fact]
        public void OnNullMaxFuncThrowsArgumentNullExceptionTest()
        {
            Func<StringLengthValidatorsTestEntity, int> minFunc =
                (StringLengthValidatorsTestEntity entity) => entity.Five;
            Assert.Throws<ArgumentNullException>(() => new StringLengthValidator(null,
                minFunc.CoerceToNonGeneric()));
        }

        [Fact]
        public void NullStringIsInvalidTest()
        {
            Assert.False(new StringLengthValidator(0, 10).Validate(
                new PropertyValidatorContext(this.testEntity, this.testEntity.NullString)).IsValid);
        }

        [Fact]
        public void MinMaxConstraintedStringIsValidTest()
        {
            Assert.True(new StringLengthValidator(0, 10).Validate(
                new PropertyValidatorContext(this.testEntity, this.testEntity.EightCharString)).IsValid);
        }

        [Fact]
        public void MinMaxConstraintViolatingStringIsInvalidTest()
        {
            Assert.False(new StringLengthValidator(10, 15).Validate(
                new PropertyValidatorContext(this.testEntity, this.testEntity.EightCharString)).IsValid);
        }

        [Fact]
        public void MinFuncMaxFuncConstraintedStringIsValidTest()
        {
            Func<StringLengthValidatorsTestEntity, int> minFunc =
                (StringLengthValidatorsTestEntity entity) => entity.Five;
            Func<StringLengthValidatorsTestEntity, int> maxFunc =
                (StringLengthValidatorsTestEntity entity) => entity.Ten;
            Assert.True(new StringLengthValidator(
                minFunc.CoerceToNonGeneric(), maxFunc.CoerceToNonGeneric()).Validate(
                new PropertyValidatorContext(this.testEntity, this.testEntity.EightCharString)).IsValid);
        }

        [Fact]
        public void MinFuncMaxFuncConstraintViolatingStringIsInvalidTest()
        {
            Func<StringLengthValidatorsTestEntity, int> minFunc =
                (StringLengthValidatorsTestEntity entity) => entity.Nine;
            Func<StringLengthValidatorsTestEntity, int> maxFunc =
                (StringLengthValidatorsTestEntity entity) => entity.Ten;
            Assert.False(new StringLengthValidator(
                minFunc.CoerceToNonGeneric(), maxFunc.CoerceToNonGeneric()).Validate(
                new PropertyValidatorContext(this.testEntity, this.testEntity.EightCharString)).IsValid);
        }
    }
}
