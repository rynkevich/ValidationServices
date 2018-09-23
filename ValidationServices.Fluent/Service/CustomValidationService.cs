using System;
using System.Reflection;
using System.Collections.Generic;
using ValidationServices.Service;
using ValidationServices.Results;
using ValidationServices.Fluent.Rules;
using ValidationServices.Fluent.Validators;
using ValidationServices.Fluent.Internal;

namespace ValidationServices.Fluent.Service
{
    using TypeValidationRule = Dictionary<string, IPropertyValidationRule>;

    public class CustomValidationService : BaseValidationService
    {
        private readonly Stack<object> _trace;
        private readonly Dictionary<Type, TypeValidationRule> _typesToValidate;

        public override bool IsRecursiveValidation { get; set; }

        public CustomValidationService(bool isRecursiveValidation = true)
        {
            this.IsRecursiveValidation = isRecursiveValidation;
            this._trace = new Stack<object>();
            this._typesToValidate = new Dictionary<Type, TypeValidationRule>();
        }

        public override ServiceConclusion Validate<T>(T objectToValidate, string objName = "")
        {
            objectToValidate.Guard(nameof(objectToValidate));

            return this.ValidateObject(objectToValidate, objName);
        }

        public void SetRule(Type validationTarget, 
            string propertyName, IPropertyValidationRule newPropertyValidationRule)
        {
            validationTarget.Guard(nameof(validationTarget));
            propertyName.Guard(nameof(propertyName));
            newPropertyValidationRule.Guard(nameof(newPropertyValidationRule));

            if (this._typesToValidate.TryGetValue(validationTarget, out TypeValidationRule typeValidationRule))
            {
                if (typeValidationRule.ContainsKey(propertyName))
                {
                    throw new InvalidOperationException($"Validation rule for property '{propertyName}' is already set");
                }
                typeValidationRule.Add(propertyName, newPropertyValidationRule);
            }
            else
            {
                var newTypeValidationRule = new TypeValidationRule
                {
                    { propertyName, newPropertyValidationRule }
                };
                this._typesToValidate.Add(validationTarget, newTypeValidationRule);
            }
        }

        private ServiceConclusion ValidateObject<T>(T objectToValidate, string fullName)
        {
            if (this.IsRecursiveValidation)
            {
                this._trace.Push(objectToValidate);
            }

            var conclusion = new ServiceConclusion(isValid: true);
            if (objectToValidate == null || !this._typesToValidate.TryGetValue(typeof(T), out TypeValidationRule typeValidationRule))
            {
                return conclusion;
            }

            foreach (var property in objectToValidate.GetType().GetProperties())
            {
                if (typeValidationRule.TryGetValue(property.Name, out IPropertyValidationRule rule))
                {
                    conclusion += rule.Validate(new PropertyValidatorContext(objectToValidate, property.GetValue(objectToValidate)));
                }
            }

            if (this.IsRecursiveValidation)
            {
                this._trace.Pop();
            }
            return conclusion;
        }
    }
}
