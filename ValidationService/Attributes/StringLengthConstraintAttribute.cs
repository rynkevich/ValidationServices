using System;
using ValidationService.Results;

namespace ValidationService.Attributes {
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public class StringLengthConstraintAttribute : Attribute, IValidator {
        public uint Min { get; set; }
        public uint Max { get; set; }
        public string FailureMessage { get; set; } = "Length of a string must satisfy specified constraints";

        public StringLengthConstraintAttribute(uint min, uint max = 0) {
            this.Min = min;
            this.Max = max;
        }

        public ElementaryConclusion Validate(object obj) {
            int length;
            try {
                length = obj == null ? 0 : ((string)obj).Length;
            } catch {
                throw new ArgumentException("Validation argument is not a string");
            }

            if ((length < this.Min) || (this.Max != 0 && length > this.Max)) {
                return new ElementaryConclusion(isValid: false, this.FailureMessage);
            }

            return new ElementaryConclusion(isValid: true);
        }
    }
}
