using Xunit;
using ValidationService.Attributes;
using ValidationServiceTests.TestEntities;

namespace ValidationServiceTests {
    public class RequiredPropertyAttributeTest {
        private readonly RequiredPropertyFoobar obj = new RequiredPropertyFoobar();

        [Fact]
        public void NotNullObjectIsValid() {
            RequiredPropertyAttribute attr = AttributeGetter.GetRequiredPropertyAttribute(nameof(this.obj.Ok1));
            Assert.True(attr.Validate(this.obj.Ok1).IsValid);
        }

        [Fact]
        public void NullObjectIsInvalid() {
            RequiredPropertyAttribute attr = AttributeGetter.GetRequiredPropertyAttribute(nameof(this.obj.NotOk1));
            Assert.False(attr.Validate(this.obj.NotOk1).IsValid);
        }

        [Fact]
        public void InvalidObjectHasCorrespondingFailureMessage() {
            RequiredPropertyAttribute attr = AttributeGetter.GetRequiredPropertyAttribute(nameof(this.obj.NotOk1));
            Assert.Equal(attr.FailureMessage, attr.Validate(this.obj.NotOk1).Details);
        }

        [Fact]
        public void EmptyStringsAreOkIfAllowed() {
            RequiredPropertyAttribute attr = AttributeGetter.GetRequiredPropertyAttribute(nameof(this.obj.Ok2));
            Assert.True(attr.Validate(this.obj.Ok2).IsValid);
        }

        [Fact]
        public void EmptyStringsAreNotOkByDefault() {
            RequiredPropertyAttribute attr = AttributeGetter.GetRequiredPropertyAttribute(nameof(this.obj.NotOk2));
            Assert.False(attr.Validate(this.obj.NotOk2).IsValid);
        }

        [Fact]
        public void WhiteSpaceStringsStringsAreOkIfAllowed() {
            RequiredPropertyAttribute attr = AttributeGetter.GetRequiredPropertyAttribute(nameof(this.obj.Ok3));
            Assert.True(attr.Validate(this.obj.Ok3).IsValid);
        }

        [Fact]
        public void WhiteSpaceStringsAreNotOkByDefault() {
            RequiredPropertyAttribute attr = AttributeGetter.GetRequiredPropertyAttribute(nameof(this.obj.NotOk3));
            Assert.False(attr.Validate(this.obj.NotOk3).IsValid);
        }

        [Fact]
        public void NotNullStringIsValid() {
            RequiredPropertyAttribute attr = AttributeGetter.GetRequiredPropertyAttribute(nameof(this.obj.Ok4));
            Assert.True(attr.Validate(this.obj.Ok4).IsValid);
        }

        [Fact]
        public void NullStringIsInvalid() {
            RequiredPropertyAttribute attr = AttributeGetter.GetRequiredPropertyAttribute(nameof(this.obj.NotOk4));
            Assert.False(attr.Validate(this.obj.NotOk4).IsValid);
        }
    }
}
