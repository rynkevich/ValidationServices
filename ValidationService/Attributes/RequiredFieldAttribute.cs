using System;
using ValidationService.Results;

namespace ValidationService.Attributes {
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public class RequiredFieldAttribute : Attribute, IValidator {
        public bool AllowEmptyStrings { get; set; } = false;
        public string IsNullFailureMessage { get; set; } = "Value of required field is null";

        public ElementaryConclusion Validate(object obj) {
            if (obj == null) {
                return new ElementaryConclusion(false, this.IsNullFailureMessage);
            }

            if (!this.AllowEmptyStrings) {
                string stringObj = obj as string;
                if (stringObj != null) {
                    return new ElementaryConclusion(stringObj.Trim().Length != 0);
                }
            }

            return new ElementaryConclusion(true);
        }
    }
}
