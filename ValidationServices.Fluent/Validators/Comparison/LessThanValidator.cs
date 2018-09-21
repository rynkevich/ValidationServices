using System;
using ValidationServices.Fluent.Internal;
using ValidationServices.Fluent.Rules;
using ValidationServices.Results;

namespace ValidationServices.Fluent.Validators.Comparison
{
    public static class LessThanRuleExtension
    {
        public static PropertyValidationRule<TOwner, TProperty> LessThan<TOwner, TProperty>(
            this PropertyValidationRule<TOwner, TProperty> rule, TProperty valueToCompare)
            where TProperty : IComparable<TProperty>, IComparable
        {
            rule.SetPropertyValidator(new LessThanValidator(valueToCompare));
            return rule;
        }

        public static PropertyValidationRule<TOwner, TProperty> LessThan<TOwner, TProperty>(
            this PropertyValidationRule<TOwner, TProperty> rule, Func<TOwner, TProperty> valueToCompare)
            where TProperty : IComparable<TProperty>, IComparable
        {
            rule.SetPropertyValidator(new LessThanValidator(valueToCompare.CoerceToNonGeneric()));
            return rule;
        }
    }

    public class LessThanValidator : AbstractComparisonValidator
    {
        private static readonly string DEFAULT_FAILURE_MESSAGE = "This value must be less than ";

        public LessThanValidator(IComparable comparisonValue) : base(comparisonValue)
        {
        }

        public LessThanValidator(Func<object, object> comparisonValueFunc) : base(comparisonValueFunc)
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

            return this.IsLessThan(context.PropertyToValidate as IComparable, comparisonValue as IComparable) ?
                new ElementaryConclusion(isValid: true) :
                new ElementaryConclusion(isValid: false, this.FailureMessage ?? DEFAULT_FAILURE_MESSAGE + comparisonValue);
        }

        private bool IsLessThan(IComparable objectToValidate, IComparable comparisonValue)
        {
            return objectToValidate.CompareTo(comparisonValue) < 0;
        }
    }
}
