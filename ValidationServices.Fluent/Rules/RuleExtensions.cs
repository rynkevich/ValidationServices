using System;
using ValidationServices.Fluent.Internal;
using ValidationServices.Fluent.Validators.State;
using ValidationServices.Fluent.Validators.StringLength;

namespace ValidationServices.Fluent.Rules
{
    public static class RuleExtensions
    {
        public static PropertyValidationRule<TOwner, TProperty> Null<TOwner, TProperty>(
            this PropertyValidationRule<TOwner, TProperty> rule)
        {
            rule.SetPropertyValidator(new NullValidator());
            return rule;
        }

        public static PropertyValidationRule<TOwner, TProperty> NotNull<TOwner, TProperty>(
            this PropertyValidationRule<TOwner, TProperty> rule)
        {
            rule.SetPropertyValidator(new NotNullValidator());
            return rule;
        }

        public static PropertyValidationRule<TOwner, TProperty> Empty<TOwner, TProperty>(
            this PropertyValidationRule<TOwner, TProperty> rule)
        {
            rule.SetPropertyValidator(new EmptyValidator(default(TProperty)));
            return rule;
        }

        public static PropertyValidationRule<TOwner, TProperty> NotEmpty<TOwner, TProperty>(
            this PropertyValidationRule<TOwner, TProperty> rule)
        {
            rule.SetPropertyValidator(new NotEmptyValidator(default(TProperty)));
            return rule;
        }

        public static PropertyValidationRule<TOwner, string> Length<TOwner>(
            this PropertyValidationRule<TOwner, string> rule, int min, int max)
        {
            rule.SetPropertyValidator(new LengthValidator(min, max));
            return rule;
        }

        public static PropertyValidationRule<TOwner, string> Length<TOwner>(
            this PropertyValidationRule<TOwner, string> rule, Func<TOwner, int> min, Func<TOwner, int> max)
        {
            rule.SetPropertyValidator(new LengthValidator(min.CoerceToNonGeneric(), max.CoerceToNonGeneric()));
            return rule;
        }

        public static PropertyValidationRule<TOwner, string> Length<TOwner>(
            this PropertyValidationRule<TOwner, string> rule, int exactLength)
        {
            rule.SetPropertyValidator(new ExactLengthValidator(exactLength));
            return rule;
        }

        public static PropertyValidationRule<TOwner, string> Length<TOwner>(
            this PropertyValidationRule<TOwner, string> rule, Func<TOwner, int> exactLength)
        {
            rule.SetPropertyValidator(new ExactLengthValidator(exactLength.CoerceToNonGeneric()));
            return rule;
        }

        public static PropertyValidationRule<TOwner, string> MaxLength<TOwner>(
            this PropertyValidationRule<TOwner, string> rule, int max)
        {
            rule.SetPropertyValidator(new MaxLengthValidator(max));
            return rule;
        }

        public static PropertyValidationRule<TOwner, string> MaxLength<TOwner>(
            this PropertyValidationRule<TOwner, string> rule, Func<TOwner, int> max)
        {
            rule.SetPropertyValidator(new MaxLengthValidator(max.CoerceToNonGeneric()));
            return rule;
        }

        public static PropertyValidationRule<TOwner, string> MinLength<TOwner>(
            this PropertyValidationRule<TOwner, string> rule, int min)
        {
            rule.SetPropertyValidator(new MinLengthValidator(min));
            return rule;
        }

        public static PropertyValidationRule<TOwner, string> MinLength<TOwner>(
            this PropertyValidationRule<TOwner, string> rule, Func<TOwner, int> min)
        {
            rule.SetPropertyValidator(new MinLengthValidator(min.CoerceToNonGeneric()));
            return rule;
        }

        public static PropertyValidationRule<TOwner, TProperty> WithMessage<TOwner, TProperty>(
            this PropertyValidationRule<TOwner, TProperty> rule, string message)
        {
            rule.SetLastValidatorFailureMessage(message);
            return rule;
        }
    }
}
