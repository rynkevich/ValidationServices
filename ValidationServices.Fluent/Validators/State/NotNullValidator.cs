using ValidationServices.Results;

namespace ValidationServices.Fluent.Validators.State
{
    public class NotNullValidator : IPropertyValidator
    {
        public string FailureMessage { get; set; } = "This property must not be null";

        public ElementaryConclusion Validate(object objectToValidate)
        {
            if (objectToValidate == null)
            {
                return new ElementaryConclusion(isValid: false, this.FailureMessage);
            }

            return new ElementaryConclusion(isValid: true);
        }
    }
}
