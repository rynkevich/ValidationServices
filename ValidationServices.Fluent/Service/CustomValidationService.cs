using System;
using System.Reflection;
using System.Collections.Generic;
using ValidationServices.Service;
using ValidationServices.Results;
using ValidationServices.Fluent.Rules;
using ValidationServices.Fluent.Validators;

namespace ValidationServices.Fluent.Service
{
    using TypeValidationRule = Dictionary<string, IPropertyValidationRule>;

    public class CustomValidationService : BaseValidationService
    {
        private readonly Stack<object> trace;
        private readonly Dictionary<Type, TypeValidationRule> typesToValidate;

        public override bool IsRecursiveValidation { get; set; }

        public CustomValidationService(bool isRecursiveValidation = true)
        {
            this.IsRecursiveValidation = isRecursiveValidation;
            this.trace = new Stack<object>();
            this.typesToValidate = new Dictionary<Type, TypeValidationRule>();
        }

        public override GeneralConclusion Validate<T>(T objectToValidate, string objName = "")
        {
            throw new NotImplementedException();
        }

        public void SetRule(Type validationTarget, 
            string propertyName, IPropertyValidationRule newPropertyValidationRule)
        {
            if (validationTarget == null)
            {
                throw new ArgumentNullException(nameof(validationTarget));
            }
            if (propertyName == null)
            {
                throw new ArgumentNullException(nameof(propertyName));
            }
            if (newPropertyValidationRule == null)
            {
                throw new ArgumentNullException(nameof(newPropertyValidationRule));
            }

            if (this.typesToValidate.TryGetValue(validationTarget, out TypeValidationRule typeValidationRule))
            {
                if (typeValidationRule.ContainsKey(propertyName))
                {
                    throw new InvalidOperationException($"Validation rule for property '{propertyName}' is already set");
                }
                typeValidationRule.Add(propertyName, newPropertyValidationRule);
            }
            else
            {
                TypeValidationRule newTypeValidationRule = new TypeValidationRule();
                newTypeValidationRule.Add(propertyName, newPropertyValidationRule);
                this.typesToValidate.Add(validationTarget, newTypeValidationRule);
            }
        }

        private GeneralConclusion ValidateObject<T>(T objectToValidate, string fullName)
        {
            if (this.IsRecursiveValidation)
            {
                this.trace.Push(objectToValidate);
            }

            GeneralConclusion conclusion = new GeneralConclusion(isValid: true);
            TypeValidationRule typeValidationRule;
            if (objectToValidate == null || !this.typesToValidate.TryGetValue(typeof(T), out typeValidationRule))
            {
                return conclusion;
            }

            foreach (PropertyInfo property in objectToValidate.GetType().GetProperties())
            {
                if (typeValidationRule.TryGetValue(property.Name, out IPropertyValidationRule rule))
                {
                    conclusion += rule.Validate(new PropertyValidatorContext(objectToValidate, property.GetValue(objectToValidate)));
                }
            }

            if (this.IsRecursiveValidation)
            {
                this.trace.Pop();
            }
            return conclusion;
        }
    }
}
