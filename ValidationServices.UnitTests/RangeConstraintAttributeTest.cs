using System;
using System.Reflection;
using Xunit;
using ValidationServices.Attributes;
using ValidationServices.UnitTests.TestEntities;

namespace ValidationServices.UnitTests
{
    public class RangeConstraintAttributeTest
    {
        private readonly RangeConstraintTestEntity obj = new RangeConstraintTestEntity();

        [Fact]
        public void OnNoConstraintsThrowsArgumentExceptionTest()
        {
            RangeConstraintAttribute attr = GetRangeConstraintAttribute(nameof(this.obj.NotOk5));
            Assert.Throws<ArgumentException>(() => { attr.Validate(this.obj.NotOk5); });
        }

        [Fact]
        public void OnLowerConstraintExceedsUpperConstraintThrowsArgumentExceptionTest()
        {
            RangeConstraintAttribute attr = GetRangeConstraintAttribute(nameof(this.obj.NotOk6));
            Assert.Throws<ArgumentException>(() => { attr.Validate(this.obj.NotOk6); });
        }

        [Fact]
        public void OnIncompatibleConstraintsThrowsArgumentException()
        {
            RangeConstraintAttribute attr = GetRangeConstraintAttribute(nameof(this.obj.NotOk7));
            Assert.Throws<ArgumentException>(() => { attr.Validate(this.obj.NotOk7); });
        }

        [Fact]
        public void OnIncompatibleConstraintsAndObjectThrowsArgumentException()
        {
            RangeConstraintAttribute attr = GetRangeConstraintAttribute(nameof(this.obj.NotOk8));
            Assert.Throws<ArgumentException>(() => { attr.Validate(this.obj.NotOk8); });
        }

        [Fact]
        public void NullObjectIsValidTest()
        {
            RangeConstraintAttribute attr = GetRangeConstraintAttribute(nameof(this.obj.Ok1));
            Assert.True(attr.Validate(this.obj.Ok1).IsValid);
        }

        [Fact]
        public void MinMaxConstraintedObjectIsValidTest()
        {
            RangeConstraintAttribute attr = GetRangeConstraintAttribute(nameof(this.obj.Ok2));
            Assert.True(attr.Validate(this.obj.Ok2).IsValid);
        }

        [Fact]
        public void MinMaxLowerConstraintViolationIsInvalidTest()
        {
            RangeConstraintAttribute attr = GetRangeConstraintAttribute(nameof(this.obj.SmallNotOk2));
            Assert.False(attr.Validate(this.obj.SmallNotOk2).IsValid);
        }

        [Fact]
        public void MinMaxUpperConstraintViolationIsInvalidTest()
        {
            RangeConstraintAttribute attr = GetRangeConstraintAttribute(nameof(this.obj.LargeNotOk2));
            Assert.False(attr.Validate(this.obj.LargeNotOk2).IsValid);
        }

        [Fact]
        public void InvalidObjectHasCorrespondingFailureMessageTest()
        {
            RangeConstraintAttribute attr = GetRangeConstraintAttribute(nameof(this.obj.SmallNotOk2));
            Assert.Equal(attr.FailureMessage, attr.Validate(this.obj.SmallNotOk2).Details);
        }

        [Fact]
        public void MaxConstraintedObjectIsValidTest()
        {
            RangeConstraintAttribute attr = GetRangeConstraintAttribute(nameof(this.obj.Ok3));
            Assert.True(attr.Validate(this.obj.Ok3).IsValid);
        }

        [Fact]
        public void MaxConstraintViolationIsInvalidTest()
        {
            RangeConstraintAttribute attr = GetRangeConstraintAttribute(nameof(this.obj.NotOk3));
            Assert.False(attr.Validate(this.obj.NotOk3).IsValid);
        }

        [Fact]
        public void MinConstraintedObjectIsValidTest()
        {
            RangeConstraintAttribute attr = GetRangeConstraintAttribute(nameof(this.obj.Ok4));
            Assert.True(attr.Validate(this.obj.Ok4).IsValid);
        }

        [Fact]
        public void MinConstraintViolationIsInvalidTest()
        {
            RangeConstraintAttribute attr = GetRangeConstraintAttribute(nameof(this.obj.NotOk4));
            Assert.False(attr.Validate(this.obj.NotOk4).IsValid);
        }

        private static RangeConstraintAttribute GetRangeConstraintAttribute(string propName)
        {
            PropertyInfo info = typeof(RangeConstraintTestEntity).GetProperty(propName);

            return (RangeConstraintAttribute)info.GetCustomAttribute(typeof(RangeConstraintAttribute));
        }
    }
}
