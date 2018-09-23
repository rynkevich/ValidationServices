using ValidationServices.Results;
using ValidationServices.Fluent.Validators;

namespace ValidationServices.Fluent.Rules
{
    /// <summary>
    /// Interface that is used to generalize <see cref="IPropertyValidationRule"/>
    /// </summary>
    public interface IPropertyValidationRule
    {
        /// <summary>
        /// Validates context.
        /// </summary>
        /// <param name="context">The context that includes information about object to validate</param>
        /// <returns><see cref="ServiceConclusion"/> with validation result</returns>
        ServiceConclusion Validate(PropertyValidatorContext context);

        /// <summary>
        /// Sets property validator.
        /// </summary>
        /// <param name="propertyValidator">The validator to set</param>
        void SetPropertyValidator(IPropertyValidator propertyValidator);

        /// <summary>
        /// Sets custom failure message for last validator in rule.
        /// </summary>
        /// <param name="message">The failure message for validator</param>
        void SetLastValidatorFailureMessage(string message);
    }
}
