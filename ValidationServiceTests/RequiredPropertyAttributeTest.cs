using System.Reflection;
using Xunit;
using ValidationService.Attributes;
using ValidationServiceTests.TestEntities;

namespace ValidationServiceTests
{
    public class RequiredPropertyAttributeTest
    {
        private readonly RequiredPropertyTestEntity obj = new RequiredPropertyTestEntity();

        [Fact]
        public void NotNullObjectIsValidTest()
        {
            RequiredPropertyAttribute attr = GetRequiredPropertyAttribute(nameof(this.obj.Ok1));
            Assert.True(attr.Validate(this.obj.Ok1).IsValid);
        }

        [Fact]
        public void NullObjectIsInvalidTest()
        {
            RequiredPropertyAttribute attr = GetRequiredPropertyAttribute(nameof(this.obj.NotOk1));
            Assert.False(attr.Validate(this.obj.NotOk1).IsValid);
        }

        [Fact]
        public void InvalidObjectHasCorrespondingFailureMessageTest()
        {
            RequiredPropertyAttribute attr = GetRequiredPropertyAttribute(nameof(this.obj.NotOk1));
            Assert.Equal(attr.FailureMessage, attr.Validate(this.obj.NotOk1).Details);
        }

        [Fact]
        public void EmptyStringIsValidIfAllowedTest()
        {
            RequiredPropertyAttribute attr = GetRequiredPropertyAttribute(nameof(this.obj.Ok2));
            Assert.True(attr.Validate(this.obj.Ok2).IsValid);
        }

        [Fact]
        public void EmptyStringIsInvalidByDefaultTest()
        {
            RequiredPropertyAttribute attr = GetRequiredPropertyAttribute(nameof(this.obj.NotOk2));
            Assert.False(attr.Validate(this.obj.NotOk2).IsValid);
        }

        [Fact]
        public void WhiteSpaceStringIsValidIfAllowedTest()
        {
            RequiredPropertyAttribute attr = GetRequiredPropertyAttribute(nameof(this.obj.Ok3));
            Assert.True(attr.Validate(this.obj.Ok3).IsValid);
        }

        [Fact]
        public void WhiteSpaceStringIsInvalidByDefaultTest()
        {
            RequiredPropertyAttribute attr = GetRequiredPropertyAttribute(nameof(this.obj.NotOk3));
            Assert.False(attr.Validate(this.obj.NotOk3).IsValid);
        }

        [Fact]
        public void NotNullStringIsValidTest()
        {
            RequiredPropertyAttribute attr = GetRequiredPropertyAttribute(nameof(this.obj.Ok4));
            Assert.True(attr.Validate(this.obj.Ok4).IsValid);
        }

        [Fact]
        public void NullStringIsValidTest()
        {
            RequiredPropertyAttribute attr = GetRequiredPropertyAttribute(nameof(this.obj.NotOk4));
            Assert.False(attr.Validate(this.obj.NotOk4).IsValid);
        }

        private static RequiredPropertyAttribute GetRequiredPropertyAttribute(string propName)
        {
            PropertyInfo info = typeof(RequiredPropertyTestEntity).GetProperty(propName);

            return (RequiredPropertyAttribute)info.GetCustomAttribute(typeof(RequiredPropertyAttribute));
        }
    }
}
