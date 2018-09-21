using System;
using ValidationServices.Fluent.Internal;
using ValidationServices.Fluent.Rules;
using ValidationServices.Results;

namespace ValidationServices.Fluent.Validators.Comparison
{
    public static class GreaterThanOrEqualRuleExtension
    {
        public static PropertyValidationRule<TOwner, TProperty> GreaterThanOrEqual<TOwner, TProperty>(
            this PropertyValidationRule<TOwner, TProperty> rule, TProperty valueToCompare)
            where TProperty : IComparable<TProperty>, IComparable
        {
            rule.SetPropertyValidator(new GreaterThanOrEqualValidator(valueToCompare));
            return rule;
        }

        public static PropertyValidationRule<TOwner, TProperty> GreaterThanOrEqual<TOwner, TProperty>(
            this PropertyValidationRule<TOwner, TProperty> rule, Func<TOwner, TProperty> valueToCompare)
            where TProperty : IComparable<TProperty>, IComparable
        {
            rule.SetPropertyValidator(new GreaterThanOrEqualValidator(valueToCompare.CoerceToNonGeneric()));
            return rule;
        }
    }

    public class GreaterThanOrEqualValidator : AbstractComparisonValidator
    {
        private static readonly string DEFAULT_FAILURE_MESSAGE = "This value must be greater than or equal ";

        public GreaterThanOrEqualValidator(IComparable comparisonValue) : base(comparisonValue)
        {
        }

        public GreaterThanOrEqualValidator(Func<object, object> comparisonValueFunc) : base(comparisonValueFunc)
        {
        }

        public override ElementaryConclusion Validate(object objectToValidate, object propertyToValidate)
        {
            if (objectToValidate == null)
            {
                throw new ArgumentNullException(nameof(objectToValidate));
            }

            if (!(propertyToValidate is IComparable))
            {
                throw new ArgumentException("Object to validate must implement IComparable interface");
            }

            object comparisonValue = this.comparisonValueFunc == null ?
                this.comparisonValue : this.comparisonValueFunc(objectToValidate);

            return this.IsGreaterThanOrEqual(propertyToValidate as IComparable, comparisonValue as IComparable) ?
                new ElementaryConclusion(isValid: true) :
                new ElementaryConclusion(isValid: false, this.FailureMessage ?? DEFAULT_FAILURE_MESSAGE + comparisonValue);
        }

        private bool IsGreaterThanOrEqual(IComparable objectToValidate, IComparable comparisonValue)
        {
            return objectToValidate.CompareTo(comparisonValue) >= 0;
        }
    }
}
