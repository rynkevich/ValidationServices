using System;
using ValidationServices.Results;

namespace ValidationServices.Fluent.Validators.Length
{
    public class StringLengthValidator : IPropertyValidator
    {
        private readonly int min;
        private readonly int max;
        private readonly Func<object, int> minFunc;
        private readonly Func<object, int> maxFunc;
        protected static readonly int MAX_NOT_SPECIFIED = -1;

        public string FailureMessage { get; set; } = "Length of a string must satisfy specified constraints";

        public StringLengthValidator(int min, int max)
        {
            this.min = min;
            this.max = max;

            if (max != MAX_NOT_SPECIFIED && min > max)
            {
                throw new ArgumentOutOfRangeException(nameof(max), "Max should be larger than min");
            }
        }

        public StringLengthValidator(Func<object, int> minFunc, Func<object, int> maxFunc)
        {
            this.minFunc = minFunc ?? throw new ArgumentNullException(nameof(minFunc));
            this.maxFunc = maxFunc ?? throw new ArgumentNullException(nameof(maxFunc));
        }

        public ElementaryConclusion Validate(object objectToValidate)
        {
            if (objectToValidate == null)
            {
                return new ElementaryConclusion(isValid: false, this.FailureMessage);
            }

            int min = this.minFunc != null ? this.minFunc(objectToValidate) : this.min;
            int max = this.maxFunc != null ? this.maxFunc(objectToValidate) : this.max;

            int length = objectToValidate.ToString().Length;

            if (length < min || (length > max && max != MAX_NOT_SPECIFIED))
            {
                return new ElementaryConclusion(isValid: false, this.FailureMessage);
            }

            return new ElementaryConclusion(isValid: true);
        }
    }
}
