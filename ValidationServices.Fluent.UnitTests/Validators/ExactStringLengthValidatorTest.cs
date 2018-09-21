using Xunit;
using System;
using ValidationServices.Fluent.Internal;
using ValidationServices.Fluent.Validators;
using ValidationServices.Fluent.Validators.Length;
using ValidationServices.Fluent.UnitTests.TestEntities.Validators;

namespace ValidationServices.Fluent.UnitTests.Validators
{
    public class ExactStringLengthValidatorTest
    {
        private readonly StringLengthValidatorsTestEntity testEntity;

        public ExactStringLengthValidatorTest()
        {
            this.testEntity = new StringLengthValidatorsTestEntity();
        }

        [Fact]
        public void ExactConstraintedStringIsValidTest()
        {
            Assert.True(new ExactStringLengthValidator(8).Validate(
                new PropertyValidatorContext(this.testEntity, this.testEntity.EightCharString)).IsValid);
        }

        [Fact]
        public void ExactConstraintViolatingStringIsInvalidTest()
        {
            Assert.False(new ExactStringLengthValidator(5).Validate(
                new PropertyValidatorContext(this.testEntity, this.testEntity.EightCharString)).IsValid);
        }

        [Fact]
        public void ExactFuncConstraintedStringIsValidTest()
        {
            Func<StringLengthValidatorsTestEntity, int> lengthFunc =
                (StringLengthValidatorsTestEntity entity) => entity.Eight;
            Assert.True(new ExactStringLengthValidator(
                lengthFunc.CoerceToNonGeneric()).Validate(
                new PropertyValidatorContext(this.testEntity, this.testEntity.EightCharString)).IsValid);
        }

        [Fact]
        public void ExactFuncConstraintViolatingStringIsInvalidTest()
        {
            Func<StringLengthValidatorsTestEntity, int> lengthFunc =
                (StringLengthValidatorsTestEntity entity) => entity.Five;
            Assert.False(new ExactStringLengthValidator(
                lengthFunc.CoerceToNonGeneric()).Validate(
                new PropertyValidatorContext(this.testEntity, this.testEntity.EightCharString)).IsValid);
        }
    }
}
