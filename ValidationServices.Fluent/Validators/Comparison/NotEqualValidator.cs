using System;
using System.Collections;
using System.Linq.Expressions;
using ValidationServices.Results;

namespace ValidationServices.Fluent.Validators.Comparison
{
    public class NotEqualValidator : AbstractEqualValidator
    {
        private readonly string _funcBodyString;

        public static string DefaultFailureMessage { get; } = Resources.Validators.NotEqualValidatorDefaultFailureMessage;

        public NotEqualValidator(object comparisonValue, IComparer comparer = null) : base(comparisonValue, comparer)
        {
        }

        public NotEqualValidator(Func<object, object> comparisonValueFunc, string funcBodyString, 
            IComparer comparer = null) : base(comparisonValueFunc, comparer)
        {
            this._funcBodyString = funcBodyString;
        }

        public override ElementaryConclusion Validate(PropertyValidatorContext context)
        {
            var comparisonValue = this.comparisonValueFunc == null ?
                this.comparisonValue : this.comparisonValueFunc(context.ObjectToValidate);

            return !this.IsEqual(context.PropertyToValidate, comparisonValue) ?
                new ElementaryConclusion(isValid: true) : new ElementaryConclusion(isValid: false,
                    this.FailureMessage ?? DefaultFailureMessage +
                    (this.comparisonValueFunc != null ? this._funcBodyString : comparisonValue.ToString()));
        }
    }
}
