using Xunit;
using System.Collections.Generic;
using ValidationServices.Fluent.Service;
using ValidationServices.Fluent.UnitTests.TestEntities;
using ValidationServices.Fluent.Validators.Absence;
using ValidationServices.Fluent.Validators.Comparison;
using ValidationServices.Fluent.Validators.Length;

namespace ValidationServices.Fluent.UnitTests.Service
{
    public class ExtensionsTest
    {
        private readonly ServiceTestEntity testEntity;

        public ExtensionsTest()
        {
            this.testEntity = new ServiceTestEntity();
        }

        [Fact]
        public void EmptyExtensionProvidesCorrectValidationTest()
        {
            ValidationServiceBuilder serviceBuilder = new ValidationServiceBuilder(false);
            serviceBuilder.RuleFor((ServiceTestEntity entity) => entity.EightCharString).Empty();
            serviceBuilder.RuleFor((ServiceTestEntity entity) => entity.WhitespaceString).Empty();

            List<string> expectedDetails = new List<string>() { new EmptyValidator(null).FailureMessage };
            Assert.Equal(expectedDetails, serviceBuilder.Build().Validate(this.testEntity).Details);
        }

        [Fact]
        public void NotEmptyExtensionProvidesCorrectValidationTest()
        {
            ValidationServiceBuilder serviceBuilder = new ValidationServiceBuilder(false);
            serviceBuilder.RuleFor((ServiceTestEntity entity) => entity.EightCharString).NotEmpty();
            serviceBuilder.RuleFor((ServiceTestEntity entity) => entity.WhitespaceString).NotEmpty();

            List<string> expectedDetails = new List<string>() { new NotEmptyValidator(null).FailureMessage };
            Assert.Equal(expectedDetails, serviceBuilder.Build().Validate(this.testEntity).Details);
        }

        [Fact]
        public void NullExtensionProvidesCorrectValidationTest()
        {
            ValidationServiceBuilder serviceBuilder = new ValidationServiceBuilder(false);
            serviceBuilder.RuleFor((ServiceTestEntity entity) => entity.EightCharString).Null();
            serviceBuilder.RuleFor((ServiceTestEntity entity) => entity.NullString).Null();

            List<string> expectedDetails = new List<string>() { new NullValidator().FailureMessage };
            Assert.Equal(expectedDetails, serviceBuilder.Build().Validate(this.testEntity).Details);
        }

        [Fact]
        public void NotNullExtensionProvidesCorrectValidationTest()
        {
            ValidationServiceBuilder serviceBuilder = new ValidationServiceBuilder(false);
            serviceBuilder.RuleFor((ServiceTestEntity entity) => entity.EightCharString).NotNull();
            serviceBuilder.RuleFor((ServiceTestEntity entity) => entity.NullString).NotNull();

            List<string> expectedDetails = new List<string>() { new NotNullValidator().FailureMessage };
            Assert.Equal(expectedDetails, serviceBuilder.Build().Validate(this.testEntity).Details);
        }

        [Fact]
        public void LengthExtensionProvidesCorrectValidationTest()
        {
            ValidationServiceBuilder serviceBuilder = new ValidationServiceBuilder(false);
            serviceBuilder.RuleFor((ServiceTestEntity entity) => entity.EightCharString).Length(0, 9);
            serviceBuilder.RuleFor((ServiceTestEntity entity) => entity.NullString).Length(12, 15);

            List<string> expectedDetails = new List<string>() { new StringLengthValidator(1, 2).FailureMessage };
            Assert.Equal(expectedDetails, serviceBuilder.Build().Validate(this.testEntity).Details);
        }

        [Fact]
        public void LengthWithLambdasExtensionProvidesCorrectValidationTest()
        {
            ValidationServiceBuilder serviceBuilder = new ValidationServiceBuilder(false);
            serviceBuilder.RuleFor((ServiceTestEntity entity) => entity.EightCharString).Length(
                entity => entity.Seven, entity => entity.Ten);
            serviceBuilder.RuleFor((ServiceTestEntity entity) => entity.NullString).Length(
                entity => entity.Nine, entity => entity.Ten);

            List<string> expectedDetails = new List<string>() { new StringLengthValidator(1, 2).FailureMessage };
            Assert.Equal(expectedDetails, serviceBuilder.Build().Validate(this.testEntity).Details);
        }

