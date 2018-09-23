using Xunit;
using System;
using System.Reflection;
using ValidationServices.Attributes;
using ValidationServices.UnitTests.TestEntities;

namespace ValidationServices.UnitTests
{
    public class RangeConstraintAttributeTests
    {
        private readonly RangeConstraintTestEntity _testEntity = new RangeConstraintTestEntity();

        [Fact]
        public void OnNoConstraintsThrowsArgumentExceptionTest()
        {
            var attr = GetRangeConstraintAttribute(nameof(this._testEntity.NotOk5));
            Assert.Throws<ArgumentException>(() => { attr.Validate(this._testEntity.NotOk5); });
        }

        [Fact]
        public void OnLowerConstraintExceedsUpperConstraintThrowsArgumentExceptionTest()
        {
            var attr = GetRangeConstraintAttribute(nameof(this._testEntity.NotOk6));
            Assert.Throws<ArgumentException>(() => { attr.Validate(this._testEntity.NotOk6); });
        }

        [Fact]
        public void OnIncompatibleConstraintsThrowsArgumentException()
        {
            var attr = GetRangeConstraintAttribute(nameof(this._testEntity.NotOk7));
            Assert.Throws<ArgumentException>(() => { attr.Validate(this._testEntity.NotOk7); });
        }

        [Fact]
        public void OnIncompatibleConstraintsAndObjectThrowsArgumentException()
        {
            var attr = GetRangeConstraintAttribute(nameof(this._testEntity.NotOk8));
            Assert.Throws<ArgumentException>(() => { attr.Validate(this._testEntity.NotOk8); });
        }

        [Fact]
        public void NullObjectIsValidTest()
        {
            var attr = GetRangeConstraintAttribute(nameof(this._testEntity.Ok1));
            Assert.True(attr.Validate(this._testEntity.Ok1).IsValid);
        }

        [Fact]
        public void MinMaxConstraintedObjectIsValidTest()
        {
            var attr = GetRangeConstraintAttribute(nameof(this._testEntity.Ok2));
            Assert.True(attr.Validate(this._testEntity.Ok2).IsValid);
        }

        [Fact]
        public void MinMaxLowerConstraintViolationIsInvalidTest()
        {
            var attr = GetRangeConstraintAttribute(nameof(this._testEntity.SmallNotOk2));
            Assert.False(attr.Validate(this._testEntity.SmallNotOk2).IsValid);
        }

        [Fact]
        public void MinMaxUpperConstraintViolationIsInvalidTest()
        {
            var attr = GetRangeConstraintAttribute(nameof(this._testEntity.LargeNotOk2));
            Assert.False(attr.Validate(this._testEntity.LargeNotOk2).IsValid);
        }

        [Fact]
        public void InvalidObjectHasCorrespondingFailureMessageTest()
        {
            var attr = GetRangeConstraintAttribute(nameof(this._testEntity.SmallNotOk2));
            Assert.Equal(attr.FailureMessage, attr.Validate(this._testEntity.SmallNotOk2).Details);
        }

        [Fact]
        public void MaxConstraintedObjectIsValidTest()
        {
            var attr = GetRangeConstraintAttribute(nameof(this._testEntity.Ok3));
            Assert.True(attr.Validate(this._testEntity.Ok3).IsValid);
        }

        [Fact]
        public void MaxConstraintViolationIsInvalidTest()
        {
            var attr = GetRangeConstraintAttribute(nameof(this._testEntity.NotOk3));
            Assert.False(attr.Validate(this._testEntity.NotOk3).IsValid);
        }

        [Fact]
        public void MinConstraintedObjectIsValidTest()
        {
            var attr = GetRangeConstraintAttribute(nameof(this._testEntity.Ok4));
            Assert.True(attr.Validate(this._testEntity.Ok4).IsValid);
        }

        [Fact]
        public void MinConstraintViolationIsInvalidTest()
        {
            var attr = GetRangeConstraintAttribute(nameof(this._testEntity.NotOk4));
            Assert.False(attr.Validate(this._testEntity.NotOk4).IsValid);
        }

        private static RangeConstraintAttribute GetRangeConstraintAttribute(string propName)
        {
            var info = typeof(RangeConstraintTestEntity).GetProperty(propName);

            return (RangeConstraintAttribute)info.GetCustomAttribute(typeof(RangeConstraintAttribute));
        }
    }
}
