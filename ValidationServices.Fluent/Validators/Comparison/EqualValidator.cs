using System;
using System.Collections;
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

        public override ElementaryConclusion Validate(object objectToValidate, object propertyToValidate)
        {
            if (objectToValidate == null)
            {
                throw new ArgumentNullException(nameof(objectToValidate));
            }

            object comparisonValue = this.comparisonValueFunc == null ?
                this.comparisonValue : this.comparisonValueFunc(objectToValidate);

            return this.IsEqual(propertyToValidate, comparisonValue) ?
                new ElementaryConclusion(isValid: true) : new ElementaryConclusion(isValid: false,
                    this.FailureMessage ?? DEFAULT_FAILURE_MESSAGE + comparisonValue);
        }
    }
}