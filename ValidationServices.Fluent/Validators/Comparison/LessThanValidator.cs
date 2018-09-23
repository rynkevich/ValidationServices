using System;
using ValidationServices.Results;

namespace ValidationServices.Fluent.Validators.Comparison
{
    /// <summary>
    /// Validator that verifies that property is less than the specified value.
    /// </summary>
    public class LessThanValidator : AbstractComparisonValidator
    {
        /// <summary>
        /// The string representation of lambda expression body that is used to provide value to compare to
        /// </summary>
        private readonly string _funcBodyString;

        /// <summary>
        /// Gets the default failure message to be returned in <see cref="ElementaryConclusion.Details"/> if validation fails.
        /// </summary>
        public static string DefaultFailureMessage { get; } = Resources.Validators.LessThanValidatorDefaultFailureMessage;

        /// <summary>
        /// Initializes a new instance of <see cref="LessThanValidator"/> class.
        /// </summary>
        /// <param name="comparisonValue">The value to compare to</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="comparisonValue"/> is null</exception>
        public LessThanValidator(IComparable comparisonValue) : base(comparisonValue)
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="LessThanValidator"/> class.
        /// </summary>
        /// <param name="comparisonValueFunc">The lambda expression to provide the value to compare to</param>
        /// <param name="funcBodyString">The string representation of <paramref name="comparisonValueFunc"/> body</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="comparisonValueFunc"/> is null</exception>
        public LessThanValidator(Func<object, object> comparisonValueFunc, string funcBodyString) 
            : base(comparisonValueFunc)
        {
            this._funcBodyString = funcBodyString;
        }

        /// <summary>
        /// Validates specified context.
        /// </summary>
        /// <param name="context">The object with info that is required for validation</param>
        /// <returns><see cref="ElementaryConclusion"/> with validation results</returns>
        /// <exception cref="ArgumentException">Thrown if <see cref="PropertyValidatorContext.PropertyToValidate"/> 
        /// in <paramref name="context"/> does not implement <see cref="IComparable"/></exception>
        public override ElementaryConclusion Validate(PropertyValidatorContext context)
        {
            if (!(context.PropertyToValidate is IComparable))
            {
                throw new ArgumentException(Resources.Validators.ArgumentExceptionMustImplementIComparable);
            }

            var comparisonValue = this.comparisonValueFunc == null ?
                this.comparisonValue : this.comparisonValueFunc(context.ObjectToValidate);

            return this.IsLessThan(context.PropertyToValidate as IComparable, comparisonValue as IComparable) ?
                new ElementaryConclusion(isValid: true) : new ElementaryConclusion(isValid: false,
                    this.FailureMessage ?? DefaultFailureMessage +
                    (this.comparisonValueFunc != null ? this._funcBodyString : comparisonValue.ToString()));
        }

        /// <summary>
        /// Checks if <paramref name="objectToValidate"/> is less than <paramref name="comparisonValue"/>
        /// </summary>
        /// <param name="objectToValidate">The object to validate</param>
        /// <param name="comparisonValue">The value to compare to</param>
        /// <returns></returns>
        private bool IsLessThan(IComparable objectToValidate, IComparable comparisonValue)
        {
            return objectToValidate.CompareTo(comparisonValue) < 0;
        }
    }
}
