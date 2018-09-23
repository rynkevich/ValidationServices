using System;
using System.Collections;
using ValidationServices.Results;

namespace ValidationServices.Fluent.Validators.Comparison
{
    public class EqualValidator : AbstractEqualValidator
    {
        private readonly string funcBodyString;

        public static string DefaultFailureMessage { get; } = "This value must be equal to ";

        public EqualValidator(object comparisonValue, IComparer comparer = null) : base(comparisonValue, comparer)
        {
        }

        public EqualValidator(Func<object, object> comparisonValueFunc, string funcBodyString, 
            IComparer comparer = null) : base(comparisonValueFunc, comparer)
        {
            this.funcBodyString = funcBodyString;
        }

        public override ElementaryConclusion Validate(PropertyValidatorContext context)
        {
            object comparisonValue = this.comparisonValueFunc == null ?
                this.comparisonValue : this.comparisonValueFunc(context.ObjectToValidate);

            return this.IsEqual(context.PropertyToValidate, comparisonValue) ?
                new ElementaryConclusion(isValid: true) : new ElementaryConclusion(isValid: false,
                    this.FailureMessage ?? DefaultFailureMessage + 
                    (this.comparisonValueFunc != null ? this.funcBodyString : comparisonValue.ToString()));
        }
    }
}