        [Fact]
        public void MaxLengthExtensionProvidesCorrectValidationTest()
        {
            ValidationServiceBuilder serviceBuilder = new ValidationServiceBuilder(false);
            serviceBuilder.RuleFor((ServiceTestEntity entity) => entity.EightCharString).MaxLength(9);
            serviceBuilder.RuleFor((ServiceTestEntity entity) => entity.NineCharString).MaxLength(7);

            List<string> expectedDetails = new List<string>() { new StringLengthValidator(1, 2).FailureMessage };
            Assert.Equal(expectedDetails, serviceBuilder.Build().Validate(this.testEntity).Details);
        }

        [Fact]
        public void MaxLengthWithLambdasExtensionProvidesCorrectValidationTest()
        {
            ValidationServiceBuilder serviceBuilder = new ValidationServiceBuilder(false);
            serviceBuilder.RuleFor((ServiceTestEntity entity) => entity.EightCharString).MaxLength(entity => entity.Ten);
            serviceBuilder.RuleFor((ServiceTestEntity entity) => entity.NineCharString).MaxLength(entity => entity.Seven);

            List<string> expectedDetails = new List<string>() { new StringLengthValidator(1, 2).FailureMessage };
            Assert.Equal(expectedDetails, serviceBuilder.Build().Validate(this.testEntity).Details);
        }

        [Fact]
        public void MinLengthExtensionProvidesCorrectValidationTest()
        {
            ValidationServiceBuilder serviceBuilder = new ValidationServiceBuilder(false);
            serviceBuilder.RuleFor((ServiceTestEntity entity) => entity.EightCharString).MinLength(7);
            serviceBuilder.RuleFor((ServiceTestEntity entity) => entity.NineCharString).MinLength(10);

            List<string> expectedDetails = new List<string>() { new StringLengthValidator(1, 2).FailureMessage };
            Assert.Equal(expectedDetails, serviceBuilder.Build().Validate(this.testEntity).Details);
        }

        [Fact]
        public void MinLengthWithLambdasExtensionProvidesCorrectValidationTest()
        {
            ValidationServiceBuilder serviceBuilder = new ValidationServiceBuilder(false);
            serviceBuilder.RuleFor((ServiceTestEntity entity) => entity.EightCharString).MinLength(entity => entity.Seven);
            serviceBuilder.RuleFor((ServiceTestEntity entity) => entity.NineCharString).MinLength(entity => entity.Ten);

            List<string> expectedDetails = new List<string>() { new StringLengthValidator(1, 2).FailureMessage };
            Assert.Equal(expectedDetails, serviceBuilder.Build().Validate(this.testEntity).Details);
        }

        [Fact]
        public void ExactLengthExtensionProvidesCorrectValidationTest()
        {
            ValidationServiceBuilder serviceBuilder = new ValidationServiceBuilder(false);
            serviceBuilder.RuleFor((ServiceTestEntity entity) => entity.EightCharString).Length(8);
            serviceBuilder.RuleFor((ServiceTestEntity entity) => entity.NineCharString).Length(10);

            List<string> expectedDetails = new List<string>() { new StringLengthValidator(1, 2).FailureMessage };
            Assert.Equal(expectedDetails, serviceBuilder.Build().Validate(this.testEntity).Details);
        }

        [Fact]
        public void ExactLengthWithLambdasExtensionProvidesCorrectValidationTest()
        {
            ValidationServiceBuilder serviceBuilder = new ValidationServiceBuilder(false);
            serviceBuilder.RuleFor((ServiceTestEntity entity) => entity.EightCharString).Length(entity => entity.Eight);
            serviceBuilder.RuleFor((ServiceTestEntity entity) => entity.NineCharString).Length(entity => entity.Ten);

            List<string> expectedDetails = new List<string>() { new StringLengthValidator(1, 2).FailureMessage };
            Assert.Equal(expectedDetails, serviceBuilder.Build().Validate(this.testEntity).Details);
        }

        [Fact]
        public void EqualExtensionProvidesCorrectValidationTest()
        {
            ValidationServiceBuilder serviceBuilder = new ValidationServiceBuilder(false);
            serviceBuilder.RuleFor((ServiceTestEntity entity) => entity.Ten).Equal(10);
            serviceBuilder.RuleFor((ServiceTestEntity entity) => entity.Nine).Equal(5);

            List<string> expectedDetails = new List<string>() { EqualValidator.DefaultFailureMessage + 5 };
            Assert.Equal(expectedDetails, serviceBuilder.Build().Validate(this.testEntity).Details);
        }

