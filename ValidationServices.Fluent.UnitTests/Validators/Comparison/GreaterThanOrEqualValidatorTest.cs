using Xunit;
using System;
using ValidationServices.Fluent.Internal;
using ValidationServices.Fluent.Validators;
using ValidationServices.Fluent.Validators.Comparison;
using ValidationServices.Fluent.UnitTests.TestEntities.Validators;

namespace ValidationServices.Fluent.UnitTests.Validators.Absence
{
    public class GreaterThanOrEqualValidatorTest
    {
        private readonly ComparisonValidatorsTestEntity testEntity;

        public GreaterThanOrEqualValidatorTest()
        {
            this.testEntity = new ComparisonValidatorsTestEntity();
        }

        [Fact]
        public void OnNotComparableValueThrowsArgumentExceptionTest()
        {
            Assert.Throws<ArgumentException>(() => new GreaterThanOrEqualValidator(8).Validate(
                new PropertyValidatorContext(this.testEntity, this.testEntity.NullObject)));
        }

        [Fact]
        public void GreaterValueIsValidTest()
        {
            Assert.True(new GreaterThanOrEqualValidator(9).Validate(
                new PropertyValidatorContext(this.testEntity, this.testEntity.Ten)).IsValid);
        }

        [Fact]
        public void EqualValueIsValidTest()
        {
            Assert.True(new GreaterThanOrEqualValidator(9).Validate(
                new PropertyValidatorContext(this.testEntity, this.testEntity.Nine)).IsValid);
        }

        [Fact]
        public void LesserValueIsInvalidTest()
        {
            Assert.False(new GreaterThanOrEqualValidator(9).Validate(
                new PropertyValidatorContext(this.testEntity, this.testEntity.Eight)).IsValid);
        }

        [Fact]
        public void GreaterValueWithLambdaIsValidTest()
        {
            Func<ComparisonValidatorsTestEntity, object> propertyFunc =
               (entity) => entity.Nine;
            Assert.True(new GreaterThanOrEqualValidator(propertyFunc.CoerceToNonGeneric()).Validate(
                new PropertyValidatorContext(this.testEntity, this.testEntity.Ten)).IsValid);
        }

        [Fact]
        public void EqualValueWithLambdaIsValidTest()
        {
            Func<ComparisonValidatorsTestEntity, object> propertyFunc =
              (entity) => entity.Nine;
            Assert.True(new GreaterThanOrEqualValidator(propertyFunc.CoerceToNonGeneric()).Validate(
                new PropertyValidatorContext(this.testEntity, this.testEntity.Nine)).IsValid);
        }

        [Fact]
        public void LesserValueWithLambdaIsInvalidTest()
        {
            Func<ComparisonValidatorsTestEntity, object> propertyFunc =
               (entity) => entity.Nine;
            Assert.False(new GreaterThanOrEqualValidator(propertyFunc.CoerceToNonGeneric()).Validate(
                new PropertyValidatorContext(this.testEntity, this.testEntity.Eight)).IsValid);
        }
    }
}
