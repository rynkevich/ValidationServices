using System;
using System.Collections.Generic;
using System.Reflection;
using ValidationService.Results;

namespace ValidationService {
    public class AttributeBasedValidationService : ValidationService {
        private HashSet<int> hashCodeSet;

        public AttributeBasedValidationService(bool isRecursiveValidation = true) {
            this.IsRecursiveValidation = isRecursiveValidation;
        }

        public override GeneralConclusion Validate(object obj, string objName = "") {
            if (this.IsRecursiveValidation) {
                this.hashCodeSet = new HashSet<int>();
                this.hashCodeSet.Add(obj.GetHashCode());
                GeneralConclusion conclusion = this.ValidateProperty(obj, objName);
                this.hashCodeSet = null;
                return conclusion;
            } else {
                return this.ValidateProperty(obj, objName);
            }
        }

        private GeneralConclusion ValidateProperty(object obj, string fullName) {
            if (obj == null) {
                return new GeneralConclusion(isValid: true);
            }

            GeneralConclusion conclusion = new GeneralConclusion(isValid: true);
            foreach (PropertyInfo prop in obj.GetType().GetProperties()) {
                object value = prop.GetValue(obj);
                if (!this.IsRecursiveValidation || value == null || !this.hashCodeSet.Contains(value.GetHashCode())) {
                    if (value != null) {
                        this.hashCodeSet.Add(value.GetHashCode());
                    }
                    if (this.IsRecursiveValidation) {
                        conclusion += this.ValidateProperty(value, fullName + '.' + prop.Name);
                    }
                    foreach (Attribute attr in prop.GetCustomAttributes()) {
                        if (attr is IValidator) {
                            ElementaryConclusion elemConclusion = (attr as IValidator).Validate(value);
                            conclusion += new ElementaryConclusion(elemConclusion.IsValid,
                                elemConclusion.Details != null ? (fullName +
                                '.' + prop.Name + ": " + elemConclusion.Details) : null);
                        }
                    }
                }
            }

            return conclusion;
        }

    }
}