        [Fact]
        public void EqualWithLambdasExtensionProvidesCorrectValidationTest()
        {
            ValidationServiceBuilder serviceBuilder = new ValidationServiceBuilder(false);
            serviceBuilder.RuleFor((ServiceTestEntity entity) => entity.Ten).Equal(entity => entity.AnotherTen);
            serviceBuilder.RuleFor((ServiceTestEntity entity) => entity.Nine).Equal(entity => entity.Ten);

            List<string> expectedDetails = new List<string>() { EqualValidator.DefaultFailureMessage + "entity.Ten" };
            Assert.Equal(expectedDetails, serviceBuilder.Build().Validate(this.testEntity).Details);
        }

        [Fact]
        public void NotEqualExtensionProvidesCorrectValidationTest()
        {
            ValidationServiceBuilder serviceBuilder = new ValidationServiceBuilder(false);
            serviceBuilder.RuleFor((ServiceTestEntity entity) => entity.Ten).NotEqual(10);
            serviceBuilder.RuleFor((ServiceTestEntity entity) => entity.Nine).NotEqual(5);

            List<string> expectedDetails = new List<string>() { NotEqualValidator.DefaultFailureMessage + 10 };
            Assert.Equal(expectedDetails, serviceBuilder.Build().Validate(this.testEntity).Details);
        }

        [Fact]
        public void NotEqualWithLambdasExtensionProvidesCorrectValidationTest()
        {
            ValidationServiceBuilder serviceBuilder = new ValidationServiceBuilder(false);
            serviceBuilder.RuleFor((ServiceTestEntity entity) => entity.Ten).NotEqual(entity => entity.AnotherTen);
            serviceBuilder.RuleFor((ServiceTestEntity entity) => entity.Nine).NotEqual(entity => entity.Ten);

            List<string> expectedDetails = new List<string>() { NotEqualValidator.DefaultFailureMessage + "entity.AnotherTen" };
            Assert.Equal(expectedDetails, serviceBuilder.Build().Validate(this.testEntity).Details);
        }

        [Fact]
        public void GreaterThanExtensionProvidesCorrectValidationTest()
        {
            ValidationServiceBuilder serviceBuilder = new ValidationServiceBuilder(false);
            serviceBuilder.RuleFor((ServiceTestEntity entity) => entity.Ten).GreaterThan(8);
            serviceBuilder.RuleFor((ServiceTestEntity entity) => entity.Nine).GreaterThan(10);

            List<string> expectedDetails = new List<string>() { GreaterThanValidator.DefaultFailureMessage + 10 };
            Assert.Equal(expectedDetails, serviceBuilder.Build().Validate(this.testEntity).Details);
        }

        [Fact]
        public void GreaterThanWithLambdasExtensionProvidesCorrectValidationTest()
        {
            ValidationServiceBuilder serviceBuilder = new ValidationServiceBuilder(false);
            serviceBuilder.RuleFor((ServiceTestEntity entity) => entity.Ten).GreaterThan(entity => entity.Eight);
            serviceBuilder.RuleFor((ServiceTestEntity entity) => entity.Nine).GreaterThan(entity => entity.Ten);

            List<string> expectedDetails = new List<string>() { GreaterThanValidator.DefaultFailureMessage + "entity.Ten" };
            Assert.Equal(expectedDetails, serviceBuilder.Build().Validate(this.testEntity).Details);
        }

        [Fact]
        public void GreaterThanOrEqualExtensionProvidesCorrectValidationTest()
        {
            ValidationServiceBuilder serviceBuilder = new ValidationServiceBuilder(false);
            serviceBuilder.RuleFor((ServiceTestEntity entity) => entity.Nine).GreaterThanOrEqual(8);
            serviceBuilder.RuleFor((ServiceTestEntity entity) => entity.Ten).GreaterThanOrEqual(10);
            serviceBuilder.RuleFor((ServiceTestEntity entity) => entity.Eight).GreaterThanOrEqual(10);

            List<string> expectedDetails = new List<string>() { GreaterThanOrEqualValidator.DefaultFailureMessage + 10 };
            Assert.Equal(expectedDetails, serviceBuilder.Build().Validate(this.testEntity).Details);
        }

