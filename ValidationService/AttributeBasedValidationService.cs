using System;
using System.Collections.Generic;
using System.Reflection;
using ValidationService.Results;

namespace ValidationService {
    class AttributeBasedValidationService : ValidationService {
        private HashSet<int> hashCodeSet;

        public AttributeBasedValidationService(bool isRecursiveValidation = true) {
            this.IsRecursiveValidation = isRecursiveValidation;
        }

        public override GeneralConclusion Validate(object obj, string objName = "") {
            if (this.IsRecursiveValidation) {
                this.hashCodeSet = new HashSet<int>();
                this.hashCodeSet.Add(obj.GetHashCode());
                GeneralConclusion conclusion = this.ValidateField(obj, objName);
                this.hashCodeSet = null;
                return conclusion;
            } else {
                return this.ValidateField(obj, objName);
            }
        }

        private GeneralConclusion ValidateField(object obj, string fullName) {
            if (obj == null) {
                return new GeneralConclusion(true);
            }

            GeneralConclusion conclusion = new GeneralConclusion(true);
            foreach (FieldInfo field in obj.GetType().GetFields()) {
                object value = field.GetValue(obj);
                if (!this.IsRecursiveValidation || value == null || !this.hashCodeSet.Contains(value.GetHashCode())) {
                    if (value != null) {
                        this.hashCodeSet.Add(value.GetHashCode());
                    }
                    if (this.IsRecursiveValidation) {
                        conclusion += this.ValidateField(value, fullName + '.' + field.Name);
                    }
                    foreach (Attribute attr in field.GetCustomAttributes()) {
                        if (attr is IValidator) {
                            ElementaryConclusion elemConclusion = (attr as IValidator).Validate(value);
                            conclusion += new ElementaryConclusion(elemConclusion.IsValid,
                                elemConclusion.Details != null ? (fullName +
                                '.' + field.Name + ": " + elemConclusion.Details) : null);
                        }
                    }
                }
            }

            return conclusion;
        }

    }
}
