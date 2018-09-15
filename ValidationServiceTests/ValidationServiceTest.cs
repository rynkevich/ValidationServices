using System.Collections.Generic;
using Xunit;
using ValidationService.Attributes;
using ValidationServiceTests.TestEntities;
using System;

namespace ValidationServiceTests
{
    public class ValidationServiceTest
    {
        [Fact]
        public void ValidObjectIsValidWithNonRecursiveValidationTest()
        {
            ValidationServiceTestEntity obj = new ValidationServiceTestEntity(
                digit: 1, negativeInteger: -5, oneCharString: "a",
                requiredObject: new List<int>(), notEmptyString: "string", someObject: null);

            ValidationService.Service.ValidationService service = new ValidationService.Service.ValidationService(false);
            Assert.True(service.Validate(obj, nameof(obj)).IsValid);
        }

        [Fact]
        public void ValidObjectIsValidWithRecursiveValidationTest()
        {
            ValidationServiceTestEntity obj = new ValidationServiceTestEntity(
                digit: 1, negativeInteger: -5, oneCharString: "a",
                requiredObject: new List<int>(), notEmptyString: "string", someObject: null);
            obj.SomeObject = new ValidationServiceTestEntity(
                digit: 2, negativeInteger: -17, oneCharString: "b",
                requiredObject: new List<int>(), notEmptyString: "str", someObject: null);

            ValidationService.Service.ValidationService service = new ValidationService.Service.ValidationService(true);
            Assert.True(service.Validate(obj, nameof(obj)).IsValid);
        }

        [Fact]
        public void ValidObjectWithRecursiveReferenceIsValidTest()
        {
            ValidationServiceTestEntity obj = new ValidationServiceTestEntity(
               digit: 1, negativeInteger: -5, oneCharString: "a",
               requiredObject: null, notEmptyString: "string", someObject: null);
            obj.RequiredObject = obj;

            ValidationService.Service.ValidationService service = new ValidationService.Service.ValidationService(true);
            Assert.True(service.Validate(obj, nameof(obj)).IsValid);
        }

        [Fact]
        public void InvalidObjectIsInvalidWithNonRecursiveValidationTest()
        {
            ValidationServiceTestEntity obj = new ValidationServiceTestEntity(
                digit: 25, negativeInteger: 5, oneCharString: "abc",
                requiredObject: null, notEmptyString: null, someObject: null);

            ValidationService.Service.ValidationService service = new ValidationService.Service.ValidationService(false);
            Assert.False(service.Validate(obj, nameof(obj)).IsValid);
        }

        [Fact]
        public void InvalidObjectIsInvalidWithRecursiveValidationTest()
        {
            ValidationServiceTestEntity obj = new ValidationServiceTestEntity(
               digit: 1, negativeInteger: -5, oneCharString: "a",
               requiredObject: null, notEmptyString: "  ", someObject: null);
            obj.RequiredObject = new ValidationServiceTestEntity(
                digit: 25, negativeInteger: 5, oneCharString: "abc",
                requiredObject: null, notEmptyString: null, someObject: null);

            ValidationService.Service.ValidationService service = new ValidationService.Service.ValidationService(true);
            Assert.False(service.Validate(obj, nameof(obj)).IsValid);
        }

        [Fact]
        public void ValidationConclusionHasAllDetailsWithNonRecursiveValidationTest()
        {
            const int ENTRIES_EXPECTED = 5;

            ValidationServiceTestEntity obj = new ValidationServiceTestEntity(
                digit: 25, negativeInteger: 5, oneCharString: "abc",
                requiredObject: null, notEmptyString: "  ", someObject: null);

            ValidationService.Service.ValidationService service = new ValidationService.Service.ValidationService(false);
            Assert.Equal(ENTRIES_EXPECTED, service.Validate(obj, nameof(obj)).Details.Count);
        }

        [Fact]
        public void ValidationConclusionHasAllDetailsWithRecursiveValidationTest()
        {
            const int ENTRIES_EXPECTED = 10;

            ValidationServiceTestEntity obj = new ValidationServiceTestEntity(
                digit: 25, negativeInteger: 5, oneCharString: "abc",
                requiredObject: null, notEmptyString: null, someObject: null);
            obj.SomeObject = new ValidationServiceTestEntity(
                digit: -23, negativeInteger: 0, oneCharString: "",
                requiredObject: null, notEmptyString: "  ", someObject: null);

            ValidationService.Service.ValidationService service = new ValidationService.Service.ValidationService(true);
            Assert.Equal(ENTRIES_EXPECTED, service.Validate(obj, nameof(obj)).Details.Count);
        }

        [Fact]
        public void OnNullThrowsArgumentNullExceptionTest()
        {
            ValidationService.Service.ValidationService service = new ValidationService.Service.ValidationService(true);
            Assert.Throws<ArgumentNullException>(() => service.Validate<ValidationServiceTestEntity>(null).IsValid);
        }

        [Fact]
        public void ObjectWithoutValidationAttributesIsValidTest()
        {
            ValidationService.Service.ValidationService service = new ValidationService.Service.ValidationService(true);
            Assert.True(service.Validate(new SortedSet<int>()).IsValid);
        }

        [Fact]
        public void NullRootNameMeansEmptyRootNameTest()
        {
            ValidationServiceTestEntity obj = new ValidationServiceTestEntity(
                digit: 25, negativeInteger: 5, oneCharString: "abc",
                requiredObject: null, notEmptyString: null, someObject: null);

            string rangeConstraintDefaultFailureMessage = new RangeConstraintAttribute().FailureMessage;
            ValidationService.Service.ValidationService service = new ValidationService.Service.ValidationService(false);
            Assert.Equal($".Digit: {rangeConstraintDefaultFailureMessage}",
                service.Validate(obj, null).Details[0]);
        }
    }
}
