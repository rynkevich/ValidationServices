using Xunit;
using System;
using ValidationServices.Fluent.Internal;
using ValidationServices.Fluent.Validators;
using ValidationServices.Fluent.Validators.Length;
using ValidationServices.Fluent.UnitTests.TestEntities;

namespace ValidationServices.Fluent.UnitTests.Validators.Length
{
    public class ExactStringLengthValidatorTest
    {
        private readonly ValidatorsTestEntity testEntity;

        public ExactStringLengthValidatorTest()
        {
            this.testEntity = new ValidatorsTestEntity();
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
            Func<ValidatorsTestEntity, int> lengthFunc = (ValidatorsTestEntity entity) => entity.Eight;
            Assert.True(new ExactStringLengthValidator(
                lengthFunc.CoerceToNonGeneric()).Validate(
                new PropertyValidatorContext(this.testEntity, this.testEntity.EightCharString)).IsValid);
        }

        [Fact]
        public void ExactFuncConstraintViolatingStringIsInvalidTest()
        {
            Func<ValidatorsTestEntity, int> lengthFunc = (ValidatorsTestEntity entity) => entity.Five;
            Assert.False(new ExactStringLengthValidator(
                lengthFunc.CoerceToNonGeneric()).Validate(
                new PropertyValidatorContext(this.testEntity, this.testEntity.EightCharString)).IsValid);
        }
    }
}
