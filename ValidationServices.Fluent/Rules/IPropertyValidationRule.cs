using ValidationServices.Results;
using ValidationServices.Fluent.Validators;

namespace ValidationServices.Fluent.Rules
{
    public interface IPropertyValidationRule
    {
        GeneralConclusion Validate(object objectToValidate, object propertyToValidate);
        void SetPropertyValidator(IPropertyValidator propertyValidator);
        void SetLastValidatorFailureMessage(string message);
    }
}
