using ValidationServices.Results;

namespace ValidationServices.Fluent.Validators.Absence
{
    /// <summary>
    /// Validator that verifies that property is empty.
    /// </summary>
    public class EmptyValidator : IPropertyValidator
    {
        /// <summary>
        /// Default value for type of specified property.
        /// </summary>
        private readonly object _defaultValueForType;

        /// <summary>
        /// Gets or sets message to be returned in <see cref="ElementaryConclusion.Details"/> if validation fails.
        /// </summary>
        public string FailureMessage { get; set; } = Resources.Validators.EmptyValidatorDefaultFailureMessage;

        /// <summary>
        /// Initializes a new instance of <see cref="EmptyValidator"/> class.
        /// </summary>
        /// <param name="defaultValueForType">The default value for type of specified property</param>
        public EmptyValidator(object defaultValueForType)
        {
            this._defaultValueForType = defaultValueForType;
        }

        /// <summary>
        /// Validates specified context.
        /// </summary>
        /// <param name="context">The object with info that is required for validation</param>
        /// <returns><see cref="ElementaryConclusion"/> with validation results</returns>
        public ElementaryConclusion Validate(PropertyValidatorContext context)
        {
            if (!(context.PropertyToValidate == null
                || AbsenceValidationRoutines.IsEmptyString(context.PropertyToValidate)
                || AbsenceValidationRoutines.IsEmptyCollection(context.PropertyToValidate)
                || Equals(context.PropertyToValidate, this._defaultValueForType)))
            {
                return new ElementaryConclusion(isValid: false, this.FailureMessage);
            }

            return new ElementaryConclusion(isValid: true);
        }
    }
}
