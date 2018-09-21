using System;
using ValidationServices.Fluent.Internal;
using ValidationServices.Fluent.Rules;

namespace ValidationServices.Fluent.Validators.Length
{
    public static class MaxStringLengthRuleExtension
    {
        public static PropertyValidationRule<TOwner, string> MaxLength<TOwner>(
            this PropertyValidationRule<TOwner, string> rule, int max)
        {
            rule.SetPropertyValidator(new MaxStringLengthValidator(max));
            return rule;
        }

        public static PropertyValidationRule<TOwner, string> MaxLength<TOwner>(
            this PropertyValidationRule<TOwner, string> rule, Func<TOwner, int> max)
        {
            rule.SetPropertyValidator(new MaxStringLengthValidator(max.CoerceToNonGeneric()));
            return rule;
        }
    }

    public class MaxStringLengthValidator : StringLengthValidator
    {
        public MaxStringLengthValidator(int max) : base(0, max)
        {
        }

        public MaxStringLengthValidator(Func<object, int> maxFunc) : base(obj => 0, maxFunc)
        {
        }
    }
}
