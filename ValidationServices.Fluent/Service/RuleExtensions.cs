using System;
using System.Collections;
using System.Linq.Expressions;
using ValidationServices.Fluent.Internal;
using ValidationServices.Fluent.Rules;
using ValidationServices.Fluent.Validators.Absence;
using ValidationServices.Fluent.Validators.Comparison;
using ValidationServices.Fluent.Validators.Length;

namespace ValidationServices.Fluent.Service
{
    /// <summary>
    /// Extension methods that provide set of validators.
    /// </summary>
    public static class RuleExtensions
    {
        /// <summary>
        /// Adds an 'empty' validator to current validation rule.
        /// Validation will fail if the property is not null, an empty or the default value for the type (for example, 0 for integers)
        /// </summary>
        /// <typeparam name="TOwner">The type of object being validated</typeparam>
        /// <typeparam name="TProperty">The type of property being validated</typeparam>
        /// <param name="rule">The rule to which the validator should be set</param>
        /// <returns><paramref name="rule"/> with new property validator</returns>
        public static PropertyValidationRule<TOwner, TProperty> Empty<TOwner, TProperty>(
            this PropertyValidationRule<TOwner, TProperty> rule)
        {
            rule.SetPropertyValidator(new EmptyValidator(default(TProperty)));
            return rule;
        }

        /// <summary>
        /// Adds a 'not empty' validator to current validation rule.
        /// Validation will fail if the property is null, an empty or the default value for the type (for example, 0 for integers)
        /// </summary>
        /// <typeparam name="TOwner">The type of object being validated</typeparam>
        /// <typeparam name="TProperty">The type of property being validated</typeparam>
        /// <param name="rule">The rule to which the validator should be set</param>
        /// <returns><paramref name="rule"/> with new property validator</returns>
        public static PropertyValidationRule<TOwner, TProperty> NotEmpty<TOwner, TProperty>(
            this PropertyValidationRule<TOwner, TProperty> rule)
        {
            rule.SetPropertyValidator(new NotEmptyValidator(default(TProperty)));
            return rule;
        }

        /// <summary>
        /// Adds a 'null' validator to current validation rule.
        /// Validation will fail if the property is not null.
        /// </summary>
        /// <typeparam name="TOwner">The type of object being validated</typeparam>
        /// <typeparam name="TProperty">The type of property being validated</typeparam>
        /// <param name="rule">The rule to which the validator should be set</param>
        /// <returns><paramref name="rule"/> with new property validator</returns>
        public static PropertyValidationRule<TOwner, TProperty> Null<TOwner, TProperty>(
            this PropertyValidationRule<TOwner, TProperty> rule)
        {
            rule.SetPropertyValidator(new NullValidator());
            return rule;
        }

        /// <summary>
        /// Adds a 'not null' validator to current validation rule.
        /// Validation will fail if the property is null.
        /// </summary>
        /// <typeparam name="TOwner">The type of object being validated</typeparam>
        /// <typeparam name="TProperty">The type of property being validated</typeparam>
        /// <param name="rule">The rule to which the validator should be set</param>
        /// <returns><paramref name="rule"/> with new property validator</returns>
        public static PropertyValidationRule<TOwner, TProperty> NotNull<TOwner, TProperty>(
            this PropertyValidationRule<TOwner, TProperty> rule)
        {
            rule.SetPropertyValidator(new NotNullValidator());
            return rule;
        }

        /// <summary>
        /// Adds an 'equal' validator to current validation rule.
        /// Validation will fail if the specified value is not equal to the value of the property.
        /// </summary>
        /// <typeparam name="TOwner">The type of object being validated</typeparam>
        /// <typeparam name="TProperty">The type of property being validated</typeparam>
        /// <param name="rule">The rule to which the validator should be set</param>
        /// <param name="valueToCompare">The value to compare</param>
        /// <param name="equalityComparer">The comparer to use</param>
        /// <returns><paramref name="rule"/> with new property validator</returns>
        public static PropertyValidationRule<TOwner, TProperty> Equal<TOwner, TProperty>(
           this PropertyValidationRule<TOwner, TProperty> rule, TProperty valueToCompare,
           IComparer equalityComparer = null)
        {
            rule.SetPropertyValidator(new EqualValidator(valueToCompare, equalityComparer));
            return rule;
        }

