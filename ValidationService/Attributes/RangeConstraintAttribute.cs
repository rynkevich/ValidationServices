using System;
using ValidationService.Results;

namespace ValidationService.Attributes {
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public class RangeConstraintAttribute : Attribute, IValidator {
        public IComparable Min { get; set; }
        public IComparable Max { get; set; }
        public string LowerConstraintFailureMessage { get; set; } = "Value of a field is less than its specified lower bound";
        public string UpperConstraintFailureMessage { get; set; } = "Value of a field is greater than its specified upper bound";

        public RangeConstraintAttribute(IComparable min, IComparable max = null) {
            this.Min = min;
            this.Max = max;
        }

        public ElementaryConclusion Validate(object obj) {
            if (this.Min == null && this.Max == null) {
                throw new ArgumentNullException("Constraints are not specified");
            }

            try {
                if (this.Min != null && this.Min.CompareTo(obj) > 0) {
                    return new ElementaryConclusion(false, this.LowerConstraintFailureMessage);
                }
                if (this.Max != null && this.Max.CompareTo(obj) < 0) {
                    return new ElementaryConclusion(false, this.UpperConstraintFailureMessage);
                }
            } catch {
                throw new ArgumentException("Range constraints and validated object are not compatible");
            }

            return new ElementaryConclusion(true);
        }
    }
}
