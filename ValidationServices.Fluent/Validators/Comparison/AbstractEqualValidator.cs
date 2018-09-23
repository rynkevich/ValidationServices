using System;
using System.Collections;

namespace ValidationServices.Fluent.Validators.Comparison
{
    /// <summary>
    /// Abstract base class for all equality validators.
    /// </summary>
    public abstract class AbstractEqualValidator : AbstractComparisonValidator
    {
        /// <summary>
        /// The comparer to use when checking for equality.
        /// </summary>
        protected readonly IComparer comparer;

        /// <summary>
        /// Initializes base properties and fields in class that inherits from <see cref="AbstractEqualValidator"/>
        /// </summary>
        /// <param name="comparisonValue">The value to compare to</param>
        /// <param name="comparer">The comparer to use when checking for equality</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="comparisonValue"/> is null</exception>
        protected AbstractEqualValidator(object comparisonValue, IComparer comparer) : base(comparisonValue)
        {
            this.comparer = comparer;
        }

        /// <summary>
        /// Initializes base properties and fields in class that inherits from <see cref="AbstractEqualValidator"/>
        /// </summary>
        /// <param name="comparisonValueFunc">The lambda expression that provides the value to compare to</param>
        /// <param name="comparer">The comparer to use when checking for equality</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="comparisonValueFunc"/> is null</exception>
        protected AbstractEqualValidator(Func<object, object> comparisonValueFunc, IComparer comparer) : base(comparisonValueFunc)
        {
            this.comparer = comparer;
        }

        /// <summary>
        /// Checks if <paramref name="propertyToValidate"/> is equal to <paramref name="comparisonValue"/>
        /// </summary>
        /// <param name="propertyToValidate">The property to validate</param>
        /// <param name="comparisonValue">The value to compare to</param>
        /// <returns></returns>
        protected bool IsEqual(object propertyToValidate, object comparisonValue)
        {
            if (this.comparer != null)
            {
                return this.comparer.Compare(propertyToValidate, comparisonValue) == 0;
            }

            if (comparisonValue is IComparable comparableComparisonValue
                && propertyToValidate is IComparable comparableObjectToValidate)
            {
                return comparableObjectToValidate.CompareTo(comparableComparisonValue) == 0;
            }

            return Equals(propertyToValidate, comparisonValue);
        }
    }
}
