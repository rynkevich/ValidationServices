using System;
using ValidationServices.Fluent.Internal;
using ValidationServices.Results;

namespace ValidationServices.Fluent.Validators.Comparison
{
    /// <summary>
    /// Abstract base class for all comparison validators.
    /// </summary>
    public abstract class AbstractComparisonValidator : IPropertyValidator
    {
        /// <summary>
        /// The value to compare to.
        /// </summary>
        protected readonly object comparisonValue;

        /// <summary>
        /// The lambda expression that provides the value to compare to.
        /// </summary>
        protected readonly Func<object, object> comparisonValueFunc;

        /// <summary>
        /// Gets or sets message to be returned in <see cref="ElementaryConclusion.Details"/> if validation fails.
        /// </summary>
        public string FailureMessage { get; set; }

        /// <summary>
        /// Initializes base properties and fields in class that inherits from <see cref="AbstractComparisonValidator"/>
        /// </summary>
        /// <param name="comparisonValue">The value to compare to</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="comparisonValue"/> is null</exception>
        protected AbstractComparisonValidator(object comparisonValue)
        {
            comparisonValue.Guard(nameof(comparisonValue));
            this.comparisonValue = comparisonValue;
        }

        /// <summary>
        /// Initializes base properties and fields in class that inherits from <see cref="AbstractComparisonValidator"/>
        /// </summary>
        /// <param name="comparisonValueFunc">The lambda expression that provides the value to compare to</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="comparisonValueFunc"/> is null</exception>
        protected AbstractComparisonValidator(Func<object, object> comparisonValueFunc)
        {
            comparisonValueFunc.Guard(nameof(comparisonValueFunc));
            this.comparisonValueFunc = comparisonValueFunc;
        }

        /// <summary>
        /// Validates specified context.
        /// </summary>
        /// <param name="context">The object with info that is required for validation</param>
        /// <returns><see cref="ElementaryConclusion"/> with validation results</returns>
        public abstract ElementaryConclusion Validate(PropertyValidatorContext context);
    }
}
