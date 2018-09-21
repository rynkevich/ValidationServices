using ValidationServices.Results;

namespace ValidationServices.Fluent.Validators.Absence
{
    public class EmptyValidator : IPropertyValidator
    {
        private readonly object defaultValueForType;

        public string FailureMessage { get; set; } = "This property must be empty";

        public EmptyValidator(object defaultValueForType)
        {
            this.defaultValueForType = defaultValueForType;
        }

        public ElementaryConclusion Validate(object objectToValidate)
        {
            if (!(objectToValidate == null
                || AbsenceValidationRoutines.IsEmptyString(objectToValidate)
                || AbsenceValidationRoutines.IsEmptyCollection(objectToValidate)
                || Equals(objectToValidate, this.defaultValueForType)))
            {
                return new ElementaryConclusion(isValid: false, this.FailureMessage);
            }

            return new ElementaryConclusion(isValid: true);
        }
    }
}
