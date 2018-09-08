using System;
using ValidationService.Results;

namespace ValidationService.Attributes {
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public class RequiredPropertyAttribute : ValidationAttribute {
        public bool AllowEmptyStrings { get; set; } = false;
        public string FailureMessage { get; set; } = "Required property must be initialized with valid value";

        public RequiredPropertyAttribute() { }

        public override ElementaryConclusion Validate(object obj) {
            if (obj == null) {
                return new ElementaryConclusion(isValid: false, this.FailureMessage);
            }

            if (!this.AllowEmptyStrings) {
                string stringObj = obj as string;
                if (stringObj != null) {
                    return new ElementaryConclusion(isValid: stringObj.Trim().Length != 0);
                }
            }

            return new ElementaryConclusion(isValid: true);
        }
    }
}
