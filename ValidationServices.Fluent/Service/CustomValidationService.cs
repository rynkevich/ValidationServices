using System;
using System.Collections.Generic;
using ValidationServices.Service;
using ValidationServices.Results;
using ValidationServices.Fluent.Rules;
using ValidationServices.Fluent.Validators;
using ValidationServices.Fluent.Internal;

namespace ValidationServices.Fluent.Service
{
    using TypeValidationRule = Dictionary<string, IPropertyValidationRule>;

    /// <summary>
    /// Validation service with custom validation rules.
    /// </summary>
    /// <remarks>Use <see cref="ValidationServiceBuilder"/> to get instance of this class</remarks>
    public class CustomValidationService : BaseValidationService
    {
        /// <summary>
        /// Stack of objects that are validating at the moment.
        /// Is maintained to avoid loops.
        /// </summary>
        private readonly Stack<object> _trace;

        /// <summary>
        /// Dictionary with types and corresponding validation rules.
        /// </summary>
        private readonly Dictionary<Type, TypeValidationRule> _typesToValidate;

        /// <summary>
        /// Gets or sets a flag indicating whether the validation should be accomplished recursively.
        /// </summary>
        public override bool IsRecursiveValidation { get; set; }

        /// <summary>
        /// Initializes a new instance of <see cref="CustomValidationService"/> class.
        /// </summary>
        /// <remarks>Use <see cref="ValidationServiceBuilder"/> to get instance of this class</remarks>
        /// <param name="isRecursiveValidation">The flag indicating whether the validation should be accomplished recursively</param>
        public CustomValidationService(bool isRecursiveValidation = true)
        {
            this.IsRecursiveValidation = isRecursiveValidation;
            this._trace = new Stack<object>();
            this._typesToValidate = new Dictionary<Type, TypeValidationRule>();
        }

        /// <summary>
        /// Override of <see cref="BaseValidationService.Validate{T}(T, string)"/>
        /// </summary>
        /// <typeparam name="T">The type of the object to be validated</typeparam>
        /// <param name="objectToValidate">The object to validate</param>
        /// <param name="objectName">The object name. 
        /// Used to print full qualified property names to <see cref="ServiceConclusion.Details"/></param>
        /// <returns>
        /// <see cref="ServiceConclusion"/> with <c>IsValid</c> flag set to <c>true</c> 
        /// if the <paramref name="objectToValidate"/> is acceptable. Otherwise, the flag is set to <c>false</c> and
        /// <see cref="ServiceConclusion.Details"/> contains a report on problems.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="objectToValidate"/> is null</exception>
        public override ServiceConclusion Validate<T>(T objectToValidate, string objectName = "")
        {
            objectToValidate.Guard(nameof(objectToValidate));

            return this.ValidateObject(objectToValidate, objectName);
        }

        /// <summary>
        /// Sets new validation rule for specified type and property.
        /// </summary>
        /// <remarks>Use extension methods to create and set rules</remarks>
        /// <param name="validationTarget">The type that contains the property to validate</param>
        /// <param name="propertyName">The property to validate</param>
        /// <param name="newPropertyValidationRule">The validation rule for specified property</param>
        /// <exception cref="InvalidOperationException">Thrown if rule for specified property is already set</exception>
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
                    throw new InvalidOperationException(
                        Resources.Service.InvalidOperationExceptionRuleAlreadySet.Replace("{propertyName}", propertyName));
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

        /// <summary>
        /// Internal logic of object validation.
        /// Is called recursively if <see cref="BaseValidationService.IsRecursiveValidation"/> equals <c>true</c>
        /// </summary>
        /// <typeparam name="T">The type of the object to be validated</typeparam>
        /// <param name="objectToValidate">The object to validate</param>
        /// <param name="fullName">The full object name</param>
        /// <returns>
        /// <see cref="ServiceConclusion"/> with <c>IsValid</c> flag set to <c>true</c> 
        /// if the <paramref name="objectToValidate"/> is acceptable. Otherwise, the flag is set to <c>false</c> and
        /// <see cref="ServiceConclusion.Details"/> contains a report on problems.
        /// </returns>
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
