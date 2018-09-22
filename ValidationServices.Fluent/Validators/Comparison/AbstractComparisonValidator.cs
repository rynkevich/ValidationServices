using System;
using ValidationServices.Fluent.Internal;
using ValidationServices.Results;

namespace ValidationServices.Fluent.Validators.Comparison
{
    public abstract class AbstractComparisonValidator : IPropertyValidator
    {
        protected readonly object comparisonValue;
        protected readonly Func<object, object> comparisonValueFunc;

        public string FailureMessage { get; set; }

        protected AbstractComparisonValidator(object comparisonValue)
        {
            comparisonValue.Guard(nameof(comparisonValue));
            this.comparisonValue = comparisonValue;
        }

        protected AbstractComparisonValidator(Func<object, object> comparisonValueFunc)
        {
            comparisonValueFunc.Guard(nameof(comparisonValueFunc));
            this.comparisonValueFunc = comparisonValueFunc;
        }

        public abstract ElementaryConclusion Validate(PropertyValidatorContext context);
    }
}
