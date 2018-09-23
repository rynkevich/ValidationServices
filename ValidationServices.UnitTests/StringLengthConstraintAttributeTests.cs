using Xunit;
using System;
using System.Reflection;
using ValidationServices.Attributes;
using ValidationServices.UnitTests.TestEntities;

namespace ValidationServices.UnitTests
{
    public class StringLengthConstraintAttributeTests
    {
        private readonly StringLengthConstraintTestEntity _testEntity = new StringLengthConstraintTestEntity();

        [Fact]
        public void OnNotStringThrowsArgumentExceptionTest()
        {
            var attr = GetStringLengthConstraintAttribute(nameof(this._testEntity.NotOk5));
            Assert.Throws<ArgumentException>(() => { attr.Validate(this._testEntity.NotOk5); });
        }

        [Fact]
        public void OnLowerConstraintExceedsUpperConstraintThrowsArgumentExceptionTest()
        {
            var attr = GetStringLengthConstraintAttribute(nameof(this._testEntity.NotOk6));
            Assert.Throws<ArgumentException>(() => { attr.Validate(this._testEntity.NotOk6); });
        }

        [Fact]
        public void OnNoConstraintsThrowsArgumentExceptionTest()
        {
            var attr = GetStringLengthConstraintAttribute(nameof(this._testEntity.NotOk7));
            Assert.Throws<ArgumentException>(() => { attr.Validate(this._testEntity.NotOk7); });
        }

        [Fact]
        public void NullStringIsValidTest()
        {
            var attr = GetStringLengthConstraintAttribute(nameof(this._testEntity.Ok1));
            Assert.True(attr.Validate(this._testEntity.Ok1).IsValid);
        }

        [Fact]
        public void MinMaxConstraintedStringIsValidTest()
        {
            var attr = GetStringLengthConstraintAttribute(nameof(this._testEntity.Ok2));
            Assert.True(attr.Validate(this._testEntity.Ok2).IsValid);
        }

        [Fact]
        public void MinMaxLowerConstraintViolationIsInvalidTest()
        {
            var attr = GetStringLengthConstraintAttribute(nameof(this._testEntity.ShortNotOk2));
            Assert.False(attr.Validate(this._testEntity.ShortNotOk2).IsValid);
        }

        [Fact]
        public void MinMaxUpperConstraintViolationIsInvalidTest()
        {
            var attr = GetStringLengthConstraintAttribute(nameof(this._testEntity.LongNotOk2));
            Assert.False(attr.Validate(this._testEntity.LongNotOk2).IsValid);
        }

        [Fact]
        public void InvalidStringHasCorrespondingFailureMessageTest()
        {
            var attr = GetStringLengthConstraintAttribute(nameof(this._testEntity.ShortNotOk2));
            Assert.Equal(attr.FailureMessage, attr.Validate(this._testEntity.ShortNotOk2).Details);
        }

        [Fact]
        public void MaxConstraintedStringIsValidTest()
        {
            var attr = GetStringLengthConstraintAttribute(nameof(this._testEntity.Ok3));
            Assert.True(attr.Validate(this._testEntity.Ok3).IsValid);
        }

        [Fact]
        public void MaxConstraintViolationIsInvalidTest()
        {
            var attr = GetStringLengthConstraintAttribute(nameof(this._testEntity.NotOk3));
            Assert.False(attr.Validate(this._testEntity.NotOk3).IsValid);
        }

        [Fact]
        public void MinConstraintedStringIsValidTest()
        {
            var attr = GetStringLengthConstraintAttribute(nameof(this._testEntity.Ok4));
            Assert.True(attr.Validate(this._testEntity.Ok4).IsValid);
        }

        [Fact]
        public void MinConstraintViolationIsInvalidTest()
        {
            var attr = GetStringLengthConstraintAttribute(nameof(this._testEntity.NotOk4));
            Assert.False(attr.Validate(this._testEntity.NotOk4).IsValid);
        }

        private static StringLengthConstraintAttribute GetStringLengthConstraintAttribute(string propName)
        {
            var info = typeof(StringLengthConstraintTestEntity).GetProperty(propName);

            return (StringLengthConstraintAttribute)info.GetCustomAttribute(typeof(StringLengthConstraintAttribute));
        }
    }
}
