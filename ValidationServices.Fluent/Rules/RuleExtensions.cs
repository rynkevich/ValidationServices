using System;
using System.Collections;
using ValidationServices.Fluent.Internal;
using ValidationServices.Fluent.Validators.Absence;
using ValidationServices.Fluent.Validators.Comparison;
using ValidationServices.Fluent.Validators.Length;

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

        public static PropertyValidationRule<TOwner, TProperty> Equal<TOwner, TProperty>(
            this PropertyValidationRule<TOwner, TProperty> rule, TProperty valueToCompare, 
            IComparer equalityComparer = null)
        {
            rule.SetPropertyValidator(new EqualValidator(valueToCompare, equalityComparer));
            return rule;
        }

        public static PropertyValidationRule<TOwner, TProperty> Equal<TOwner, TProperty>(
            this PropertyValidationRule<TOwner, TProperty> rule, Func<TOwner, TProperty> valueToCompare,
            IComparer equalityComparer = null)
        {
            rule.SetPropertyValidator(new EqualValidator(valueToCompare, equalityComparer));
            return rule;
        }

        public static PropertyValidationRule<TOwner, TProperty> NotEqual<TOwner, TProperty>(
            this PropertyValidationRule<TOwner, TProperty> rule, TProperty valueToCompare,
            IComparer equalityComparer = null)
        {
            rule.SetPropertyValidator(new NotEqualValidator(valueToCompare, equalityComparer));
            return rule;
        }

        public static PropertyValidationRule<TOwner, TProperty> NotEqual<TOwner, TProperty>(
            this PropertyValidationRule<TOwner, TProperty> rule, Func<TOwner, TProperty> valueToCompare,
            IComparer equalityComparer = null)
        {
            rule.SetPropertyValidator(new NotEqualValidator(valueToCompare, equalityComparer));
            return rule;
        }

        public static PropertyValidationRule<TOwner, TProperty> GreaterThan<TOwner, TProperty>(
            this PropertyValidationRule<TOwner, TProperty> rule, TProperty valueToCompare)
            where TProperty : IComparable<TProperty>, IComparable
        {
            rule.SetPropertyValidator(new GreaterThanValidator(valueToCompare));
            return rule;
        }

        public static PropertyValidationRule<TOwner, TProperty> GreaterThan<TOwner, TProperty>(
            this PropertyValidationRule<TOwner, TProperty> rule, Func<TOwner, TProperty> valueToCompare)
            where TProperty : IComparable<TProperty>, IComparable
        {
            rule.SetPropertyValidator(new GreaterThanValidator(valueToCompare.CoerceToNonGeneric()));
            return rule;
        }

        public static PropertyValidationRule<TOwner, TProperty> GreaterThanOrEqual<TOwner, TProperty>(
            this PropertyValidationRule<TOwner, TProperty> rule, TProperty valueToCompare)
            where TProperty : IComparable<TProperty>, IComparable
        {
            rule.SetPropertyValidator(new GreaterThanOrEqualValidator(valueToCompare));
            return rule;
        }

        public static PropertyValidationRule<TOwner, TProperty> GreaterThanOrEqual<TOwner, TProperty>(
            this PropertyValidationRule<TOwner, TProperty> rule, Func<TOwner, TProperty> valueToCompare)
            where TProperty : IComparable<TProperty>, IComparable
        {
            rule.SetPropertyValidator(new GreaterThanOrEqualValidator(valueToCompare.CoerceToNonGeneric()));
            return rule;
        }

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

        public static PropertyValidationRule<TOwner, TProperty> LessThanOrEqual<TOwner, TProperty>(
            this PropertyValidationRule<TOwner, TProperty> rule, TProperty valueToCompare)
            where TProperty : IComparable<TProperty>, IComparable
        {
            rule.SetPropertyValidator(new LessThanOrEqualValidator(valueToCompare));
            return rule;
        }

        public static PropertyValidationRule<TOwner, TProperty> LessThanOrEqual<TOwner, TProperty>(
            this PropertyValidationRule<TOwner, TProperty> rule, Func<TOwner, TProperty> valueToCompare)
            where TProperty : IComparable<TProperty>, IComparable
        {
            rule.SetPropertyValidator(new LessThanOrEqualValidator(valueToCompare.CoerceToNonGeneric()));
            return rule;
        }

        public static PropertyValidationRule<TOwner, string> Length<TOwner>(
            this PropertyValidationRule<TOwner, string> rule, int min, int max)
        {
            rule.SetPropertyValidator(new StringLengthValidator(min, max));
            return rule;
        }

        public static PropertyValidationRule<TOwner, string> Length<TOwner>(
            this PropertyValidationRule<TOwner, string> rule, Func<TOwner, int> min, Func<TOwner, int> max)
        {
            rule.SetPropertyValidator(new StringLengthValidator(min.CoerceToNonGeneric(), max.CoerceToNonGeneric()));
            return rule;
        }

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

        public static PropertyValidationRule<TOwner, TProperty> WithMessage<TOwner, TProperty>(
            this PropertyValidationRule<TOwner, TProperty> rule, string message)
        {
            rule.SetLastValidatorFailureMessage(message);
            return rule;
        }
    }
}
