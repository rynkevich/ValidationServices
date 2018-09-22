using Xunit;
using ValidationServices.Fluent.Validators;
using ValidationServices.Fluent.Validators.Comparison;
using ValidationServices.Fluent.UnitTests.TestEntities.Validators;
using System;
using System.Collections.Generic;

namespace ValidationServices.Fluent.UnitTests.Validators.Comparison
{
    public class EqualValidatorTest
    {
        private readonly ComparisonValidatorsTestEntity testEntity;

        public EqualValidatorTest()
        {
            this.testEntity = new ComparisonValidatorsTestEntity();
        }

        [Fact]
        public void OnNullComparisonValueThrowsArgumentNullExceptionTest()
        {
            Assert.Throws<ArgumentNullException>(() => new EqualValidator(this.testEntity.NullString));
        }

        [Fact]
        public void OnNullComparisonValueFuncThrowsArgumentNullExceptionTest()
        {
            Func<object, object> comparisonValueFunc = null;
            Assert.Throws<ArgumentNullException>(() => new EqualValidator(comparisonValueFunc));
        }

        [Fact]
        public void OnNotNullComparisonValueCreatesInstanceOfValidatorTest()
        {
            Assert.NotNull(new EqualValidator(this.testEntity.FooString));
        }

        [Fact]
        public void OnNotNullComparisonValueFuncCreatesInstanceOfValidatorTest()
        {
            Func<ComparisonValidatorsTestEntity, int> comparisonValueFunc = (entity) => entity.Eight;
            Assert.NotNull(new EqualValidator(comparisonValueFunc));
        }

        [Fact]
        public void EqualComparablesAreValidTest()
        {
            Assert.True(new EqualValidator(8).Validate(
                new PropertyValidatorContext(this.testEntity, this.testEntity.Eight)).IsValid);
        }

        [Fact]
        public void NotEqualComparablesAreInvalidTest()
        {
            Assert.False(new EqualValidator(5).Validate(
                new PropertyValidatorContext(this.testEntity, this.testEntity.Eight)).IsValid);
        }

        [Fact]
        public void ComparerEqualValuesAreValidTest()
        {
            Assert.True(new EqualValidator(8, Comparer<int>.Create((x, y) => x - y)).Validate(
                new PropertyValidatorContext(this.testEntity, this.testEntity.Eight)).IsValid);
        }

        [Fact]
        public void NotComparerEqualValuesAreInvalidTest()
        {
            Assert.False(new EqualValidator(5, Comparer<int>.Create((x, y) => x - y)).Validate(
                new PropertyValidatorContext(this.testEntity, this.testEntity.Eight)).IsValid);
        }
        
        [Fact]
        public void ReferenceEqualValuesAreValidTest()
        {
            Assert.True(new EqualValidator(ComparisonValidatorsTestEntity.SomeObject).Validate(
                new PropertyValidatorContext(this.testEntity, this.testEntity.SameObject)).IsValid);
        }

        [Fact]
        public void NotReferenceEqualValuesAreInvalidTest()
        {
            Assert.False(new EqualValidator(ComparisonValidatorsTestEntity.SomeObject).Validate(
                new PropertyValidatorContext(this.testEntity, this.testEntity.Ten)).IsValid);
        }
    }
}
