using System;
using System.Reflection;
using Xunit;
using ValidationService.Attributes;
using ValidationServiceTests.TestEntities;

namespace ValidationServiceTests {
    public class RangeConstraintAttributeTest {
        private readonly RangeConstraintTestEntity obj = new RangeConstraintTestEntity();

        [Fact]
        public void NullObjectIsValid() {
            RangeConstraintAttribute attr = GetRangeConstraintAttribute(nameof(this.obj.Ok1));
            Assert.True(attr.Validate(this.obj.Ok1).IsValid);
        }

        [Fact]
        public void MinMaxConstraintedObjectIsOk() {
            RangeConstraintAttribute attr = GetRangeConstraintAttribute(nameof(this.obj.Ok2));
            Assert.True(attr.Validate(this.obj.Ok2).IsValid);
        }

        [Fact]
        public void MinMaxLowerConstraintViolationIsNotOk() {
            RangeConstraintAttribute attr = GetRangeConstraintAttribute(nameof(this.obj.SmallNotOk2));
            Assert.False(attr.Validate(this.obj.SmallNotOk2).IsValid);
        }

        [Fact]
        public void MinMaxUpperConstraintViolationIsNotOk() {
            RangeConstraintAttribute attr = GetRangeConstraintAttribute(nameof(this.obj.LargeNotOk2));
            Assert.False(attr.Validate(this.obj.LargeNotOk2).IsValid);
        }

        [Fact]
        public void InvalidObjectHasCorrespondingFailureMessage() {
            RangeConstraintAttribute attr = GetRangeConstraintAttribute(nameof(this.obj.SmallNotOk2));
            Assert.Equal(attr.FailureMessage, attr.Validate(this.obj.SmallNotOk2).Details);
        }

        [Fact]
        public void MaxConstraintedObjectIsOk() {
            RangeConstraintAttribute attr = GetRangeConstraintAttribute(nameof(this.obj.Ok3));
            Assert.True(attr.Validate(this.obj.Ok3).IsValid);
        }

        [Fact]
        public void MaxConstraintViolationIsNotOk() {
            RangeConstraintAttribute attr = GetRangeConstraintAttribute(nameof(this.obj.NotOk3));
            Assert.False(attr.Validate(this.obj.NotOk3).IsValid);
        }

        [Fact]
        public void MinConstraintedObjectIsOk() {
            RangeConstraintAttribute attr = GetRangeConstraintAttribute(nameof(this.obj.Ok4));
            Assert.True(attr.Validate(this.obj.Ok4).IsValid);
        }

        [Fact]
        public void MinConstraintViolationIsNotOk() {
            RangeConstraintAttribute attr = GetRangeConstraintAttribute(nameof(this.obj.NotOk4));
            Assert.False(attr.Validate(this.obj.NotOk4).IsValid);
        }

        [Fact]
        public void AtLeastOneConstraintMustBeSpecified() {
            RangeConstraintAttribute attr = GetRangeConstraintAttribute(nameof(this.obj.NotOk5));
            Assert.Throws<ArgumentNullException>(() => { attr.Validate(this.obj.NotOk5); });
        }

        [Fact]
        public void LowerConstraintCanNotExceedUpperConstraint() {
            RangeConstraintAttribute attr = GetRangeConstraintAttribute(nameof(this.obj.NotOk6));
            Assert.Throws<ArgumentException>(() => { attr.Validate(this.obj.NotOk6); });
        }

        [Fact]
        public void ConstraintsMustBeCompatibleToEachOther() {
            RangeConstraintAttribute attr = GetRangeConstraintAttribute(nameof(this.obj.NotOk7));
            Assert.Throws<ArgumentException>(() => { attr.Validate(this.obj.NotOk7); });
        }

        [Fact]
        public void ConstraintsMustBeCompatibleToObject() {
            RangeConstraintAttribute attr = GetRangeConstraintAttribute(nameof(this.obj.NotOk8));
            Assert.Throws<ArgumentException>(() => { attr.Validate(this.obj.NotOk8); });
        }

        private static RangeConstraintAttribute GetRangeConstraintAttribute(string propName) {
            PropertyInfo info = typeof(RangeConstraintTestEntity).GetProperty(propName);

            return (RangeConstraintAttribute)info.GetCustomAttribute(typeof(RangeConstraintAttribute));
        }
    }
}
