using ValidationServices.Results;
using ValidationServices.Fluent.Validators;

namespace ValidationServices.Fluent.Rules
{
    public interface IPropertyValidationRule
    {
        ServiceConclusion Validate(PropertyValidatorContext context);
        void SetPropertyValidator(IPropertyValidator propertyValidator);
        void SetLastValidatorFailureMessage(string message);
    }
}
