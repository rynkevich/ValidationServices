using System;
using ValidationServices.Results;

namespace ValidationServices.Fluent.Validators.Comparison
{
    public class LessThanValidator : AbstractComparisonValidator
    {
        private readonly string funcBodyString;

        public static string DefaultFailureMessage { get; } = "This value must be less than ";

        public LessThanValidator(IComparable comparisonValue) : base(comparisonValue)
        {
        }

        public LessThanValidator(Func<object, object> comparisonValueFunc, string funcBodyString) 
            : base(comparisonValueFunc)
        {
            this.funcBodyString = funcBodyString;
        }

        public override ElementaryConclusion Validate(PropertyValidatorContext context)
        {
            if (!(context.PropertyToValidate is IComparable))
            {
                throw new ArgumentException("Object to validate must implement IComparable interface");
            }

            object comparisonValue = this.comparisonValueFunc == null ?
                this.comparisonValue : this.comparisonValueFunc(context.ObjectToValidate);

            return this.IsLessThan(context.PropertyToValidate as IComparable, comparisonValue as IComparable) ?
                new ElementaryConclusion(isValid: true) : new ElementaryConclusion(isValid: false,
                    this.FailureMessage ?? DefaultFailureMessage +
                    (this.comparisonValueFunc != null ? this.funcBodyString : comparisonValue.ToString()));
        }

        private bool IsLessThan(IComparable objectToValidate, IComparable comparisonValue)
        {
            return objectToValidate.CompareTo(comparisonValue) < 0;
        }
    }
}
