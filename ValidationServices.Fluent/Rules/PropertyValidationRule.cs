using System;
using System.Collections.Generic;
using ValidationServices.Fluent.Validators;
using ValidationServices.Results;

namespace ValidationServices.Fluent.Rules
{
    public class PropertyValidationRule<TOwner, TProperty> : IPropertyValidationRule
    {
        private readonly List<IPropertyValidator> validators;

        public PropertyValidationRule()
        {
            this.validators = new List<IPropertyValidator>();
        }

        public GeneralConclusion Validate(PropertyValidatorContext context)
        {
            GeneralConclusion conclusion = new GeneralConclusion(isValid: true);
            foreach (IPropertyValidationRule validator in this.validators) {
                conclusion += validator.Validate(context);
            }

            return conclusion;
        }

        public void SetPropertyValidator(IPropertyValidator propertyValidator)
        {
            this.validators.Add(propertyValidator);
        }

        public void SetLastValidatorFailureMessage(string message)
        {
            if (this.validators.Count == 0)
            {
                throw new InvalidOperationException("There is no validator in rule to set failure message");
            }

            this.validators[this.validators.Count - 1].FailureMessage = message;
        }
    }
}
