using ValidationServices.Results;

namespace ValidationServices.Fluent.Validators
{
    /// <summary>
    /// Contract for any property validator.
    /// </summary>
    public interface IPropertyValidator
    {
        /// <summary>
        /// Gets or sets message to be returned in <see cref="ElementaryConclusion.Details"/> if validation fails.
        /// </summary>
        string FailureMessage { get; set; }

        /// <summary>
        /// Validates specified context.
        /// </summary>
        /// <param name="context">The object with info that is required for validation</param>
        /// <returns><see cref="ElementaryConclusion"/> with validation results</returns>
        ElementaryConclusion Validate(PropertyValidatorContext context);
    }
}
