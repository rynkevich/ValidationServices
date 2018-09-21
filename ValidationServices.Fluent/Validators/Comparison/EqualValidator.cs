using System;
using System.Collections;
using ValidationServices.Fluent.Rules;
using ValidationServices.Results;

namespace ValidationServices.Fluent.Validators.Comparison
{
    public static class EqualRuleExtension
    {
        public static PropertyValidationRule<TOwner, TProperty> Equal<TOwner, TProperty>(
           this PropertyValidationRule<TOwner, TProperty> rule, TProperty valueToCompare,
           IComparer equalityComparer = null)
        {
            rule.SetPropertyValidator(new EqualValidator(valueToCompare, equalityComparer));
            return rule;
        }

        public static PropertyValidationRule<TOwner, TProperty> Equal<TOwner, TProperty>(
            this PropertyValidationRule<TOwner, TProperty> rule, Func<TOwner, TProperty> valueToCompare,
            IComparer equalityComparer = null)
        {
            rule.SetPropertyValidator(new EqualValidator(valueToCompare, equalityComparer));
            return rule;
        }
    }

    public class EqualValidator : AbstractEqualValidator
    {
        private static readonly string DEFAULT_FAILURE_MESSAGE = "This value must be equal to ";

        public EqualValidator(object comparisonValue, IComparer comparer = null) : base(comparisonValue, comparer)
        {
        }

        public EqualValidator(Func<object, object> comparisonValueFunc, IComparer comparer = null) : base(comparisonValueFunc, comparer)
        {
        }

        public override ElementaryConclusion Validate(PropertyValidatorContext context)
        {
            object comparisonValue = this.comparisonValueFunc == null ?
                this.comparisonValue : this.comparisonValueFunc(context.ObjectToValidate);

            return this.IsEqual(context.PropertyToValidate, comparisonValue) ?
                new ElementaryConclusion(isValid: true) : new ElementaryConclusion(isValid: false,
                    this.FailureMessage ?? DEFAULT_FAILURE_MESSAGE + comparisonValue);
        }
    }
}