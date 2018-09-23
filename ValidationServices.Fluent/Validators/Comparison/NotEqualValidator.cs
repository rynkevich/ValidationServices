using System;
using System.Collections;
using System.Linq.Expressions;
using ValidationServices.Fluent.Internal;
using ValidationServices.Fluent.Rules;
using ValidationServices.Results;

namespace ValidationServices.Fluent.Validators.Comparison
{
    public static class NotEqualRuleExtension
    {
        public static PropertyValidationRule<TOwner, TProperty> NotEqual<TOwner, TProperty>(
            this PropertyValidationRule<TOwner, TProperty> rule, TProperty valueToCompare,
            IComparer equalityComparer = null)
        {
            rule.SetPropertyValidator(new NotEqualValidator(valueToCompare, equalityComparer));
            return rule;
        }

        public static PropertyValidationRule<TOwner, TProperty> NotEqual<TOwner, TProperty>(
            this PropertyValidationRule<TOwner, TProperty> rule, Expression<Func<TOwner, TProperty>> valueToCompare,
            IComparer equalityComparer = null)
        {
            valueToCompare.Guard(nameof(valueToCompare));
            rule.SetPropertyValidator(new NotEqualValidator(valueToCompare.Compile().CoerceToNonGeneric(), 
                valueToCompare.Body.ToString(), equalityComparer));
            return rule;
        }
    }

    public class NotEqualValidator : AbstractEqualValidator
    {
        private readonly string funcBodyString;

        public static string DefaultFailureMessage { get; } = "This value must not be equal to ";

        public NotEqualValidator(object comparisonValue, IComparer comparer = null) : base(comparisonValue, comparer)
        {
        }

        public NotEqualValidator(Func<object, object> comparisonValueFunc, string funcBodyString, 
            IComparer comparer = null) : base(comparisonValueFunc, comparer)
        {
            this.funcBodyString = funcBodyString;
        }

        public override ElementaryConclusion Validate(PropertyValidatorContext context)
        {
            object comparisonValue = this.comparisonValueFunc == null ?
                this.comparisonValue : this.comparisonValueFunc(context.ObjectToValidate);

            return !this.IsEqual(context.PropertyToValidate, comparisonValue) ?
                new ElementaryConclusion(isValid: true) : new ElementaryConclusion(isValid: false,
                    this.FailureMessage ?? DefaultFailureMessage +
                    (this.comparisonValueFunc != null ? this.funcBodyString : comparisonValue.ToString()));
        }
    }
}
