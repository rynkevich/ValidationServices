using ValidationServices.Results;

namespace ValidationServices.Fluent.Validators.Absence
{
    /// <summary>
    /// Validator that verifies that property is null.
    /// </summary>
    public class NullValidator : IPropertyValidator
    {
        /// <summary>
        /// Gets or sets message to be returned in <see cref="ElementaryConclusion.Details"/> if validation fails.
        /// </summary>
        public string FailureMessage { get; set; } = Resources.Validators.NullValidatorDefaultFailureMessage;

        /// <summary>
        /// Validates specified context.
        /// </summary>
        /// <param name="context">The object with info that is required for validation</param>
        /// <returns><see cref="ElementaryConclusion"/> with validation results</returns>
        public ElementaryConclusion Validate(PropertyValidatorContext context)
        {
            return context.PropertyToValidate != null ?
                new ElementaryConclusion(isValid: false, this.FailureMessage) :
                new ElementaryConclusion(isValid: true);
        }
    }
}
