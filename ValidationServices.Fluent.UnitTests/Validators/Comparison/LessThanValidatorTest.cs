using Xunit;
using System;
using ValidationServices.Fluent.Internal;
using ValidationServices.Fluent.Validators;
using ValidationServices.Fluent.Validators.Comparison;
using ValidationServices.Fluent.UnitTests.TestEntities.Validators;

namespace ValidationServices.Fluent.UnitTests.Validators.Absence
{
    public class LessThanValidatorTest
    {
        private readonly ComparisonValidatorsTestEntity testEntity;

        public LessThanValidatorTest()
        {
            this.testEntity = new ComparisonValidatorsTestEntity();
        }

        [Fact]
        public void OnNotComparableValueThrowsArgumentExceptionTest()
        {
            Assert.Throws<ArgumentException>(() => new LessThanValidator(8).Validate(
                new PropertyValidatorContext(this.testEntity, this.testEntity.NullObject)));
        }

        [Fact]
        public void GreaterValueIsInvalidTest()
        {
            Assert.False(new LessThanValidator(9).Validate(
                new PropertyValidatorContext(this.testEntity, this.testEntity.Ten)).IsValid);
        }

        [Fact]
        public void LesserValueIsValidTest()
        {
            Assert.True(new LessThanValidator(9).Validate(
                new PropertyValidatorContext(this.testEntity, this.testEntity.Eight)).IsValid);
        }

        [Fact]
        public void GreaterValueWithLambdaIsInvalidTest()
        {
            Func<ComparisonValidatorsTestEntity, object> propertyFunc =
               (entity) => entity.Nine;
            Assert.False(new LessThanValidator(propertyFunc.CoerceToNonGeneric()).Validate(
                new PropertyValidatorContext(this.testEntity, this.testEntity.Ten)).IsValid);
        }

        [Fact]
        public void LesserValueWithLambdaIsValidTest()
        {
            Func<ComparisonValidatorsTestEntity, object> propertyFunc =
               (entity) => entity.Nine;
            Assert.True(new LessThanValidator(propertyFunc.CoerceToNonGeneric()).Validate(
                new PropertyValidatorContext(this.testEntity, this.testEntity.Eight)).IsValid);
        }
    }
}