        /// <summary>
        /// Adds an 'equal' validator to current validation rule.
        /// Validation will fail if the specified value is not equal to the value of the property.
        /// </summary>
        /// <typeparam name="TOwner">The type of object being validated</typeparam>
        /// <typeparam name="TProperty">The type of property being validated</typeparam>
        /// <param name="rule">The rule to which the validator should be set</param>
        /// <param name="valueToCompare">The lambda expression to provide the comparison value</param>
        /// <param name="equalityComparer">The comparer to use</param>
        /// <returns><paramref name="rule"/> with new property validator</returns>
        public static PropertyValidationRule<TOwner, TProperty> Equal<TOwner, TProperty>(
            this PropertyValidationRule<TOwner, TProperty> rule, Expression<Func<TOwner, TProperty>> valueToCompare,
            IComparer equalityComparer = null)
        {
            valueToCompare.Guard(nameof(valueToCompare));
            rule.SetPropertyValidator(new EqualValidator(valueToCompare.Compile().CoerceToNonGeneric(),
                valueToCompare.Body.ToString(), equalityComparer));
            return rule;
        }

        /// <summary>
        /// Adds a 'not equal' validator to current validation rule.
        /// Validation will fail if the specified value is equal to the value of the property.
        /// </summary>
        /// <typeparam name="TOwner">The type of object being validated</typeparam>
        /// <typeparam name="TProperty">The type of property being validated</typeparam>
        /// <param name="rule">The rule to which the validator should be set</param>
        /// <param name="valueToCompare">The value to compare</param>
        /// <param name="equalityComparer">The comparer to use</param>
        /// <returns><paramref name="rule"/> with new property validator</returns>
        public static PropertyValidationRule<TOwner, TProperty> NotEqual<TOwner, TProperty>(
            this PropertyValidationRule<TOwner, TProperty> rule, TProperty valueToCompare,
            IComparer equalityComparer = null)
        {
            rule.SetPropertyValidator(new NotEqualValidator(valueToCompare, equalityComparer));
            return rule;
        }

        /// <summary>
        /// Adds a 'not equal' validator to current validation rule.
        /// Validation will fail if the specified value is equal to the value of the property.
        /// </summary>
        /// <typeparam name="TOwner">The type of object being validated</typeparam>
        /// <typeparam name="TProperty">The type of property being validated</typeparam>
        /// <param name="rule">The rule to which the validator should be set</param>
        /// <param name="valueToCompare">The lambda expression to provide the comparison value</param>
        /// <param name="equalityComparer">The comparer to use</param>
        /// <returns><paramref name="rule"/> with new property validator</returns>
        public static PropertyValidationRule<TOwner, TProperty> NotEqual<TOwner, TProperty>(
            this PropertyValidationRule<TOwner, TProperty> rule, Expression<Func<TOwner, TProperty>> valueToCompare,
            IComparer equalityComparer = null)
        {
            valueToCompare.Guard(nameof(valueToCompare));
            rule.SetPropertyValidator(new NotEqualValidator(valueToCompare.Compile().CoerceToNonGeneric(),
                valueToCompare.Body.ToString(), equalityComparer));
            return rule;
        }

        /// <summary>
        /// Adds a 'greater than or equal' validator to current validation rule.
        /// Validation will succeed if the property value is greater than or equal to the specified value, otherwise it will fail.
        /// </summary>
        /// <typeparam name="TOwner">The type of object being validated</typeparam>
        /// <typeparam name="TProperty">The type of property being validated</typeparam>
        /// <param name="rule">The rule to which the validator should be set</param>
        /// <param name="valueToCompare">The value to compare</param>
        /// <returns><paramref name="rule"/> with new property validator</returns>
        public static PropertyValidationRule<TOwner, TProperty> GreaterThanOrEqual<TOwner, TProperty>(
            this PropertyValidationRule<TOwner, TProperty> rule, TProperty valueToCompare)
            where TProperty : IComparable<TProperty>, IComparable
        {
            rule.SetPropertyValidator(new GreaterThanOrEqualValidator(valueToCompare));
            return rule;
        }

