using System;
using ValidationServices.Fluent.Internal;
using ValidationServices.Results;

namespace ValidationServices.Fluent.Validators.Length
{
    public class StringLengthValidator : IPropertyValidator
    {
        private readonly int _min;
        private readonly int _max;
        private readonly Func<object, int> _minFunc;
        private readonly Func<object, int> _maxFunc;
        protected static readonly int MAX_NOT_SPECIFIED = int.MaxValue;

        public string FailureMessage { get; set; } = Resources.Validators.StringLengthValidatorDefaultFailureMessage;

        public StringLengthValidator(int min, int max)
        {
            this._min = min;
            this._max = max;

            if (min < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(min), 
                    Resources.Validators.ArgumentOutOfRangeExceptionInvalidLowerLengthConstraint);
            }
            if (max < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(max), 
                    Resources.Validators.ArgumentOutOfRangeExceptionInvalidUpperLengthConstraint);
            }

            if (min > max)
            {
                throw new ArgumentOutOfRangeException(nameof(max), 
                    Resources.Validators.ArgumentOutOfRangeExceptionMinExceedsMax);
            }
        }

        public StringLengthValidator(Func<object, int> minFunc, Func<object, int> maxFunc)
        {
            minFunc.Guard(nameof(minFunc));
            maxFunc.Guard(nameof(maxFunc));
            this._minFunc = minFunc;
            this._maxFunc = maxFunc;
        }

        public ElementaryConclusion Validate(PropertyValidatorContext context)
        {
            if (context.PropertyToValidate == null)
            {
                return new ElementaryConclusion(isValid: false, this.FailureMessage);
            }

            int _min = this._minFunc != null ? this._minFunc(context.ObjectToValidate) : this._min;
            int _max = this._maxFunc != null ? this._maxFunc(context.ObjectToValidate) : this._max;

            int length = context.PropertyToValidate.ToString().Length;

            if (length < _min || (length > _max && _max != MAX_NOT_SPECIFIED))
            {
                return new ElementaryConclusion(isValid: false, this.FailureMessage);
            }

            return new ElementaryConclusion(isValid: true);
        }
    }
}
