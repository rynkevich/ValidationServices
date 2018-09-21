using System;
using ValidationServices.Results;

namespace ValidationServices.Fluent.Validators.Absence
{
    public class NotEmptyValidator : IPropertyValidator
    {
        private readonly object defaultValueForType;

        public string FailureMessage { get; set; } = "This property must not be empty";

        public NotEmptyValidator(object defaultValueForType)
        {
            this.defaultValueForType = defaultValueForType;
        }

        public ElementaryConclusion Validate(object objectToValidate, object propertyToValidate)
        {
            if (objectToValidate == null)
            {
                throw new ArgumentNullException(nameof(objectToValidate));
            }

            if (propertyToValidate == null
                || AbsenceValidationRoutines.IsEmptyString(propertyToValidate)
                || AbsenceValidationRoutines.IsEmptyCollection(propertyToValidate)
                || Equals(propertyToValidate, this.defaultValueForType))
            {
                return new ElementaryConclusion(isValid: false, this.FailureMessage);
            }

            return new ElementaryConclusion(isValid: true);
        }
    }
}
