using System;
using ValidationServices.Results;

namespace ValidationServices.Fluent.Validators.Comparison
{
    public class GreaterThanValidator : AbstractComparisonValidator
    {
        private readonly string _funcBodyString;

        public static string DefaultFailureMessage { get; } = "This value must be greater than ";

        public GreaterThanValidator(IComparable comparisonValue) : base(comparisonValue)
        {
        }

        public GreaterThanValidator(Func<object, object> comparisonValueFunc, string funcBodyString) : base(comparisonValueFunc)
        {
            this._funcBodyString = funcBodyString;
        }

        public override ElementaryConclusion Validate(PropertyValidatorContext context)
        {
            if (!(context.PropertyToValidate is IComparable))
            {
                throw new ArgumentException("Object to validate must implement IComparable interface");
            }

            var comparisonValue = this.comparisonValueFunc == null ?
                this.comparisonValue : this.comparisonValueFunc(context.ObjectToValidate);

            return this.IsGreaterThan(context.PropertyToValidate as IComparable, comparisonValue as IComparable) ?
                new ElementaryConclusion(isValid: true) : new ElementaryConclusion(isValid: false,
                    this.FailureMessage ?? DefaultFailureMessage +
                    (this.comparisonValueFunc != null ? this._funcBodyString : comparisonValue.ToString()));
        }

        private bool IsGreaterThan(IComparable objectToValidate, IComparable comparisonValue)
        {
            return objectToValidate.CompareTo(comparisonValue) > 0;
        }
    }
}
