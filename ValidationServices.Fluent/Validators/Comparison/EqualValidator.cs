using System;
using System.Collections;
using System.Linq.Expressions;
using ValidationServices.Fluent.Internal;
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
            this PropertyValidationRule<TOwner, TProperty> rule, Expression<Func<TOwner, TProperty>> valueToCompare,
            IComparer equalityComparer = null)
        {
            valueToCompare.Guard(nameof(valueToCompare));
            rule.SetPropertyValidator(new EqualValidator(valueToCompare.Compile().CoerceToNonGeneric(),
                valueToCompare.Body.ToString(), equalityComparer));
            return rule;
        }
    }

    public class EqualValidator : AbstractEqualValidator
    {
        private readonly string funcBodyString;

        public static string DefaultFailureMessage { get; } = "This value must be equal to ";

        public EqualValidator(object comparisonValue, IComparer comparer = null) : base(comparisonValue, comparer)
        {
        }

        public EqualValidator(Func<object, object> comparisonValueFunc, string funcBodyString, 
            IComparer comparer = null) : base(comparisonValueFunc, comparer)
        {
            this.funcBodyString = funcBodyString;
        }

        public override ElementaryConclusion Validate(PropertyValidatorContext context)
        {
            object comparisonValue = this.comparisonValueFunc == null ?
                this.comparisonValue : this.comparisonValueFunc(context.ObjectToValidate);

            return this.IsEqual(context.PropertyToValidate, comparisonValue) ?
                new ElementaryConclusion(isValid: true) : new ElementaryConclusion(isValid: false,
                    this.FailureMessage ?? DefaultFailureMessage + 
                    (this.comparisonValueFunc != null ? this.funcBodyString : comparisonValue.ToString()));
        }
    }
}