using System.Collections.Generic;
using System.Reflection;
using ValidationServices.Results;
using ValidationServices.Attributes;
using System;

namespace ValidationServices.Service
{
    /// <summary>
    /// Attribute based service of object validation.
    /// </summary>
    public class ValidationService : BaseValidationService
    {
        /// <summary>
        /// Stack of objects that are validating at the moment.
        /// Is maintained to avoid loops.
        /// </summary>
        private readonly Stack<object> _trace;

        /// <summary>
        /// Gets or sets a flag indicating whether the validation should be accomplished recursively.
        /// </summary>
        public override bool IsRecursiveValidation { get; set; }

        /// <summary>
        /// Service constructor.
        /// </summary>
        /// <param name="isRecursiveValidation">The flag indicating whether the validation should be accomplished recursively.</param>
        public ValidationService(bool isRecursiveValidation = true)
        {
            this._trace = new Stack<object>();
            this.IsRecursiveValidation = isRecursiveValidation;
        }

        /// <summary>
        /// Override of <see cref="BaseValidationService.Validate{T}(T, string)"/>
        /// </summary>
        /// <typeparam name="T">The type of object to be validated</typeparam>
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
            if (objectToValidate == null)
            {
                throw new ArgumentNullException(nameof(objectToValidate));
            }

            return this.ValidateObject(objectToValidate, objectName);
        }

        /// <summary>
        /// Internal logic of object validation.
        /// Is called recursively if <see cref="BaseValidationService.IsRecursiveValidation"/> equals <c>true</c>
        /// </summary>
        /// <typeparam name="T">The type of object to be validated</typeparam>
        /// <param name="objectToValidate">The object to validate</param>
        /// <param name="fullName">The full object name</param>
        /// <returns>
        /// <see cref="ServiceConclusion"/> with <c>IsValid</c> flag set to <c>true</c> 
        /// if the <paramref name="objectToValidate"/> is acceptable. Otherwise, the flag is set to <c>false</c> and
        /// <see cref="ServiceConclusion.Details"/> contains a report on problems.
        /// </returns>
        private ServiceConclusion ValidateObject<T>(T objectToValidate, string fullName) where T : class
        {
            if (this.IsRecursiveValidation)
            {
                this._trace.Push(objectToValidate);
            }

            var conclusion = new ServiceConclusion(isValid: true);
            if (objectToValidate == null)
            {
                return conclusion;
            }
            foreach (var property in objectToValidate.GetType().GetProperties())
            {
                if (property.GetIndexParameters().Length == 0)
                {
                    var value = property.GetValue(objectToValidate);

                    if (this.IsRecursiveValidation && !this._trace.Contains(value))
                    {
                        conclusion += this.ValidateObject(value, $"{fullName}.{property.Name}");
                    }

                    foreach (var attr in property.GetCustomAttributes<ValidationAttribute>())
                    {
                        ElementaryConclusion elemConclusion = attr.Validate(value);
                        conclusion += new ElementaryConclusion(elemConclusion.IsValid,
                            elemConclusion.Details != null ?
                            $"{fullName}.{property.Name}: {elemConclusion.Details}" : null);
                    }
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
