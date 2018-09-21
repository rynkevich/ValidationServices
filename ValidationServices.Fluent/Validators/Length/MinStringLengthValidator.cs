using System;
using ValidationServices.Fluent.Internal;
using ValidationServices.Fluent.Rules;

namespace ValidationServices.Fluent.Validators.Length
{
    public static class MinStringLengthRuleExtension
    {
        public static PropertyValidationRule<TOwner, string> MinLength<TOwner>(
            this PropertyValidationRule<TOwner, string> rule, int min)
        {
            rule.SetPropertyValidator(new MinStringLengthValidator(min));
            return rule;
        }

        public static PropertyValidationRule<TOwner, string> MinLength<TOwner>(
            this PropertyValidationRule<TOwner, string> rule, Func<TOwner, int> min)
        {
            rule.SetPropertyValidator(new MinStringLengthValidator(min.CoerceToNonGeneric()));
            return rule;
        }
    }

    public class MinStringLengthValidator : StringLengthValidator
    {
        public MinStringLengthValidator(int min) : base(min, MAX_NOT_SPECIFIED)
        {
        }

        public MinStringLengthValidator(Func<object, int> minFunc) : base(minFunc, obj => MAX_NOT_SPECIFIED)
        {
        }
    }
}
