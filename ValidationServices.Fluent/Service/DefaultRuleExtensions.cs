using ValidationServices.Fluent.Rules;

namespace ValidationServices.Fluent.Service
{
    public static class DefaultRuleExtensions
    {
        public static PropertyValidationRule<TOwner, TProperty> WithMessage<TOwner, TProperty>(
            this PropertyValidationRule<TOwner, TProperty> rule, string message)
        {
            rule.SetLastValidatorFailureMessage(message);
            return rule;
        }
    }
}
