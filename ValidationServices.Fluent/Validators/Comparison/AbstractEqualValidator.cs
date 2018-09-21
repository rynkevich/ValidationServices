using System;
using System.Collections;

namespace ValidationServices.Fluent.Validators.Comparison
{
    public abstract class AbstractEqualValidator : AbstractComparisonValidator
    {
        protected readonly IComparer comparer;

        protected AbstractEqualValidator(object comparisonValue, IComparer comparer) : base(comparisonValue)
        {
            this.comparer = comparer;
        }

        protected AbstractEqualValidator(Func<object, object> comparisonValueFunc, IComparer comparer) : base(comparisonValueFunc)
        {
            this.comparer = comparer;
        }

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
