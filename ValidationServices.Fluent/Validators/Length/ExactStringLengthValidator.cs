using System;
using ValidationServices.Fluent.Internal;
using ValidationServices.Fluent.Rules;

namespace ValidationServices.Fluent.Validators.Length
{
    public static class ExactStringLengthRuleExtension
    {
        public static PropertyValidationRule<TOwner, string> Length<TOwner>(
            this PropertyValidationRule<TOwner, string> rule, int exactLength)
        {
            rule.SetPropertyValidator(new ExactStringLengthValidator(exactLength));
            return rule;
        }

        public static PropertyValidationRule<TOwner, string> Length<TOwner>(
            this PropertyValidationRule<TOwner, string> rule, Func<TOwner, int> exactLength)
        {
            rule.SetPropertyValidator(new ExactStringLengthValidator(exactLength.CoerceToNonGeneric()));
            return rule;
        }
    }

    public class ExactStringLengthValidator : StringLengthValidator
    {
        public ExactStringLengthValidator(int length) : base(length, length)
        {
        }

        public ExactStringLengthValidator(Func<object, int> length) : base(length, length)
        {
        }
    }
}