        /// <summary>
        /// Adds a 'greater than or equal' validator to current validation rule.
        /// Validation will succeed if the property value is greater than or equal to the specified value, otherwise it will fail.
        /// </summary>
        /// <typeparam name="TOwner">The type of object being validated</typeparam>
        /// <typeparam name="TProperty">The type of property being validated</typeparam>
        /// <param name="rule">The rule to which the validator should be set</param>
        /// <param name="valueToCompare">The lambda expression to provide the comparison value</param>
        /// <returns><paramref name="rule"/> with new property validator</returns>
        public static PropertyValidationRule<TOwner, TProperty> GreaterThanOrEqual<TOwner, TProperty>(
            this PropertyValidationRule<TOwner, TProperty> rule, Expression<Func<TOwner, TProperty>> valueToCompare)
            where TProperty : IComparable<TProperty>, IComparable
        {
            valueToCompare.Guard(nameof(valueToCompare));
            rule.SetPropertyValidator(new GreaterThanOrEqualValidator(valueToCompare.Compile().CoerceToNonGeneric(),
                valueToCompare.Body.ToString()));
            return rule;
        }

        /// <summary>
        /// Adds a 'greater than' validator to current validation rule.
        /// Validation will succeed if the property value is greater than the specified value, otherwise it will fail.
        /// </summary>
        /// <typeparam name="TOwner">The type of object being validated</typeparam>
        /// <typeparam name="TProperty">The type of property being validated</typeparam>
        /// <param name="rule">The rule to which the validator should be set</param>
        /// <param name="valueToCompare">The value to compare</param>
        /// <returns><paramref name="rule"/> with new property validator</returns>
        public static PropertyValidationRule<TOwner, TProperty> GreaterThan<TOwner, TProperty>(
            this PropertyValidationRule<TOwner, TProperty> rule, TProperty valueToCompare)
            where TProperty : IComparable<TProperty>, IComparable
        {
            rule.SetPropertyValidator(new GreaterThanValidator(valueToCompare));
            return rule;
        }

        /// <summary>
        /// Adds a 'greater than' validator to current validation rule.
        /// Validation will succeed if the property value is greater than the specified value, otherwise it will fail.
        /// </summary>
        /// <typeparam name="TOwner">The type of object being validated</typeparam>
        /// <typeparam name="TProperty">The type of property being validated</typeparam>
        /// <param name="rule">The rule to which the validator should be set</param>
        /// <param name="valueToCompare">The lambda expression to provide the comparison value</param>
        /// <returns><paramref name="rule"/> with new property validator</returns>
        public static PropertyValidationRule<TOwner, TProperty> GreaterThan<TOwner, TProperty>(
            this PropertyValidationRule<TOwner, TProperty> rule, Expression<Func<TOwner, TProperty>> valueToCompare)
            where TProperty : IComparable<TProperty>, IComparable
        {
            valueToCompare.Guard(nameof(valueToCompare));
            rule.SetPropertyValidator(new GreaterThanValidator(valueToCompare.Compile().CoerceToNonGeneric(),
                valueToCompare.Body.ToString()));
            return rule;
        }

        /// <summary>
        /// Adds a 'less than or equal' validator to current validation rule.
        /// Validation will succeed if the property value is less than or equal to the specified value, otherwise it will fail.
        /// </summary>
        /// <typeparam name="TOwner">The type of object being validated</typeparam>
        /// <typeparam name="TProperty">The type of property being validated</typeparam>
        /// <param name="rule">The rule to which the validator should be set</param>
        /// <param name="valueToCompare">The value to compare</param>
        /// <returns><paramref name="rule"/> with new property validator</returns>
        public static PropertyValidationRule<TOwner, TProperty> LessThanOrEqual<TOwner, TProperty>(
            this PropertyValidationRule<TOwner, TProperty> rule, TProperty valueToCompare)
            where TProperty : IComparable<TProperty>, IComparable
        {
            rule.SetPropertyValidator(new LessThanOrEqualValidator(valueToCompare));
            return rule;
        }

        /// <summary>
        /// Adds a 'less than or equal' validator to current validation rule.
        /// Validation will succeed if the property value is less than or equal to the specified value, otherwise it will fail.
        /// </summary>
        /// <typeparam name="TOwner">The type of object being validated</typeparam>
        /// <typeparam name="TProperty">The type of property being validated</typeparam>
        /// <param name="rule">The rule to which the validator should be set</param>
        /// <param name="valueToCompare">The lambda expression to provide the comparison value</param>
        /// <returns><paramref name="rule"/> with new property validator</returns>
        public static PropertyValidationRule<TOwner, TProperty> LessThanOrEqual<TOwner, TProperty>(
            this PropertyValidationRule<TOwner, TProperty> rule, Expression<Func<TOwner, TProperty>> valueToCompare)
            where TProperty : IComparable<TProperty>, IComparable
        {
            valueToCompare.Guard(nameof(valueToCompare));
            rule.SetPropertyValidator(new LessThanOrEqualValidator(valueToCompare.Compile().CoerceToNonGeneric(),
                valueToCompare.Body.ToString()));
            return rule;
        }

