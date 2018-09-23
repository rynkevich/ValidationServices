using ValidationServices.Fluent.Internal;

namespace ValidationServices.Fluent.Validators
{
    /// <summary>
    /// Object with info that is required for validation.
    /// </summary>
    public class PropertyValidatorContext
    {
        /// <summary>
        /// Gets the object to validate.
        /// </summary>
        public object ObjectToValidate { get; private set; }

        /// <summary>
        /// Gets the property of <see cref="ObjectToValidate"/> to validate.
        /// </summary>
        public object PropertyToValidate { get; private set; }

        /// <summary>
        /// Initializes a new instance of <see cref="PropertyValidatorContext"/> class.
        /// </summary>
        /// <param name="objectToValidate">The object to validate</param>
        /// <param name="propertyToValidate">The property of <paramref name="objectToValidate"/> to validate</param>
        public PropertyValidatorContext(object objectToValidate, object propertyToValidate)
        {
            objectToValidate.Guard(nameof(objectToValidate));
            this.ObjectToValidate = objectToValidate;
            this.PropertyToValidate = propertyToValidate;
        }
    }
}
