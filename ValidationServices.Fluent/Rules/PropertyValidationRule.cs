using System;
using System.Collections.Generic;
using ValidationServices.Fluent.Validators;
using ValidationServices.Results;

namespace ValidationServices.Fluent.Rules
{
    /// <summary>
    /// Class that accumulates validators for single property.
    /// </summary>
    /// <typeparam name="TOwner">The type of the object that owns a property.</typeparam>
    /// <typeparam name="TProperty">The type of the property to validate.</typeparam>
    public class PropertyValidationRule<TOwner, TProperty> : IPropertyValidationRule
    {
        /// <summary>
        /// Accumulator for validators.
        /// </summary>
        private readonly List<IPropertyValidator> _validators;

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyValidationRule{TOwner, TProperty}"/> class.
        /// </summary>
        public PropertyValidationRule()
        {
            this._validators = new List<IPropertyValidator>();
        }

        /// <summary>
        /// Validates context.
        /// </summary>
        /// <param name="context">The context that includes information about object to validate</param>
        /// <returns><see cref="ServiceConclusion"/> with validation result</returns>
        public ServiceConclusion Validate(PropertyValidatorContext context)
        {
            var conclusion = new ServiceConclusion(isValid: true);
            foreach (var validator in this._validators) {
                conclusion += validator.Validate(context);
            }

            return conclusion;
        }

        /// <summary>
        /// Sets property validator.
        /// </summary>
        /// <param name="propertyValidator">The validator to set</param>
        public void SetPropertyValidator(IPropertyValidator propertyValidator)
        {
            this._validators.Add(propertyValidator);
        }

        /// <summary>
        /// Sets custom failure message for last validator in rule.
        /// </summary>
        /// <param name="message">The failure message for validator</param>
        /// <exception cref="InvalidOperationException">Thrown if there is no validators in rule</exception>
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
