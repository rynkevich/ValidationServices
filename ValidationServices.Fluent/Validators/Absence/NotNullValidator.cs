using ValidationServices.Results;

namespace ValidationServices.Fluent.Validators.Absence
{
    public class NotNullValidator : IPropertyValidator
    {
        public string FailureMessage { get; set; } = Resources.Validators.NotNullValidatorDefaultFailureMessage;

        public ElementaryConclusion Validate(PropertyValidatorContext context)
        {
            return context.PropertyToValidate == null ? 
                new ElementaryConclusion(isValid: false, this.FailureMessage) :
                new ElementaryConclusion(isValid: true);
        }
    }
}
