using System;
using System.Collections.Generic;
using System.Reflection;
using ValidationService.Results;
using ValidationService.Attributes;

namespace ValidationService
{
    public class AttributeBasedValidationService : ValidationService
    {
        private HashSet<int> hashCodeSet;

        public AttributeBasedValidationService(bool isRecursiveValidation = true)
        {
            this.IsRecursiveValidation = isRecursiveValidation;
        }

        public override GeneralConclusion Validate<T>(T obj, string objName = "")
        {
            if (obj == null)
            {
                return new GeneralConclusion(isValid: true);
            }

            if (this.IsRecursiveValidation)
            {
                this.hashCodeSet = new HashSet<int>
                {
                    obj.GetHashCode()
                };
                GeneralConclusion conclusion = this.ValidateObject(obj, objName);
                this.hashCodeSet = null;
                return conclusion;
            }
            else
            {
                return this.ValidateObject(obj, objName);
            }
        }

        private GeneralConclusion ValidateObject(object obj, string fullName)
        {
            GeneralConclusion conclusion = new GeneralConclusion(isValid: true);

            foreach (PropertyInfo prop in obj.GetType().GetProperties())
            {
                if (prop.GetIndexParameters().Length == 0)
                {
                    object value = prop.GetValue(obj);
                    if (!this.IsRecursiveValidation || value == null || !this.hashCodeSet.Contains(value.GetHashCode()))
                    {
                        if (this.IsRecursiveValidation && value != null)
                        {
                            this.hashCodeSet.Add(value.GetHashCode());
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
            }

            return conclusion;
        }

    }
}
