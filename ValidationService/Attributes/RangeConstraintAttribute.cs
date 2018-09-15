using System;
using ValidationService.Results;

namespace ValidationService.Attributes
{
    /// <summary>
    /// Validation attribute to specify a range constraint.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public class RangeConstraintAttribute : ValidationAttribute
    {
        /// <summary>
        /// Gets or sets the minimum value for the range.
        /// </summary>
        public object Min { get; set; }

        /// <summary>
        /// Gets or sets the maximum value for the range.
        /// </summary>
        public object Max { get; set; }

        /// <summary>
        /// Gets or sets a message that will be returned by <see cref="RangeConstraintAttribute.Validate(object)"/>
        /// in <see cref="ElementaryConclusion.Details"/></c>
        /// </summary>
        public string FailureMessage { get; set; } = "Property value must satisfy specified constraints";

        /// <summary>
        /// Override of <see cref="ValidationAttribute.Validate(object)"/>
        /// </summary>
        /// <param name="obj">The object to validate</param>
        /// <returns>
        /// <see cref="ElementaryConclusion"/> with <c>IsValid</c> flag set to <c>true</c> 
        /// if the value falls between min and max, inclusive. Otherwise, the flag is set to <c>false</c> and
        /// <see cref="ElementaryConclusion.Details"/> contains <see cref="FailureMessage"/>
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown if the current attribute is ill-formed.</exception>
        /// <exception cref="ArgumentException">Thrown if the current attribute is ill-formed.</exception>
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
                /* 
                    In case of unsigned values, Min and Max are to be casted to signed first:
                    these properties have object type and are set by integer literals, which are always signed;
                    variable of object type, that contains signed value, cant not be directly casted to unsigned.
                */
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
