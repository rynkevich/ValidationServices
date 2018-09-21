using Xunit;
using System;
using ValidationServices.Fluent.Internal;
using ValidationServices.Fluent.Validators;
using ValidationServices.Fluent.Validators.Length;
using ValidationServices.Fluent.UnitTests.TestEntities.Validators;

namespace ValidationServices.Fluent.UnitTests.Validators
{
    public class MinStringLengthValidatorTest
    {
        private readonly StringLengthValidatorsTestEntity testEntity;

        public MinStringLengthValidatorTest()
        {
            this.testEntity = new StringLengthValidatorsTestEntity();
        }

        [Fact]
        public void MinConstraintedStringIsValidTest()
        {
            Assert.True(new MinStringLengthValidator(0).Validate(
                new PropertyValidatorContext(this.testEntity, this.testEntity.EightCharString)).IsValid);
        }

        [Fact]
        public void MinConstraintViolatingStringIsInvalidTest()
        {
            Assert.False(new MinStringLengthValidator(10).Validate(
                new PropertyValidatorContext(this.testEntity, this.testEntity.EightCharString)).IsValid);
        }

        [Fact]
        public void MinFuncConstraintedStringIsValidTest()
        {
            Func<StringLengthValidatorsTestEntity, int> minFunc =
                (StringLengthValidatorsTestEntity entity) => entity.Five;
            Assert.True(new MinStringLengthValidator(
                minFunc.CoerceToNonGeneric()).Validate(
                new PropertyValidatorContext(this.testEntity, this.testEntity.EightCharString)).IsValid);
        }

        [Fact]
        public void MinFuncConstraintViolatingStringIsInvalidTest()
        {
            Func<StringLengthValidatorsTestEntity, int> minFunc =
                (StringLengthValidatorsTestEntity entity) => entity.Nine;
            Assert.False(new MinStringLengthValidator(
                minFunc.CoerceToNonGeneric()).Validate(
                new PropertyValidatorContext(this.testEntity, this.testEntity.EightCharString)).IsValid);
        }
    }
}
