using System;
using ValidationServices.Results;

namespace ValidationServices.Fluent.Validators.Comparison
{
    public abstract class AbstractComparisonValidator : IPropertyValidator
    {
        protected readonly object comparisonValue;
        protected readonly Func<object, object> comparisonValueFunc;

        public string FailureMessage { get; set; }

        public AbstractComparisonValidator(object comparisonValue)
        {
            this.comparisonValue = comparisonValue ?? throw new ArgumentNullException(nameof(comparisonValue));
        }

        protected AbstractComparisonValidator(Func<object, object> comparisonValueFunc)
        {
            this.comparisonValueFunc = comparisonValueFunc ?? throw new ArgumentNullException(nameof(comparisonValueFunc));
        }

        public abstract ElementaryConclusion Validate(PropertyValidatorContext context);
    }
}