        /// <summary>
        /// Adds a 'less than' validator to current validation rule.
        /// Validation will succeed if the property value is less than the specified value, otherwise it will fail.
        /// </summary>
        /// <typeparam name="TOwner">The type of object being validated</typeparam>
        /// <typeparam name="TProperty">The type of property being validated</typeparam>
        /// <param name="rule">The rule to which the validator should be set</param>
        /// <param name="valueToCompare">The value to compare</param>
        /// <returns><paramref name="rule"/> with new property validator</returns>
        public static PropertyValidationRule<TOwner, TProperty> LessThan<TOwner, TProperty>(
            this PropertyValidationRule<TOwner, TProperty> rule, TProperty valueToCompare)
            where TProperty : IComparable<TProperty>, IComparable
        {
            rule.SetPropertyValidator(new LessThanValidator(valueToCompare));
            return rule;
        }

        /// <summary>
        /// Adds a 'less than' validator to current validation rule.
        /// Validation will succeed if the property value is less than the specified value, otherwise it will fail.
        /// </summary>
        /// <typeparam name="TOwner">The type of object being validated</typeparam>
        /// <typeparam name="TProperty">The type of property being validated</typeparam>
        /// <param name="rule">The rule to which the validator should be set</param>
        /// <param name="valueToCompare">The lambda expression to provide the comparison value</param>
        /// <returns><paramref name="rule"/> with new property validator</returns>
        public static PropertyValidationRule<TOwner, TProperty> LessThan<TOwner, TProperty>(
            this PropertyValidationRule<TOwner, TProperty> rule, Expression<Func<TOwner, TProperty>> valueToCompare)
            where TProperty : IComparable<TProperty>, IComparable
        {
            valueToCompare.Guard(nameof(valueToCompare));
            rule.SetPropertyValidator(new LessThanValidator(valueToCompare.Compile().CoerceToNonGeneric(),
                valueToCompare.Body.ToString()));
            return rule;
        }

        /// <summary>
        /// Adds a 'string length' validator to current validation rule.
        /// Validation will fail if the length of the string is outside of the specified range. The range is inclusive.
        /// </summary>
        /// <typeparam name="TOwner">The type of object being validated</typeparam>
        /// <param name="rule">The rule to which the validator should be set</param>
        /// <param name="min">The lower length constraint</param>
        /// <param name="max">The upper length constraint</param>
        /// <returns><paramref name="rule"/> with new property validator</returns>
        public static PropertyValidationRule<TOwner, string> Length<TOwner>(
            this PropertyValidationRule<TOwner, string> rule, int min, int max)
        {
            rule.SetPropertyValidator(new StringLengthValidator(min, max));
            return rule;
        }

        /// <summary>
        /// Adds a 'string length' validator to current validation rule.
        /// Validation will fail if the length of the string is outside of the specified range. The range is inclusive.
        /// </summary>
        /// <typeparam name="TOwner">The type of object being validated</typeparam>
        /// <param name="rule">The rule to which the validator should be set</param>
        /// <param name="min">The lambda expression to provide the lower length constraint</param>
        /// <param name="max">The lambda expression to provide the upper length constraint</param>
        /// <returns><paramref name="rule"/> with new property validator</returns>
        public static PropertyValidationRule<TOwner, string> Length<TOwner>(
            this PropertyValidationRule<TOwner, string> rule, Func<TOwner, int> min, Func<TOwner, int> max)
        {
            rule.SetPropertyValidator(new StringLengthValidator(min.CoerceToNonGeneric(), max.CoerceToNonGeneric()));
            return rule;
        }

        /// <summary>
        /// Adds an 'exact string length' validator to current validation rule.
        /// Validation will fail if the length of the string is not equal to the length specified.
        /// </summary>
        /// <typeparam name="TOwner">The type of object being validated</typeparam>
        /// <param name="rule">The rule to which the validator should be set</param>
        /// <param name="exactLength">The required length of the string</param>
        /// <returns><paramref name="rule"/> with new property validator</returns>
        public static PropertyValidationRule<TOwner, string> Length<TOwner>(
            this PropertyValidationRule<TOwner, string> rule, int exactLength)
        {
            rule.SetPropertyValidator(new ExactStringLengthValidator(exactLength));
            return rule;
        }

