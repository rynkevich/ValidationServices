using Xunit;
using System.Reflection;
using ValidationServices.Attributes;
using ValidationServices.UnitTests.TestEntities;

namespace ValidationServices.UnitTests
{
    public class RequiredPropertyAttributeTests
    {
        private readonly RequiredPropertyTestEntity _testEntity = new RequiredPropertyTestEntity();

        [Fact]
        public void NotNullObjectIsValidTest()
        {
            var attr = GetRequiredPropertyAttribute(nameof(this._testEntity.Ok1));
            Assert.True(attr.Validate(this._testEntity.Ok1).IsValid);
        }

        [Fact]
        public void NullObjectIsInvalidTest()
        {
            var attr = GetRequiredPropertyAttribute(nameof(this._testEntity.NotOk1));
            Assert.False(attr.Validate(this._testEntity.NotOk1).IsValid);
        }

        [Fact]
        public void InvalidObjectHasCorrespondingFailureMessageTest()
        {
            var attr = GetRequiredPropertyAttribute(nameof(this._testEntity.NotOk1));
            Assert.Equal(attr.FailureMessage, attr.Validate(this._testEntity.NotOk1).Details);
        }

        [Fact]
        public void EmptyStringIsValidIfAllowedTest()
        {
            var attr = GetRequiredPropertyAttribute(nameof(this._testEntity.Ok2));
            Assert.True(attr.Validate(this._testEntity.Ok2).IsValid);
        }

        [Fact]
        public void EmptyStringIsInvalidByDefaultTest()
        {
            var attr = GetRequiredPropertyAttribute(nameof(this._testEntity.NotOk2));
            Assert.False(attr.Validate(this._testEntity.NotOk2).IsValid);
        }

        [Fact]
        public void WhiteSpaceStringIsValidIfAllowedTest()
        {
            var attr = GetRequiredPropertyAttribute(nameof(this._testEntity.Ok3));
            Assert.True(attr.Validate(this._testEntity.Ok3).IsValid);
        }

        [Fact]
        public void WhiteSpaceStringIsInvalidByDefaultTest()
        {
            var attr = GetRequiredPropertyAttribute(nameof(this._testEntity.NotOk3));
            Assert.False(attr.Validate(this._testEntity.NotOk3).IsValid);
        }

        [Fact]
        public void NotNullStringIsValidTest()
        {
            var attr = GetRequiredPropertyAttribute(nameof(this._testEntity.Ok4));
            Assert.True(attr.Validate(this._testEntity.Ok4).IsValid);
        }

        [Fact]
        public void NullStringIsValidTest()
        {
            var attr = GetRequiredPropertyAttribute(nameof(this._testEntity.NotOk4));
            Assert.False(attr.Validate(this._testEntity.NotOk4).IsValid);
        }

        private static RequiredPropertyAttribute GetRequiredPropertyAttribute(string propName)
        {
            var info = typeof(RequiredPropertyTestEntity).GetProperty(propName);

            return (RequiredPropertyAttribute)info.GetCustomAttribute(typeof(RequiredPropertyAttribute));
        }
    }
}
