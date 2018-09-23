using Xunit;
using System;
using ValidationServices.Fluent.Internal;
using ValidationServices.Fluent.Validators;
using ValidationServices.Fluent.Validators.Length;
using ValidationServices.Fluent.UnitTests.TestEntities;

namespace ValidationServices.Fluent.UnitTests.Validators.Length
{
    public class MinStringLengthValidatorTests
    {
        private readonly ValidatorsTestEntity _testEntity;

        public MinStringLengthValidatorTests()
        {
            this._testEntity = new ValidatorsTestEntity();
        }

        [Fact]
        public void MinConstraintedStringIsValidTest()
        {
            Assert.True(new MinStringLengthValidator(0).Validate(
                new PropertyValidatorContext(this._testEntity, this._testEntity.EightCharString)).IsValid);
        }

        [Fact]
        public void MinConstraintViolatingStringIsInvalidTest()
        {
            Assert.False(new MinStringLengthValidator(10).Validate(
                new PropertyValidatorContext(this._testEntity, this._testEntity.EightCharString)).IsValid);
        }

        [Fact]
        public void MinFuncConstraintedStringIsValidTest()
        {
            Func<ValidatorsTestEntity, int> minFunc = (ValidatorsTestEntity entity) => entity.Five;
            Assert.True(new MinStringLengthValidator(
                minFunc.CoerceToNonGeneric()).Validate(
                new PropertyValidatorContext(this._testEntity, this._testEntity.EightCharString)).IsValid);
        }

        [Fact]
        public void MinFuncConstraintViolatingStringIsInvalidTest()
        {
            Func<ValidatorsTestEntity, int> minFunc = (ValidatorsTestEntity entity) => entity.Nine;
            Assert.False(new MinStringLengthValidator(
                minFunc.CoerceToNonGeneric()).Validate(
                new PropertyValidatorContext(this._testEntity, this._testEntity.EightCharString)).IsValid);
        }
    }
}
