using Xunit;
using System;
using ValidationServices.Fluent.Internal;
using ValidationServices.Fluent.Validators;
using ValidationServices.Fluent.Validators.Comparison;
using ValidationServices.Fluent.UnitTests.TestEntities;

namespace ValidationServices.Fluent.UnitTests.Validators.Comparison
{
    public class LessThanOrEqualValidatorTest
    {
        private readonly ValidatorsTestEntity testEntity;

        public LessThanOrEqualValidatorTest()
        {
            this.testEntity = new ValidatorsTestEntity();
        }

        [Fact]
        public void OnNotComparableValueThrowsArgumentExceptionTest()
        {
            Assert.Throws<ArgumentException>(() => new LessThanOrEqualValidator(8).Validate(
                new PropertyValidatorContext(this.testEntity, this.testEntity.NullObject)));
        }

        [Fact]
        public void GreaterValueIsInvalidTest()
        {
            Assert.False(new LessThanOrEqualValidator(9).Validate(
                new PropertyValidatorContext(this.testEntity, this.testEntity.Ten)).IsValid);
        }

        [Fact]
        public void EqualValueIsValidTest()
        {
            Assert.True(new LessThanOrEqualValidator(9).Validate(
                new PropertyValidatorContext(this.testEntity, this.testEntity.Nine)).IsValid);
        }

        [Fact]
        public void LesserValueIsValidTest()
        {
            Assert.True(new LessThanOrEqualValidator(9).Validate(
                new PropertyValidatorContext(this.testEntity, this.testEntity.Eight)).IsValid);
        }

        [Fact]
        public void GreaterValueWithLambdaIsInvalidTest()
        {
            Func<ValidatorsTestEntity, object> propertyFunc = (entity) => entity.Nine;
            Assert.False(new LessThanOrEqualValidator(propertyFunc.CoerceToNonGeneric()).Validate(
                new PropertyValidatorContext(this.testEntity, this.testEntity.Ten)).IsValid);
        }

        [Fact]
        public void EqualValueWithLambdaIsValidTest()
        {
            Func<ValidatorsTestEntity, object> propertyFunc = (entity) => entity.Nine;
            Assert.True(new LessThanOrEqualValidator(propertyFunc.CoerceToNonGeneric()).Validate(
                new PropertyValidatorContext(this.testEntity, this.testEntity.Nine)).IsValid);
        }

        [Fact]
        public void LesserValueWithLambdaIsValidTest()
        {
            Func<ValidatorsTestEntity, object> propertyFunc = (entity) => entity.Nine;
            Assert.True(new LessThanOrEqualValidator(propertyFunc.CoerceToNonGeneric()).Validate(
                new PropertyValidatorContext(this.testEntity, this.testEntity.Eight)).IsValid);
        }
    }
}
