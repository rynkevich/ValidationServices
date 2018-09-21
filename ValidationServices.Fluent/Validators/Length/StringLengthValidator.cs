using System;
using ValidationServices.Fluent.Internal;
using ValidationServices.Fluent.Rules;
using ValidationServices.Results;

namespace ValidationServices.Fluent.Validators.Length
{
    public static class StringLengthRuleExtension
    {
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
    }

    public class StringLengthValidator : IPropertyValidator
    {
        private readonly int min;
        private readonly int max;
        private readonly Func<object, int> minFunc;
        private readonly Func<object, int> maxFunc;
        protected static readonly int MAX_NOT_SPECIFIED = -1;

        public string FailureMessage { get; set; } = "Length of a string must satisfy specified constraints";

        public StringLengthValidator(int min, int max)
        {
            this.min = min;
            this.max = max;

            if (max != MAX_NOT_SPECIFIED && min > max)
            {
                throw new ArgumentOutOfRangeException(nameof(max), "Max should be larger than min");
            }
        }

        public StringLengthValidator(Func<object, int> minFunc, Func<object, int> maxFunc)
        {
            minFunc.Guard(nameof(minFunc));
            maxFunc.Guard(nameof(maxFunc));
            this.minFunc = minFunc;
            this.maxFunc = maxFunc;
        }

        public ElementaryConclusion Validate(PropertyValidatorContext context)
        {
            if (context.PropertyToValidate == null)
            {
                return new ElementaryConclusion(isValid: false, this.FailureMessage);
            }

            int min = this.minFunc != null ? this.minFunc(context.ObjectToValidate) : this.min;
            int max = this.maxFunc != null ? this.maxFunc(context.ObjectToValidate) : this.max;

            int length = context.PropertyToValidate.ToString().Length;

            if (length < min || (length > max && max != MAX_NOT_SPECIFIED))
            {
                return new ElementaryConclusion(isValid: false, this.FailureMessage);
            }

            return new ElementaryConclusion(isValid: true);
        }
    }
}
