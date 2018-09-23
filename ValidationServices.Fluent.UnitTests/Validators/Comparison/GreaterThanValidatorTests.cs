using Xunit;
using System;
using System.Linq.Expressions;
using ValidationServices.Fluent.Internal;
using ValidationServices.Fluent.Validators;
using ValidationServices.Fluent.Validators.Comparison;
using ValidationServices.Fluent.UnitTests.TestEntities;

namespace ValidationServices.Fluent.UnitTests.Validators.Comparison
{
    public class GreaterThanValidatorTests
    {
        private readonly ValidatorsTestEntity _testEntity;

        public GreaterThanValidatorTests()
        {
            this._testEntity = new ValidatorsTestEntity();
        }

        [Fact]
        public void OnNotComparableValueThrowsArgumentExceptionTest()
        {
            Assert.Throws<ArgumentException>(() => new GreaterThanValidator(8).Validate(
                new PropertyValidatorContext(this._testEntity, this._testEntity.NullObject)));
        }

        [Fact]
        public void GreaterValueIsValidTest()
        {
            Assert.True(new GreaterThanValidator(9).Validate(
                new PropertyValidatorContext(this._testEntity, this._testEntity.Ten)).IsValid);
        }

        [Fact]
        public void LesserValueIsInvalidTest()
        {
            Assert.False(new GreaterThanValidator(9).Validate(
                new PropertyValidatorContext(this._testEntity, this._testEntity.Eight)).IsValid);
        }

        [Fact]
        public void GreaterValueWithLambdaIsValidTest()
        {
            Expression<Func<ValidatorsTestEntity, object>> propertyExpression = (entity) => entity.Nine;
            string funcBodyString = propertyExpression.Body.ToString();
            Assert.True(new GreaterThanValidator(propertyExpression.Compile().CoerceToNonGeneric(), funcBodyString)
                .Validate(new PropertyValidatorContext(this._testEntity, this._testEntity.Ten)).IsValid);
        }

        [Fact]
        public void LesserValueWithLambdaIsInvalidTest()
        {
            Expression<Func<ValidatorsTestEntity, object>> propertyExpression = (entity) => entity.Nine;
            string funcBodyString = propertyExpression.Body.ToString();
            Assert.False(new GreaterThanValidator(propertyExpression.Compile().CoerceToNonGeneric(), funcBodyString)
                .Validate(new PropertyValidatorContext(this._testEntity, this._testEntity.Eight)).IsValid);
        }
    }
}
