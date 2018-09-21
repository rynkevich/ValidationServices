using Xunit;
using System;
using ValidationServices.Fluent.Internal;
using ValidationServices.Fluent.Validators;
using ValidationServices.Fluent.Validators.Length;
using ValidationServices.Fluent.UnitTests.TestEntities.Validators;

namespace ValidationServices.Fluent.UnitTests.Validators
{
    public class MaxStringLengthValidatorTest
    {
        private readonly StringLengthValidatorsTestEntity testEntity;

        public MaxStringLengthValidatorTest()
        {
            this.testEntity = new StringLengthValidatorsTestEntity();
        }

        [Fact]
        public void MaxConstraintedStringIsValidTest()
        {
            Assert.True(new MaxStringLengthValidator(10).Validate(
                new PropertyValidatorContext(this.testEntity, this.testEntity.EightCharString)).IsValid);
        }

        [Fact]
        public void MaxConstraintViolatingStringIsInvalidTest()
        {
            Assert.False(new MaxStringLengthValidator(5).Validate(
                new PropertyValidatorContext(this.testEntity, this.testEntity.EightCharString)).IsValid);
        }

        [Fact]
        public void MaxFuncConstraintedStringIsValidTest()
        {
            Func<StringLengthValidatorsTestEntity, int> maxFunc =
                (StringLengthValidatorsTestEntity entity) => entity.Nine;
            Assert.True(new MaxStringLengthValidator(
                maxFunc.CoerceToNonGeneric()).Validate(
                new PropertyValidatorContext(this.testEntity, this.testEntity.EightCharString)).IsValid);
        }

        [Fact]
        public void MaxFuncConstraintViolatingStringIsInvalidTest()
        {
            Func<StringLengthValidatorsTestEntity, int> maxFunc =
                (StringLengthValidatorsTestEntity entity) => entity.Five;
            Assert.False(new MaxStringLengthValidator(
                maxFunc.CoerceToNonGeneric()).Validate(
                new PropertyValidatorContext(this.testEntity, this.testEntity.EightCharString)).IsValid);
        }
    }
}
