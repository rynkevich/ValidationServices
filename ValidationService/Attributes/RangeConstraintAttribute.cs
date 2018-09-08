using System;
using ValidationService.Results;

namespace ValidationService.Attributes {
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public class RangeConstraintAttribute : ValidationAttribute {
        public object Min { get; set; }
        public object Max { get; set; }
        public string FailureMessage { get; set; } = "Property value must satisfy specified constraints";

        public RangeConstraintAttribute() { }

        public override ElementaryConclusion Validate(object obj) {
            if (this.Min == null && this.Max == null) {
                throw new ArgumentNullException("Constraint is not specified");
            }

            if (obj == null) {
                return new ElementaryConclusion(isValid: true);
            }

            try {
                if (this.Min != null && this.Max != null && ((IComparable)this.Min).CompareTo(this.Max) > 0) {
                    throw new ArgumentException("Lower constraint exceeds upper constraint");
                }
            } catch {
                throw new ArgumentException("Range constraints are not compatible");
            }

            try {
                if ((this.Min != null && ((IComparable)this.Min).CompareTo(obj) > 0) || 
                    (this.Max != null && ((IComparable)this.Max).CompareTo(obj) < 0)) 
                {
                    return new ElementaryConclusion(isValid: false, this.FailureMessage);
                }
            } catch {
                throw new ArgumentException("Range constraints and validated object are not compatible");
            }

            return new ElementaryConclusion(isValid: true);
        }
    }
}
