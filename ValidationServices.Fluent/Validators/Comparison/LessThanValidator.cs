using System;
using System.Linq.Expressions;
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
            this PropertyValidationRule<TOwner, TProperty> rule, Expression<Func<TOwner, TProperty>> valueToCompare)
            where TProperty : IComparable<TProperty>, IComparable
        {
            valueToCompare.Guard(nameof(valueToCompare));
            rule.SetPropertyValidator(new LessThanValidator(valueToCompare.Compile().CoerceToNonGeneric(),
                valueToCompare.Body.ToString()));
            return rule;
        }
    }

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
