using Xunit;
using System;
using ValidationServices.Fluent.Internal;
using ValidationServices.Fluent.Validators;
using ValidationServices.Fluent.Validators.Comparison;
using ValidationServices.Fluent.UnitTests.TestEntities;

namespace ValidationServices.Fluent.UnitTests.Validators.Comparison
{
    public class GreaterThanValidatorTest
    {
        private readonly ValidatorsTestEntity testEntity;

        public GreaterThanValidatorTest()
        {
            this.testEntity = new ValidatorsTestEntity();
        }

        [Fact]
        public void OnNotComparableValueThrowsArgumentExceptionTest()
        {
            Assert.Throws<ArgumentException>(() => new GreaterThanValidator(8).Validate(
                new PropertyValidatorContext(this.testEntity, this.testEntity.NullObject)));
        }

        [Fact]
        public void GreaterValueIsValidTest()
        {
            Assert.True(new GreaterThanValidator(9).Validate(
                new PropertyValidatorContext(this.testEntity, this.testEntity.Ten)).IsValid);
        }

        [Fact]
        public void LesserValueIsInvalidTest()
        {
            Assert.False(new GreaterThanValidator(9).Validate(
                new PropertyValidatorContext(this.testEntity, this.testEntity.Eight)).IsValid);
        }

        [Fact]
        public void GreaterValueWithLambdaIsValidTest()
        {
            Func<ValidatorsTestEntity, object> propertyFunc = (entity) => entity.Nine;
            Assert.True(new GreaterThanValidator(propertyFunc.CoerceToNonGeneric()).Validate(
                new PropertyValidatorContext(this.testEntity, this.testEntity.Ten)).IsValid);
        }

        [Fact]
        public void LesserValueWithLambdaIsInvalidTest()
        {
            Func<ValidatorsTestEntity, object> propertyFunc = (entity) => entity.Nine;
            Assert.False(new GreaterThanValidator(propertyFunc.CoerceToNonGeneric()).Validate(
                new PropertyValidatorContext(this.testEntity, this.testEntity.Eight)).IsValid);
        }
    }
}
