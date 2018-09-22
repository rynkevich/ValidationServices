using System;
using System.Reflection;
using Xunit;
using ValidationServices.Attributes;
using ValidationServices.UnitTests.TestEntities;

namespace ValidationServices.UnitTests
{
    public class StringLengthConstraintAttributeTest
    {
        private readonly StringLengthConstraintTestEntity obj = new StringLengthConstraintTestEntity();

        [Fact]
        public void OnNotStringThrowsArgumentExceptionTest()
        {
            StringLengthConstraintAttribute attr = GetStringLengthConstraintAttribute(nameof(this.obj.NotOk5));
            Assert.Throws<ArgumentException>(() => { attr.Validate(this.obj.NotOk5); });
        }

        [Fact]
        public void OnLowerConstraintExceedsUpperConstraintThrowsArgumentExceptionTest()
        {
            StringLengthConstraintAttribute attr = GetStringLengthConstraintAttribute(nameof(this.obj.NotOk6));
            Assert.Throws<ArgumentException>(() => { attr.Validate(this.obj.NotOk6); });
        }

        [Fact]
        public void OnNoConstraintsThrowsArgumentExceptionTest()
        {
            StringLengthConstraintAttribute attr = GetStringLengthConstraintAttribute(nameof(this.obj.NotOk7));
            Assert.Throws<ArgumentException>(() => { attr.Validate(this.obj.NotOk7); });
        }

        [Fact]
        public void NullStringIsValidTest()
        {
            StringLengthConstraintAttribute attr = GetStringLengthConstraintAttribute(nameof(this.obj.Ok1));
            Assert.True(attr.Validate(this.obj.Ok1).IsValid);
        }

        [Fact]
        public void MinMaxConstraintedStringIsValidTest()
        {
            StringLengthConstraintAttribute attr = GetStringLengthConstraintAttribute(nameof(this.obj.Ok2));
            Assert.True(attr.Validate(this.obj.Ok2).IsValid);
        }

        [Fact]
        public void MinMaxLowerConstraintViolationIsInvalidTest()
        {
            StringLengthConstraintAttribute attr = GetStringLengthConstraintAttribute(nameof(this.obj.ShortNotOk2));
            Assert.False(attr.Validate(this.obj.ShortNotOk2).IsValid);
        }

        [Fact]
        public void MinMaxUpperConstraintViolationIsInvalidTest()
        {
            StringLengthConstraintAttribute attr = GetStringLengthConstraintAttribute(nameof(this.obj.LongNotOk2));
            Assert.False(attr.Validate(this.obj.LongNotOk2).IsValid);
        }

        [Fact]
        public void InvalidStringHasCorrespondingFailureMessageTest()
        {
            StringLengthConstraintAttribute attr = GetStringLengthConstraintAttribute(nameof(this.obj.ShortNotOk2));
            Assert.Equal(attr.FailureMessage, attr.Validate(this.obj.ShortNotOk2).Details);
        }

        [Fact]
        public void MaxConstraintedStringIsValidTest()
        {
            StringLengthConstraintAttribute attr = GetStringLengthConstraintAttribute(nameof(this.obj.Ok3));
            Assert.True(attr.Validate(this.obj.Ok3).IsValid);
        }

        [Fact]
        public void MaxConstraintViolationIsInvalidTest()
        {
            StringLengthConstraintAttribute attr = GetStringLengthConstraintAttribute(nameof(this.obj.NotOk3));
            Assert.False(attr.Validate(this.obj.NotOk3).IsValid);
        }

        [Fact]
        public void MinConstraintedStringIsValidTest()
        {
            StringLengthConstraintAttribute attr = GetStringLengthConstraintAttribute(nameof(this.obj.Ok4));
            Assert.True(attr.Validate(this.obj.Ok4).IsValid);
        }

        [Fact]
        public void MinConstraintViolationIsInvalidTest()
        {
            StringLengthConstraintAttribute attr = GetStringLengthConstraintAttribute(nameof(this.obj.NotOk4));
            Assert.False(attr.Validate(this.obj.NotOk4).IsValid);
        }

        private static StringLengthConstraintAttribute GetStringLengthConstraintAttribute(string propName)
        {
            PropertyInfo info = typeof(StringLengthConstraintTestEntity).GetProperty(propName);

            return (StringLengthConstraintAttribute)info.GetCustomAttribute(typeof(StringLengthConstraintAttribute));
        }
    }
}
