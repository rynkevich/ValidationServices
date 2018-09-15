using System.Collections.Generic;
using System.Reflection;
using ValidationService.Results;
using ValidationService.Attributes;

namespace ValidationService.Service
{
    /// <summary>
    /// Attribute based service of object validation.
    /// </summary>
    public class AttributeBasedValidationService : ValidationService
    {
        /// <summary>
        /// Stack of objects that are validating at the moment.
        /// Is maintained to avoid loops.
        /// </summary>
        private readonly Stack<object> trace;

        /// <summary>
        /// Gets or sets a flag indicating whether the validation should be accomplished recursively.
        /// </summary>
        public override bool IsRecursiveValidation { get; set; }

        /// <summary>
        /// Service constructor.
        /// </summary>
        /// <param name="isRecursiveValidation">The flag indicating whether the validation should be accomplished recursively.</param>
        public AttributeBasedValidationService(bool isRecursiveValidation = true)
        {
            this.trace = new Stack<object>();
            this.IsRecursiveValidation = isRecursiveValidation;
        }

        /// <summary>
        /// Override of <see cref="ValidationService.Validate{T}(T, string)"/>
        /// </summary>
        /// <typeparam name="T">The type of object to be validated</typeparam>
        /// <param name="obj">The object to validate</param>
        /// <param name="objName">The object name. 
        /// Used to print full qualified property names to <see cref="GeneralConclusion.Details"/></param>
        /// <returns>
        /// <see cref="GeneralConclusion"/> with <c>IsValid</c> flag set to <c>true</c> 
        /// if the <paramref name="obj"/> is acceptable. Otherwise, the flag is set to <c>false</c> and
        /// <see cref="GeneralConclusion.Details"/> contains a report on problems.
        /// </returns>
        public override GeneralConclusion Validate<T>(T obj, string objName = "")
        {
            if (obj == null)
            {
                return new GeneralConclusion(isValid: true);
            }

            return this.ValidateObject(obj, objName);
        }

        /// <summary>
        /// Internal logic of object validation.
        /// Is called recursively if <see cref="ValidationService.IsRecursiveValidation"/> equals <c>true</c>
        /// </summary>
        /// <typeparam name="T">The type of object to be validated</typeparam>
        /// <param name="obj">The object to validate</param>
        /// <param name="fullName">The full object name</param>
        /// <returns>
        /// <see cref="GeneralConclusion"/> with <c>IsValid</c> flag set to <c>true</c> 
        /// if the <paramref name="obj"/> is acceptable. Otherwise, the flag is set to <c>false</c> and
        /// <see cref="GeneralConclusion.Details"/> contains a report on problems.
        /// </returns>
        private GeneralConclusion ValidateObject<T>(T obj, string fullName) where T : class
        {
            if (this.IsRecursiveValidation)
            {
                this.trace.Push(obj);
            }

            GeneralConclusion conclusion = new GeneralConclusion(isValid: true);
            if (obj == null)
            {
                return conclusion;
            }
            foreach (PropertyInfo prop in obj.GetType().GetProperties())
            {
                if (prop.GetIndexParameters().Length == 0)
                {
                    object value = prop.GetValue(obj);

                    if (this.IsRecursiveValidation && !this.trace.Contains(value))
                    {
                        conclusion += this.ValidateObject(value, $"{fullName}.{prop.Name}");
                    }

                    foreach (ValidationAttribute attr in prop.GetCustomAttributes<ValidationAttribute>())
                    {
                        ElementaryConclusion elemConclusion = attr.Validate(value);
                        conclusion += new ElementaryConclusion(elemConclusion.IsValid,
                            elemConclusion.Details != null ?
                            $"{fullName}.{prop.Name}: {elemConclusion.Details}" : null);
                    }
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
