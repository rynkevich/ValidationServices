using System;
using ValidationService.Results;

namespace ValidationService.Attributes {
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public class StringLengthConstraintAttribute : ValidationAttribute {
        private static readonly uint DEFAULT_MIN = 0;
        private static readonly uint DEFAULT_MAX = uint.MaxValue;

        public uint Min { get; set; } = DEFAULT_MIN;
        public uint Max { get; set; } = DEFAULT_MAX;
        public string FailureMessage { get; set; } = "Length of a string must satisfy specified constraints";

        public StringLengthConstraintAttribute() { }

        public override ElementaryConclusion Validate(object obj) {
            if (this.Min == DEFAULT_MIN && this.Max == DEFAULT_MAX) {
                throw new ArgumentException("Constraint is not specified");
            }
            if (this.Min > this.Max) {
                throw new ArgumentException("Lower constraint exceeds upper constraint");
            }

            if (obj == null) {
                return new ElementaryConclusion(isValid: true);
            }

            int length;
            try {
                length = ((string)obj).Length;
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
