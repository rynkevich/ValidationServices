using ValidationServices.Fluent.Internal;

namespace ValidationServices.Fluent.Validators
{
    public class PropertyValidatorContext
    {
        public object ObjectToValidate { get; private set; }
        public object PropertyToValidate { get; private set; }

        public PropertyValidatorContext(object objectToValidate, object propertyToValidate)
        {
            objectToValidate.Guard(nameof(objectToValidate));
            this.ObjectToValidate = objectToValidate;
            this.PropertyToValidate = propertyToValidate;
        }
    }
}
