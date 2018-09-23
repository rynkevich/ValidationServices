using ValidationServices.Results;

namespace ValidationServices.Fluent.Validators.Absence
{
    public class NotEmptyValidator : IPropertyValidator
    {
        private readonly object _defaultValueForType;

        public string FailureMessage { get; set; } = "This property must not be empty";

        public NotEmptyValidator(object defaultValueForType)
        {
            this._defaultValueForType = defaultValueForType;
        }

        public ElementaryConclusion Validate(PropertyValidatorContext context)
        {
            if (context.PropertyToValidate == null
                || AbsenceValidationRoutines.IsEmptyString(context.PropertyToValidate)
                || AbsenceValidationRoutines.IsEmptyCollection(context.PropertyToValidate)
                || Equals(context.PropertyToValidate, this._defaultValueForType))
            {
                return new ElementaryConclusion(isValid: false, this.FailureMessage);
            }

            return new ElementaryConclusion(isValid: true);
        }
    }
}
