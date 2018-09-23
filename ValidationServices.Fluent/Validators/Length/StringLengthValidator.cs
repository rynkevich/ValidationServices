using System;
using ValidationServices.Fluent.Internal;
using ValidationServices.Results;

namespace ValidationServices.Fluent.Validators.Length
{
    public class StringLengthValidator : IPropertyValidator
    {
        private readonly int min;
        private readonly int max;
        private readonly Func<object, int> minFunc;
        private readonly Func<object, int> maxFunc;
        protected static readonly int MAX_NOT_SPECIFIED = int.MaxValue;

        public string FailureMessage { get; set; } = "Length of a string must satisfy specified constraints";

        public StringLengthValidator(int min, int max)
        {
            this.min = min;
            this.max = max;

            if (min < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(min), "Lower length constraint must be a zero or a positive number");
            }
            if (max < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(max), "Upper length constraint must be a zero or a positive number");
            }

            if (min > max)
            {
                throw new ArgumentOutOfRangeException(nameof(max), "Max should be larger than min");
            }
        }

        public StringLengthValidator(Func<object, int> minFunc, Func<object, int> maxFunc)
        {
            minFunc.Guard(nameof(minFunc));
            maxFunc.Guard(nameof(maxFunc));
            this.minFunc = minFunc;
            this.maxFunc = maxFunc;
        }

        public ElementaryConclusion Validate(PropertyValidatorContext context)
        {
            if (context.PropertyToValidate == null)
            {
                return new ElementaryConclusion(isValid: false, this.FailureMessage);
            }

            int min = this.minFunc != null ? this.minFunc(context.ObjectToValidate) : this.min;
            int max = this.maxFunc != null ? this.maxFunc(context.ObjectToValidate) : this.max;

            int length = context.PropertyToValidate.ToString().Length;

            if (length < min || (length > max && max != MAX_NOT_SPECIFIED))
            {
                return new ElementaryConclusion(isValid: false, this.FailureMessage);
            }

            return new ElementaryConclusion(isValid: true);
        }
    }
}
