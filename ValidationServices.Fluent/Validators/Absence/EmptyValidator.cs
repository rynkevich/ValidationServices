using System;
using ValidationServices.Fluent.Rules;
using ValidationServices.Results;

namespace ValidationServices.Fluent.Validators.Absence
{
    public static class EmptyRuleExtension
    {
        public static PropertyValidationRule<TOwner, TProperty> Empty<TOwner, TProperty>(
            this PropertyValidationRule<TOwner, TProperty> rule)
        {
            rule.SetPropertyValidator(new EmptyValidator(default(TProperty)));
            return rule;
        }
    }

    public class EmptyValidator : IPropertyValidator
    {
        private readonly object defaultValueForType;

        public string FailureMessage { get; set; } = "This property must be empty";

        public EmptyValidator(object defaultValueForType)
        {
            this.defaultValueForType = defaultValueForType;
        }

        public ElementaryConclusion Validate(PropertyValidatorContext context)
        {
            if (!(context.PropertyToValidate == null
                || AbsenceValidationRoutines.IsEmptyString(context.PropertyToValidate)
                || AbsenceValidationRoutines.IsEmptyCollection(context.PropertyToValidate)
                || Equals(context.PropertyToValidate, this.defaultValueForType)))
            {
                return new ElementaryConclusion(isValid: false, this.FailureMessage);
            }

            return new ElementaryConclusion(isValid: true);
        }
    }
}
