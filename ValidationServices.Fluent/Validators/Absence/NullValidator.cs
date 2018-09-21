using System;
using ValidationServices.Fluent.Rules;
using ValidationServices.Results;

namespace ValidationServices.Fluent.Validators.Absence
{
    public static class NullRuleExtension
    {
        public static PropertyValidationRule<TOwner, TProperty> Null<TOwner, TProperty>(
            this PropertyValidationRule<TOwner, TProperty> rule)
        {
            rule.SetPropertyValidator(new NullValidator());
            return rule;
        }
    }

    public class NullValidator : IPropertyValidator
    {
        public string FailureMessage { get; set; } = "This property must be null";

        public ElementaryConclusion Validate(object objectToValidate, object propertyToValidate)
        {
            if (objectToValidate == null)
            {
                throw new ArgumentNullException(nameof(objectToValidate));
            }

            if (propertyToValidate != null)
            {
                return new ElementaryConclusion(isValid: false, this.FailureMessage);
            }

            return new ElementaryConclusion(isValid: true);
        }
    }
}