        /// <summary>
        /// Adds an 'exact string length' validator to current validation rule.
        /// Validation will fail if the length of the string is not equal to the length specified.
        /// </summary>
        /// <typeparam name="TOwner">The type of object being validated</typeparam>
        /// <param name="rule">The rule to which the validator should be set</param>
        /// <param name="exactLength">The lambda expression to provide the required length of the string</param>
        /// <returns><paramref name="rule"/> with new property validator</returns>
        public static PropertyValidationRule<TOwner, string> Length<TOwner>(
            this PropertyValidationRule<TOwner, string> rule, Func<TOwner, int> exactLength)
        {
            rule.SetPropertyValidator(new ExactStringLengthValidator(exactLength.CoerceToNonGeneric()));
            return rule;
        }

        /// <summary>
        /// Adds a 'min string length' validator to current validation rule.
        /// Validation will fail if the length of the string is less than the length specified.
        /// </summary>
        /// <typeparam name="TOwner">The type of object being validated</typeparam>
        /// <param name="rule">The rule to which the validator should be set</param>
        /// <param name="min">The lower length constraint</param>
        /// <returns><paramref name="rule"/> with new property validator</returns>
        public static PropertyValidationRule<TOwner, string> MinLength<TOwner>(
            this PropertyValidationRule<TOwner, string> rule, int min)
        {
            rule.SetPropertyValidator(new MinStringLengthValidator(min));
            return rule;
        }

        /// <summary>
        /// Adds a 'min string length' validator to current validation rule.
        /// Validation will fail if the length of the string is less than the length specified.
        /// </summary>
        /// <typeparam name="TOwner">The type of object being validated</typeparam>
        /// <param name="rule">The rule to which the validator should be set</param>
        /// <param name="min">The lambda expression to provide the lower length constraint</param>
        /// <returns><paramref name="rule"/> with new property validator</returns>
        public static PropertyValidationRule<TOwner, string> MinLength<TOwner>(
            this PropertyValidationRule<TOwner, string> rule, Func<TOwner, int> min)
        {
            rule.SetPropertyValidator(new MinStringLengthValidator(min.CoerceToNonGeneric()));
            return rule;
        }

        /// <summary>
        /// Adds a 'max string length' validator to current validation rule.
        /// Validation will fail if the length of the string is greater than the length specified.
        /// </summary>
        /// <typeparam name="TOwner">The type of object being validated</typeparam>
        /// <param name="rule">The rule to which the validator should be set</param>
        /// <param name="max">The upper length constraint</param>
        /// <returns><paramref name="rule"/> with new property validator</returns>
        public static PropertyValidationRule<TOwner, string> MaxLength<TOwner>(
            this PropertyValidationRule<TOwner, string> rule, int max)
        {
            rule.SetPropertyValidator(new MaxStringLengthValidator(max));
            return rule;
        }

        /// <summary>
        /// Adds a 'max string length' validator to current validation rule.
        /// Validation will fail if the length of the string is greater than the length specified.
        /// </summary>
        /// <typeparam name="TOwner">The type of object being validated</typeparam>
        /// <param name="rule">The rule to which the validator should be set</param>
        /// <param name="max">The lambda expression to provide the upper length constraint</param>
        /// <returns><paramref name="rule"/> with new property validator</returns>
        public static PropertyValidationRule<TOwner, string> MaxLength<TOwner>(
            this PropertyValidationRule<TOwner, string> rule, Func<TOwner, int> max)
        {
            rule.SetPropertyValidator(new MaxStringLengthValidator(max.CoerceToNonGeneric()));
            return rule;
        }

        /// <summary>
        /// Specifies a custom failure message to use if validation fails.
        /// </summary>
        /// <typeparam name="TOwner">The type of object being validated</typeparam>
        /// <typeparam name="TProperty">The property being validated</typeparam>
        /// <param name="rule">The rule with the validator whose failure message should be set</param>
        /// <param name="message">The new message to set</param>
        /// <returns><paramref name="rule"/> with validator whose failure message was changed</returns>
        public static PropertyValidationRule<TOwner, TProperty> WithMessage<TOwner, TProperty>(
            this PropertyValidationRule<TOwner, TProperty> rule, string message)
        {
            rule.SetLastValidatorFailureMessage(message);
            return rule;
        }
    }
}
