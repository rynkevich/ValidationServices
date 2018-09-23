using System;
using System.Collections;
using ValidationServices.Results;

namespace ValidationServices.Fluent.Validators.Comparison
{
    /// <summary>
    /// Validator that verifies that property is equal to specified value.
    /// </summary>
    public class EqualValidator : AbstractEqualValidator
    {
        /// <summary>
        /// The string representation of lambda expression body that is used to provide value to compare to
        /// </summary>
        private readonly string _funcBodyString;

        /// <summary>
        /// Gets the default failure message to be returned in <see cref="ElementaryConclusion.Details"/> if validation fails.
        /// </summary>
        public static string DefaultFailureMessage { get; } = Resources.Validators.EqualValidatorDefaultFailureMessage;

        /// <summary>
        /// Initializes a new instance of <see cref="EqualValidator"/> class.
        /// </summary>
        /// <param name="comparisonValue">The value to compare to</param>
        /// <param name="comparer">The comparer to use when checking for equality</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="comparisonValue"/> is null</exception>
        public EqualValidator(object comparisonValue, IComparer comparer = null) : base(comparisonValue, comparer)
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="EqualValidator"/> class.
        /// </summary>
        /// <param name="comparisonValueFunc">The lambda expression to provide the value to compare to</param>
        /// <param name="funcBodyString">The string representation of <paramref name="comparisonValueFunc"/> body</param>
        /// <param name="comparer">The comparer to use when checking for equality</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="comparisonValueFunc"/> is null</exception>
        public EqualValidator(Func<object, object> comparisonValueFunc, string funcBodyString, 
            IComparer comparer = null) : base(comparisonValueFunc, comparer)
        {
            this._funcBodyString = funcBodyString;
        }

        /// <summary>
        /// Validates specified context.
        /// </summary>
        /// <param name="context">The object with info that is required for validation</param>
        /// <returns><see cref="ElementaryConclusion"/> with validation results</returns>
        public override ElementaryConclusion Validate(PropertyValidatorContext context)
        {
            var comparisonValue = this.comparisonValueFunc == null ?
                this.comparisonValue : this.comparisonValueFunc(context.ObjectToValidate);

            return this.IsEqual(context.PropertyToValidate, comparisonValue) ?
                new ElementaryConclusion(isValid: true) : new ElementaryConclusion(isValid: false,
                    this.FailureMessage ?? DefaultFailureMessage + 
                    (this.comparisonValueFunc != null ? this._funcBodyString : comparisonValue.ToString()));
        }
    }
}