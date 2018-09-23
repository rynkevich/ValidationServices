using System;
using ValidationServices.Results;

namespace ValidationServices.Fluent.Validators.Comparison
{
    public class GreaterThanOrEqualValidator : AbstractComparisonValidator
    {
        private readonly string _funcBodyString;

        public static string DefaultFailureMessage { get; } = Resources.Validators.GreaterThanOrEqualValidatorDefaultFailureMessage;

        public GreaterThanOrEqualValidator(IComparable comparisonValue) : base(comparisonValue)
        {
        }

        public GreaterThanOrEqualValidator(Func<object, object> comparisonValueFunc, string funcBodyString) : base(comparisonValueFunc)
        {
            this._funcBodyString = funcBodyString;
        }

        public override ElementaryConclusion Validate(PropertyValidatorContext context)
        {
            if (!(context.PropertyToValidate is IComparable))
            {
                throw new ArgumentException(Resources.Validators.ArgumentExceptionMustImplementIComparable);
            }

            var comparisonValue = this.comparisonValueFunc == null ?
                this.comparisonValue : this.comparisonValueFunc(context.ObjectToValidate);

            return this.IsGreaterThanOrEqual(context.PropertyToValidate as IComparable, comparisonValue as IComparable) ?
               new ElementaryConclusion(isValid: true) : new ElementaryConclusion(isValid: false,
                    this.FailureMessage ?? DefaultFailureMessage +
                    (this.comparisonValueFunc != null ? this._funcBodyString : comparisonValue.ToString()));
        }

        private bool IsGreaterThanOrEqual(IComparable objectToValidate, IComparable comparisonValue)
        {
            return objectToValidate.CompareTo(comparisonValue) >= 0;
        }
    }
}
