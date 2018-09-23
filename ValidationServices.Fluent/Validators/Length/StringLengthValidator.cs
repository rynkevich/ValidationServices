using System;
using ValidationServices.Fluent.Internal;
using ValidationServices.Results;

namespace ValidationServices.Fluent.Validators.Length
{
    /// <summary>
    /// Validator that verifies that string property is inside the specified range.
    /// </summary>
    public class StringLengthValidator : IPropertyValidator
    {
        /// <summary>
        /// The lower length constraint.
        /// </summary>
        private readonly int _min;

        /// <summary>
        /// The upper length constraint.
        /// </summary>
        private readonly int _max;

        /// <summary>
        /// The lambda expression that provides the lower length constraint.
        /// </summary>
        private readonly Func<object, int> _minFunc;

        /// <summary>
        /// The lambda expression that provides the upper length constraint.
        /// </summary>
        private readonly Func<object, int> _maxFunc;

        /// <summary>
        /// The value for <see cref="_max"/> if max constraint is not specified.
        /// </summary>
        protected static readonly int MAX_NOT_SPECIFIED = int.MaxValue;

        /// <summary>
        /// Gets or sets message to be returned in <see cref="ElementaryConclusion.Details"/> if validation fails.
        /// </summary>
        public string FailureMessage { get; set; } = Resources.Validators.StringLengthValidatorDefaultFailureMessage;

        /// <summary>
        /// Initializes a new instance of <see cref="StringLengthValidator"/> class.
        /// </summary>
        /// <param name="min">The lower length constraint</param>
        /// <param name="max">The upper length constraint</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if at least one of specified constraints is invalid</exception>
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

        /// <summary>
        /// Initializes a new instance of <see cref="StringLengthValidator"/> class.
        /// </summary>
        /// <param name="minFunc">The lambda expression that provides the lower length constraint</param>
        /// <param name="maxFunc">The lambda expression that provides the upper length constraint</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="minFunc"/> or <paramref name="maxFunc"/> is null</exception>
        public StringLengthValidator(Func<object, int> minFunc, Func<object, int> maxFunc)
        {
            minFunc.Guard(nameof(minFunc));
            maxFunc.Guard(nameof(maxFunc));
            this._minFunc = minFunc;
            this._maxFunc = maxFunc;
        }

        /// <summary>
        /// Validates specified context.
        /// </summary>
        /// <param name="context">The object with info that is required for validation</param>
        /// <returns><see cref="ElementaryConclusion"/> with validation results</returns>
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
