using ValidationServices.Results;

namespace ValidationServices.Fluent.Validators.Absence
{
    public class NullValidator : IPropertyValidator
    {
        public string FailureMessage { get; set; } = Resources.Validators.NullValidatorDefaultFailureMessage;

        public ElementaryConclusion Validate(PropertyValidatorContext context)
        {
            return context.PropertyToValidate != null ?
               new ElementaryConclusion(isValid: false, this.FailureMessage) :
               new ElementaryConclusion(isValid: true);
        }
    }
}
