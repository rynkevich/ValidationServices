using System.Reflection;
using ValidationService.Attributes;
using ValidationServiceTests.TestEntities;

namespace ValidationServiceTests {
    public static class AttributeGetter {
        public static RequiredPropertyAttribute GetRequiredPropertyAttribute(string propName) {
            PropertyInfo info = typeof(RequiredPropertyFoobar).GetProperty(propName);

            return (RequiredPropertyAttribute)info.GetCustomAttribute(typeof(RequiredPropertyAttribute));
        }
    }
}
