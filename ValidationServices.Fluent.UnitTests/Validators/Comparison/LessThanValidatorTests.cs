using Xunit;
using System;
using System.Linq.Expressions;
using ValidationServices.Fluent.Internal;
using ValidationServices.Fluent.Validators;
using ValidationServices.Fluent.Validators.Comparison;
using ValidationServices.Fluent.UnitTests.TestEntities;

namespace ValidationServices.Fluent.UnitTests.Validators.Comparison
{
    public class LessThanValidatorTests
    {
        private readonly ValidatorsTestEntity _testEntity;

        public LessThanValidatorTests()
        {
            this._testEntity = new ValidatorsTestEntity();
        }

        [Fact]
        public void OnNotComparableValueThrowsArgumentExceptionTest()
        {
            Assert.Throws<ArgumentException>(() => new LessThanValidator(8).Validate(
                new PropertyValidatorContext(this._testEntity, this._testEntity.NullObject)));
        }

        [Fact]
        public void GreaterValueIsInvalidTest()
        {
            Assert.False(new LessThanValidator(9).Validate(
                new PropertyValidatorContext(this._testEntity, this._testEntity.Ten)).IsValid);
        }

        [Fact]
        public void LesserValueIsValidTest()
        {
            Assert.True(new LessThanValidator(9).Validate(
                new PropertyValidatorContext(this._testEntity, this._testEntity.Eight)).IsValid);
        }

        [Fact]
        public void GreaterValueWithLambdaIsInvalidTest()
        {
            Expression<Func<ValidatorsTestEntity, object>> propertyExpression = (entity) => entity.Nine;
            string funcBodyString = propertyExpression.Body.ToString();
            Assert.False(new LessThanValidator(propertyExpression.Compile().CoerceToNonGeneric(), funcBodyString)
                .Validate(new PropertyValidatorContext(this._testEntity, this._testEntity.Ten)).IsValid);
        }

        [Fact]
        public void LesserValueWithLambdaIsValidTest()
        {
            Expression<Func<ValidatorsTestEntity, object>> propertyExpression = (entity) => entity.Nine;
            string funcBodyString = propertyExpression.Body.ToString();
            Assert.True(new LessThanValidator(propertyExpression.Compile().CoerceToNonGeneric(), funcBodyString)
                .Validate(new PropertyValidatorContext(this._testEntity, this._testEntity.Eight)).IsValid);
        }
    }
}
