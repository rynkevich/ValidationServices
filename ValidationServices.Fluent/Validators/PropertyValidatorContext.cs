using System;

namespace ValidationServices.Fluent.Validators
{
    public class PropertyValidatorContext
    {
        public object ObjectToValidate { get; private set; }
        public object PropertyToValidate { get; private set; }

        public PropertyValidatorContext(object objectToValidate, object propertyToValidate)
        {
            this.ObjectToValidate = objectToValidate ?? throw new ArgumentNullException(nameof(objectToValidate));
            this.PropertyToValidate = propertyToValidate;
        }
    }
}
