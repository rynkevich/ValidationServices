using System;
using ValidationService.Results;

namespace ValidationService.Attributes
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public class RangeConstraintAttribute : ValidationAttribute
    {
        public object Min { get; set; }
        public object Max { get; set; }
        public string FailureMessage { get; set; } = "Property value must satisfy specified constraints";

        public RangeConstraintAttribute() { }

        public override ElementaryConclusion Validate(object obj)
        {
            if (this.Min == null && this.Max == null)
            {
                throw new ArgumentNullException("Constraint is not specified");
            }

            if (obj == null)
            {
                return new ElementaryConclusion(isValid: true);
            }

            try
            {
                if (this.Min != null && this.Max != null && ((IComparable)this.Min).CompareTo(this.Max) > 0)
                {
                    throw new ArgumentException("Lower constraint exceeds upper constraint");
                }
            }
            catch
            {
                throw new ArgumentException("Range constraints are not compatible");
            }

            bool isValid = true;
            try
            {
                if (obj is byte)
                {
                    isValid = !((this.Min != null) && ((byte)(sbyte)this.Min > (byte)obj) ||
                        (this.Max != null) && ((byte)(sbyte)this.Max < (byte)obj));
                }
                else if (obj is ushort)
                {
                    isValid = !((this.Min != null) && ((ushort)(short)this.Min > (ushort)obj) ||
                        (this.Max != null) && ((ushort)(short)this.Max < (ushort)obj));
                }
                else if (obj is uint)
                {
                    isValid = !((this.Min != null) && ((uint)(int)this.Min > (uint)obj) ||
                        (this.Max != null) && ((uint)(int)this.Max < (uint)obj));
                }
                else if (obj is ulong)
                {
                    isValid = !((this.Min != null) && ((ulong)(long)this.Min > (ulong)obj) ||
                        (this.Max != null) && ((ulong)(long)this.Max < (ulong)obj));
                }
                else
                {
                    isValid = !((this.Min != null && ((IComparable)obj).CompareTo(this.Min) < 0) ||
                        (this.Max != null && ((IComparable)obj).CompareTo(this.Max) > 0));
                }
            }
            catch
            {
                throw new ArgumentException("Range constraints and validated object are not compatible");
            }

            return isValid ? new ElementaryConclusion(isValid: true) : 
                new ElementaryConclusion(isValid: false, this.FailureMessage);
        }
    }
}
