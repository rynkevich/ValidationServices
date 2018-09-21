using System;
using ValidationServices.Fluent.Internal;
using ValidationServices.Fluent.Rules;
using ValidationServices.Results;

namespace ValidationServices.Fluent.Validators.Comparison
{
    public static class GreaterThanRuleExtension
    {
        public static PropertyValidationRule<TOwner, TProperty> GreaterThan<TOwner, TProperty>(
            this PropertyValidationRule<TOwner, TProperty> rule, TProperty valueToCompare)
            where TProperty : IComparable<TProperty>, IComparable
        {
            rule.SetPropertyValidator(new GreaterThanValidator(valueToCompare));
            return rule;
        }

        public static PropertyValidationRule<TOwner, TProperty> GreaterThan<TOwner, TProperty>(
            this PropertyValidationRule<TOwner, TProperty> rule, Func<TOwner, TProperty> valueToCompare)
            where TProperty : IComparable<TProperty>, IComparable
        {
            rule.SetPropertyValidator(new GreaterThanValidator(valueToCompare.CoerceToNonGeneric()));
            return rule;
        }
    }

    public class GreaterThanValidator : AbstractComparisonValidator
    {
        private static readonly string DEFAULT_FAILURE_MESSAGE = "This value must be greater than ";

        public GreaterThanValidator(IComparable comparisonValue) : base(comparisonValue)
        {
        }

        public GreaterThanValidator(Func<object, object> comparisonValueFunc) : base(comparisonValueFunc)
        {
        }

        public override ElementaryConclusion Validate(PropertyValidatorContext context)
        {
            if (!(context.PropertyToValidate is IComparable))
            {
                throw new ArgumentException("Object to validate must implement IComparable interface");
            }

            object comparisonValue = this.comparisonValueFunc == null ?
                this.comparisonValue : this.comparisonValueFunc(context.ObjectToValidate);

            return this.IsGreaterThan(context.PropertyToValidate as IComparable, comparisonValue as IComparable) ?
                new ElementaryConclusion(isValid: true) :
                new ElementaryConclusion(isValid: false, this.FailureMessage ?? DEFAULT_FAILURE_MESSAGE + comparisonValue);
        }

        private bool IsGreaterThan(IComparable objectToValidate, IComparable comparisonValue)
        {
            return objectToValidate.CompareTo(comparisonValue) > 0;
        }
    }
}
