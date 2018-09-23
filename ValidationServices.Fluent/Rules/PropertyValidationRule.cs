using System;
using System.Collections.Generic;
using ValidationServices.Fluent.Validators;
using ValidationServices.Results;

namespace ValidationServices.Fluent.Rules
{
    public class PropertyValidationRule<TOwner, TProperty> : IPropertyValidationRule
    {
        private readonly List<IPropertyValidator> _validators;

        public PropertyValidationRule()
        {
            this._validators = new List<IPropertyValidator>();
        }

        public ServiceConclusion Validate(PropertyValidatorContext context)
        {
            var conclusion = new ServiceConclusion(isValid: true);
            foreach (var validator in this._validators) {
                conclusion += validator.Validate(context);
            }

            return conclusion;
        }

        public void SetPropertyValidator(IPropertyValidator propertyValidator)
        {
            this._validators.Add(propertyValidator);
        }

        public void SetLastValidatorFailureMessage(string message)
        {
            if (this._validators.Count == 0)
            {
                throw new InvalidOperationException(Resources.Rules.InvalidOperationExceptionNoValidators);
            }

            this._validators[this._validators.Count - 1].FailureMessage = message;
        }
    }
}
