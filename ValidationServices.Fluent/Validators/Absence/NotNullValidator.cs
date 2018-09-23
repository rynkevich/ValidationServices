using ValidationServices.Results;

namespace ValidationServices.Fluent.Validators.Absence
{
    /// <summary>
    /// Validator that verifies that property is not null.
    /// </summary>
    public class NotNullValidator : IPropertyValidator
    {
        /// <summary>
        /// Gets or sets message to be returned in <see cref="ElementaryConclusion.Details"/> if validation fails.
        /// </summary>
        public string FailureMessage { get; set; } = Resources.Validators.NotNullValidatorDefaultFailureMessage;

        /// <summary>
        /// Validates specified context.
        /// </summary>
        /// <param name="context">The object with info that is required for validation</param>
        /// <returns><see cref="ElementaryConclusion"/> with validation results</returns>
        public ElementaryConclusion Validate(PropertyValidatorContext context)
        {
            return context.PropertyToValidate == null ? 
                new ElementaryConclusion(isValid: false, this.FailureMessage) :
                new ElementaryConclusion(isValid: true);
        }
    }
}
