using Xunit;
using System;
using System.Linq.Expressions;
using ValidationServices.Fluent.Internal;
using ValidationServices.Fluent.Validators;
using ValidationServices.Fluent.Validators.Comparison;
using ValidationServices.Fluent.UnitTests.TestEntities;

namespace ValidationServices.Fluent.UnitTests.Validators.Comparison
{
    public class LessThanOrEqualValidatorTests
    {
        private readonly ValidatorsTestEntity _testEntity;

        public LessThanOrEqualValidatorTests()
        {
            this._testEntity = new ValidatorsTestEntity();
        }

        [Fact]
        public void OnNotComparableValueThrowsArgumentExceptionTest()
        {
            Assert.Throws<ArgumentException>(() => new LessThanOrEqualValidator(8).Validate(
                new PropertyValidatorContext(this._testEntity, this._testEntity.NullObject)));
        }

        [Fact]
        public void GreaterValueIsInvalidTest()
        {
            Assert.False(new LessThanOrEqualValidator(9).Validate(
                new PropertyValidatorContext(this._testEntity, this._testEntity.Ten)).IsValid);
        }

        [Fact]
        public void EqualValueIsValidTest()
        {
            Assert.True(new LessThanOrEqualValidator(9).Validate(
                new PropertyValidatorContext(this._testEntity, this._testEntity.Nine)).IsValid);
        }

        [Fact]
        public void LesserValueIsValidTest()
        {
            Assert.True(new LessThanOrEqualValidator(9).Validate(
                new PropertyValidatorContext(this._testEntity, this._testEntity.Eight)).IsValid);
        }

        [Fact]
        public void GreaterValueWithLambdaIsInvalidTest()
        {
            Expression<Func<ValidatorsTestEntity, object>> propertyExpression = (entity) => entity.Nine;
            string funcBodyString = propertyExpression.Body.ToString();
            Assert.False(new LessThanOrEqualValidator(propertyExpression.Compile().CoerceToNonGeneric(), funcBodyString)
                .Validate(new PropertyValidatorContext(this._testEntity, this._testEntity.Ten)).IsValid);
        }

        [Fact]
        public void EqualValueWithLambdaIsValidTest()
        {
            Expression<Func<ValidatorsTestEntity, object>> propertyExpression = (entity) => entity.Nine;
            string funcBodyString = propertyExpression.Body.ToString();
            Assert.True(new LessThanOrEqualValidator(propertyExpression.Compile().CoerceToNonGeneric(), funcBodyString)
                .Validate(new PropertyValidatorContext(this._testEntity, this._testEntity.Nine)).IsValid);
        }

        [Fact]
        public void LesserValueWithLambdaIsValidTest()
        {
            Expression<Func<ValidatorsTestEntity, object>> propertyExpression = (entity) => entity.Nine;
            string funcBodyString = propertyExpression.Body.ToString();
            Assert.True(new LessThanOrEqualValidator(propertyExpression.Compile().CoerceToNonGeneric(), funcBodyString)
                .Validate(new PropertyValidatorContext(this._testEntity, this._testEntity.Eight)).IsValid);
        }
    }
}
