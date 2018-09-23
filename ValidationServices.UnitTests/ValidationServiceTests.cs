using Xunit;
using System;
using System.Collections.Generic;
using ValidationServices.Service;
using ValidationServices.Attributes;
using ValidationServices.UnitTests.TestEntities;

namespace ValidationServices.UnitTests
{
    public class ValidationServiceTests
    {
        [Fact]
        public void ValidObjectIsValidWithNonRecursiveValidationTest()
        {
            var testEntity = new ValidationServiceTestEntity(
                digit: 1, negativeInteger: -5, oneCharString: "a",
                requiredObject: new List<int>(), notEmptyString: "string", someObject: null);

            var service = new ValidationService(false);
            Assert.True(service.Validate(testEntity, nameof(testEntity)).IsValid);
        }

        [Fact]
        public void ValidObjectIsValidWithRecursiveValidationTest()
        {
            var testEntity = new ValidationServiceTestEntity(
                digit: 1, negativeInteger: -5, oneCharString: "a",
                requiredObject: new List<int>(), notEmptyString: "string", someObject: null);
            testEntity.SomeObject = new ValidationServiceTestEntity(
                digit: 2, negativeInteger: -17, oneCharString: "b",
                requiredObject: new List<int>(), notEmptyString: "str", someObject: null);

            var service = new ValidationService(true);
            Assert.True(service.Validate(testEntity, nameof(testEntity)).IsValid);
        }

        [Fact]
        public void ValidObjectWithRecursiveReferenceIsValidTest()
        {
            var testEntity = new ValidationServiceTestEntity(
               digit: 1, negativeInteger: -5, oneCharString: "a",
               requiredObject: null, notEmptyString: "string", someObject: null);
            testEntity.RequiredObject = testEntity;

            var service = new ValidationService(true);
            Assert.True(service.Validate(testEntity, nameof(testEntity)).IsValid);
        }

        [Fact]
        public void InvalidObjectIsInvalidWithNonRecursiveValidationTest()
        {
            var testEntity = new ValidationServiceTestEntity(
                digit: 25, negativeInteger: 5, oneCharString: "abc",
                requiredObject: null, notEmptyString: null, someObject: null);

            var service = new ValidationService(false);
            Assert.False(service.Validate(testEntity, nameof(testEntity)).IsValid);
        }

        [Fact]
        public void InvalidObjectIsInvalidWithRecursiveValidationTest()
        {
            var testEntity = new ValidationServiceTestEntity(
               digit: 1, negativeInteger: -5, oneCharString: "a",
               requiredObject: null, notEmptyString: "  ", someObject: null);
            testEntity.RequiredObject = new ValidationServiceTestEntity(
                digit: 25, negativeInteger: 5, oneCharString: "abc",
                requiredObject: null, notEmptyString: null, someObject: null);

            var service = new ValidationService(true);
            Assert.False(service.Validate(testEntity, nameof(testEntity)).IsValid);
        }

        [Fact]
        public void ValidationConclusionHasAllDetailsWithNonRecursiveValidationTest()
        {
            const int ENTRIES_EXPECTED = 5;

            var testEntity = new ValidationServiceTestEntity(
                digit: 25, negativeInteger: 5, oneCharString: "abc",
                requiredObject: null, notEmptyString: "  ", someObject: null);

            var service = new ValidationService(false);
            Assert.Equal(ENTRIES_EXPECTED, service.Validate(testEntity, nameof(testEntity)).Details.Count);
        }

        [Fact]
        public void ValidationConclusionHasAllDetailsWithRecursiveValidationTest()
        {
            const int ENTRIES_EXPECTED = 10;

            var testEntity = new ValidationServiceTestEntity(
                digit: 25, negativeInteger: 5, oneCharString: "abc",
                requiredObject: null, notEmptyString: null, someObject: null);
            testEntity.SomeObject = new ValidationServiceTestEntity(
                digit: -23, negativeInteger: 0, oneCharString: "",
                requiredObject: null, notEmptyString: "  ", someObject: null);

            var service = new ValidationService(true);
            Assert.Equal(ENTRIES_EXPECTED, service.Validate(testEntity, nameof(testEntity)).Details.Count);
        }

        [Fact]
        public void OnNullThrowsArgumentNullExceptionTest()
        {
            var service = new ValidationService(true);
            Assert.Throws<ArgumentNullException>(() => service.Validate<ValidationServiceTestEntity>(null).IsValid);
        }

        [Fact]
        public void ObjectWithoutValidationAttributesIsValidTest()
        {
            var service = new ValidationService(true);
            Assert.True(service.Validate(new SortedSet<int>()).IsValid);
        }

        [Fact]
        public void NullRootNameMeansEmptyRootNameTest()
        {
            var testEntity = new ValidationServiceTestEntity(
                digit: 25, negativeInteger: 5, oneCharString: "abc",
                requiredObject: null, notEmptyString: null, someObject: null);

            string rangeConstraintDefaultFailureMessage = new RangeConstraintAttribute().FailureMessage;
            var service = new ValidationService(false);
            Assert.Equal($".Digit: {rangeConstraintDefaultFailureMessage}",
                service.Validate(testEntity, null).Details[0]);
        }
    }
}
