using System.Reflection;
using Xunit;
using ValidationService.Attributes;
using ValidationServiceTests.TestEntities;

namespace ValidationServiceTests {
    public class RequiredPropertyAttributeTest {
        private readonly RequiredPropertyFoobar obj = new RequiredPropertyFoobar();

        [Fact]
        public void NotNullObjectIsValid() {
            RequiredPropertyAttribute attr = (RequiredPropertyAttribute)GetRequiredPropertyAttribute(nameof(this.obj.Ok1));
            Assert.True(attr.Validate(this.obj.Ok1).IsValid);
        }

        [Fact]
        public void NullObjectIsInvalid() {
            RequiredPropertyAttribute attr = (RequiredPropertyAttribute)GetRequiredPropertyAttribute(nameof(this.obj.NotOk1));
            Assert.False(attr.Validate(this.obj.NotOk1).IsValid);
        }

        [Fact]
        public void InvalidObjectHasCorrespondingFailureMessage() {
            RequiredPropertyAttribute attr = (RequiredPropertyAttribute)GetRequiredPropertyAttribute(nameof(this.obj.NotOk1));
            Assert.Equal(attr.FailureMessage, attr.Validate(this.obj.NotOk1).Details);
        }

        [Fact]
        public void EmptyStringIsOkIfAllowed() {
            RequiredPropertyAttribute attr = (RequiredPropertyAttribute)GetRequiredPropertyAttribute(nameof(this.obj.Ok2));
            Assert.True(attr.Validate(this.obj.Ok2).IsValid);
        }

        [Fact]
        public void EmptyStringIsNotOkByDefault() {
            RequiredPropertyAttribute attr = (RequiredPropertyAttribute)GetRequiredPropertyAttribute(nameof(this.obj.NotOk2));
            Assert.False(attr.Validate(this.obj.NotOk2).IsValid);
        }

        [Fact]
        public void WhiteSpaceStringIsOkIfAllowed() {
            RequiredPropertyAttribute attr = (RequiredPropertyAttribute)GetRequiredPropertyAttribute(nameof(this.obj.Ok3));
            Assert.True(attr.Validate(this.obj.Ok3).IsValid);
        }

        [Fact]
        public void WhiteSpaceStringIsNotOkByDefault() {
            RequiredPropertyAttribute attr = (RequiredPropertyAttribute)GetRequiredPropertyAttribute(nameof(this.obj.NotOk3));
            Assert.False(attr.Validate(this.obj.NotOk3).IsValid);
        }

        [Fact]
        public void NotNullStringIsValid() {
            RequiredPropertyAttribute attr = (RequiredPropertyAttribute)GetRequiredPropertyAttribute(nameof(this.obj.Ok4));
            Assert.True(attr.Validate(this.obj.Ok4).IsValid);
        }

        [Fact]
        public void NullStringIsInvalid() {
            RequiredPropertyAttribute attr = (RequiredPropertyAttribute)GetRequiredPropertyAttribute(nameof(this.obj.NotOk4));
            Assert.False(attr.Validate(this.obj.NotOk4).IsValid);
        }

        private static ValidationAttribute GetRequiredPropertyAttribute(string propName) {
            PropertyInfo info = typeof(RequiredPropertyFoobar).GetProperty(propName);

            return (RequiredPropertyAttribute)info.GetCustomAttribute(typeof(RequiredPropertyAttribute));
        }
    }
}
