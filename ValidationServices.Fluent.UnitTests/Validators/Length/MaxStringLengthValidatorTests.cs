using Xunit;
using System;
using ValidationServices.Fluent.Internal;
using ValidationServices.Fluent.Validators;
using ValidationServices.Fluent.Validators.Length;
using ValidationServices.Fluent.UnitTests.TestEntities;

namespace ValidationServices.Fluent.UnitTests.Validators.Length
{
    public class MaxStringLengthValidatorTests
    {
        private readonly ValidatorsTestEntity _testEntity;

        public MaxStringLengthValidatorTests()
        {
            this._testEntity = new ValidatorsTestEntity();
        }

        [Fact]
        public void MaxConstraintedStringIsValidTest()
        {
            Assert.True(new MaxStringLengthValidator(10).Validate(
                new PropertyValidatorContext(this._testEntity, this._testEntity.EightCharString)).IsValid);
        }

        [Fact]
        public void MaxConstraintViolatingStringIsInvalidTest()
        {
            Assert.False(new MaxStringLengthValidator(5).Validate(
                new PropertyValidatorContext(this._testEntity, this._testEntity.EightCharString)).IsValid);
        }

        [Fact]
        public void MaxFuncConstraintedStringIsValidTest()
        {
            Func<ValidatorsTestEntity, int> maxFunc = (ValidatorsTestEntity entity) => entity.Nine;
            Assert.True(new MaxStringLengthValidator(
                maxFunc.CoerceToNonGeneric()).Validate(
                new PropertyValidatorContext(this._testEntity, this._testEntity.EightCharString)).IsValid);
        }

        [Fact]
        public void MaxFuncConstraintViolatingStringIsInvalidTest()
        {
            Func<ValidatorsTestEntity, int> maxFunc = (ValidatorsTestEntity entity) => entity.Five;
            Assert.False(new MaxStringLengthValidator(
                maxFunc.CoerceToNonGeneric()).Validate(
                new PropertyValidatorContext(this._testEntity, this._testEntity.EightCharString)).IsValid);
        }
    }
}
