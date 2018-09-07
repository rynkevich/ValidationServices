using System;
using ValidationService.Results;

namespace ValidationService.Attributes {
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public class StringLengthConstraintAttribute : Attribute, IValidator {
        public uint Min { get; set; }
        public uint Max { get; set; }
        public string LowerConstraintFailureMessage { get; set; } = "Length of a string is less than minimal allowed value";
        public string UpperConstraintFailureMessage { get; set; } = "Length of a string is greater than maximal allowed value";

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

            if (length < this.Min) {
                return new ElementaryConclusion(false, this.LowerConstraintFailureMessage);
            }
            if (this.Max != 0 && length > this.Max) {
                return new ElementaryConclusion(false, this.UpperConstraintFailureMessage);
            }

            return new ElementaryConclusion(true);
        }
    }
}