        [Fact]
        public void GreaterThanOrEqualWithLambdasExtensionProvidesCorrectValidationTest()
        {
            ValidationServiceBuilder serviceBuilder = new ValidationServiceBuilder(false);
            serviceBuilder.RuleFor((ServiceTestEntity entity) => entity.Nine).GreaterThanOrEqual(entity => entity.Eight);
            serviceBuilder.RuleFor((ServiceTestEntity entity) => entity.Ten).GreaterThanOrEqual(entity => entity.AnotherTen);
            serviceBuilder.RuleFor((ServiceTestEntity entity) => entity.Eight).GreaterThanOrEqual(entity => entity.Ten);

            List<string> expectedDetails = new List<string>() { GreaterThanOrEqualValidator.DefaultFailureMessage + "entity.Ten" };
            Assert.Equal(expectedDetails, serviceBuilder.Build().Validate(this.testEntity).Details);
        }

        [Fact]
        public void LessThanExtensionProvidesCorrectValidationTest()
        {
            ValidationServiceBuilder serviceBuilder = new ValidationServiceBuilder(false);
            serviceBuilder.RuleFor((ServiceTestEntity entity) => entity.Nine).LessThan(10);
            serviceBuilder.RuleFor((ServiceTestEntity entity) => entity.Eight).LessThan(7);

            List<string> expectedDetails = new List<string>() { LessThanValidator.DefaultFailureMessage + 7 };
            Assert.Equal(expectedDetails, serviceBuilder.Build().Validate(this.testEntity).Details);
        }

        [Fact]
        public void LessThanWithLambdasExtensionProvidesCorrectValidationTest()
        {
            ValidationServiceBuilder serviceBuilder = new ValidationServiceBuilder(false);
            serviceBuilder.RuleFor((ServiceTestEntity entity) => entity.Nine).LessThan(entity => entity.Ten);
            serviceBuilder.RuleFor((ServiceTestEntity entity) => entity.Ten).LessThan(entity => entity.Seven);

            List<string> expectedDetails = new List<string>() { LessThanValidator.DefaultFailureMessage + "entity.Seven" };
            Assert.Equal(expectedDetails, serviceBuilder.Build().Validate(this.testEntity).Details);
        }

        [Fact]
        public void LessThanOrEqualExtensionProvidesCorrectValidationTest()
        {
            ValidationServiceBuilder serviceBuilder = new ValidationServiceBuilder(false);
            serviceBuilder.RuleFor((ServiceTestEntity entity) => entity.Nine).LessThanOrEqual(10);
            serviceBuilder.RuleFor((ServiceTestEntity entity) => entity.Ten).LessThanOrEqual(10);
            serviceBuilder.RuleFor((ServiceTestEntity entity) => entity.Eight).LessThanOrEqual(7);

            List<string> expectedDetails = new List<string>() { LessThanOrEqualValidator.DefaultFailureMessage + 7 };
            Assert.Equal(expectedDetails, serviceBuilder.Build().Validate(this.testEntity).Details);
        }

        [Fact]
        public void LessThanOrEqualWithLambdasExtensionProvidesCorrectValidationTest()
        {
            ValidationServiceBuilder serviceBuilder = new ValidationServiceBuilder(false);
            serviceBuilder.RuleFor((ServiceTestEntity entity) => entity.Nine).LessThanOrEqual(entity => entity.Ten);
            serviceBuilder.RuleFor((ServiceTestEntity entity) => entity.Ten).LessThanOrEqual(entity => entity.AnotherTen);
            serviceBuilder.RuleFor((ServiceTestEntity entity) => entity.Eight).LessThanOrEqual(entity => entity.Seven);

            List<string> expectedDetails = new List<string>() { LessThanOrEqualValidator.DefaultFailureMessage + "entity.Seven" };
            Assert.Equal(expectedDetails, serviceBuilder.Build().Validate(this.testEntity).Details);
        }

        [Fact]
        public void WithMessageExtensionChangesDetailMessageTest()
        {
            const string newValidationMessage = "Nine is less than ten!";
            ValidationServiceBuilder serviceBuilder = new ValidationServiceBuilder(false);
            serviceBuilder.RuleFor((ServiceTestEntity entity) => entity.Ten).LessThan(5);
            serviceBuilder.RuleFor((ServiceTestEntity entity) => entity.Nine).GreaterThan(10).WithMessage(newValidationMessage);

            List<string> expectedDetails = new List<string>() { newValidationMessage, LessThanValidator.DefaultFailureMessage + 5 };
            Assert.Equal(expectedDetails, serviceBuilder.Build().Validate(this.testEntity).Details);
        }
    }
}
