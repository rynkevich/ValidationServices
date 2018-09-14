using System;
using System.Collections.Generic;
using System.Reflection;
using ValidationService.Results;
using ValidationService.Attributes;

namespace ValidationService
{
    public class AttributeBasedValidationService : ValidationService
    {
        private readonly Stack<object> trace;

        public AttributeBasedValidationService(bool isRecursiveValidation = true)
        {
            this.trace = new Stack<object>();
            this.IsRecursiveValidation = isRecursiveValidation;
        }

        public override GeneralConclusion Validate<T>(T obj, string objName = "")
        {
            if (obj == null)
            {
                return new GeneralConclusion(isValid: true);
            }

            return this.ValidateObject(obj, objName);
        }

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
