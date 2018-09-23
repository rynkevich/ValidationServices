using Xunit;
using System;
using System.Linq.Expressions;
using ValidationServices.Fluent.Internal;
using ValidationServices.Fluent.Validators;
using ValidationServices.Fluent.Validators.Comparison;
using ValidationServices.Fluent.UnitTests.TestEntities;

namespace ValidationServices.Fluent.UnitTests.Validators.Comparison
{
    public class GreaterThanOrEqualValidatorTests
    {
        private readonly ValidatorsTestEntity _testEntity;

        public GreaterThanOrEqualValidatorTests()
        {
            this._testEntity = new ValidatorsTestEntity();
        }

        [Fact]
        public void OnNotComparableValueThrowsArgumentExceptionTest()
        {
            Assert.Throws<ArgumentException>(() => new GreaterThanOrEqualValidator(8).Validate(
                new PropertyValidatorContext(this._testEntity, this._testEntity.NullObject)));
        }

        [Fact]
        public void GreaterValueIsValidTest()
        {
            Assert.True(new GreaterThanOrEqualValidator(9).Validate(
                new PropertyValidatorContext(this._testEntity, this._testEntity.Ten)).IsValid);
        }

        [Fact]
        public void EqualValueIsValidTest()
        {
            Assert.True(new GreaterThanOrEqualValidator(9).Validate(
                new PropertyValidatorContext(this._testEntity, this._testEntity.Nine)).IsValid);
        }

        [Fact]
        public void LesserValueIsInvalidTest()
        {
            Assert.False(new GreaterThanOrEqualValidator(9).Validate(
                new PropertyValidatorContext(this._testEntity, this._testEntity.Eight)).IsValid);
        }

        [Fact]
        public void GreaterValueWithLambdaIsValidTest()
        {
            Expression<Func<ValidatorsTestEntity, object>> propertyExpression = (entity) => entity.Nine;
            string funcBodyString = propertyExpression.Body.ToString();
            Assert.True(new GreaterThanOrEqualValidator(propertyExpression.Compile().CoerceToNonGeneric(), funcBodyString)
                .Validate(new PropertyValidatorContext(this._testEntity, this._testEntity.Ten)).IsValid);
        }

        [Fact]
        public void EqualValueWithLambdaIsValidTest()
        {
            Expression<Func<ValidatorsTestEntity, object>> propertyExpression = (entity) => entity.Nine;
            string funcBodyString = propertyExpression.Body.ToString();
            Assert.True(new GreaterThanOrEqualValidator(propertyExpression.Compile().CoerceToNonGeneric(), funcBodyString)
                .Validate(new PropertyValidatorContext(this._testEntity, this._testEntity.Nine)).IsValid);
        }

        [Fact]
        public void LesserValueWithLambdaIsInvalidTest()
        {
            Expression<Func<ValidatorsTestEntity, object>> propertyExpression = (entity) => entity.Nine;
            string funcBodyString = propertyExpression.Body.ToString();
            Assert.False(new GreaterThanOrEqualValidator(propertyExpression.Compile().CoerceToNonGeneric(), funcBodyString)
                .Validate(new PropertyValidatorContext(this._testEntity, this._testEntity.Eight)).IsValid);
        }
    }
}
