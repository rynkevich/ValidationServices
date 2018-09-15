using System;
using ValidationService.Results;

namespace ValidationService.Attributes
{
    /// <summary>
    /// Validation attribute to specify a string length constraint.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public class StringLengthConstraintAttribute : ValidationAttribute
    {
        /// <summary>
        /// Used as <see cref="Min"/> when lower constraint is not specified. 
        /// </summary>
        private static readonly uint DEFAULT_MIN = 0;

        /// <summary>
        /// Used as <see cref="Max"/> when upper constraint is not specified. 
        /// </summary>
        private static readonly uint DEFAULT_MAX = uint.MaxValue;

        /// <summary>
        /// Gets or sets the minimum acceptable length of the string.
        /// </summary>
        public uint Min { get; set; } = DEFAULT_MIN;

        /// <summary>
        /// Gets or sets the maximum acceptable length of the string.
        /// </summary>
        public uint Max { get; set; } = DEFAULT_MAX;

        /// <summary>
        /// Gets or sets a message that will be returned by <see cref="StringLengthConstraintAttribute.Validate(object)"/>
        /// in <see cref="ElementaryConclusion.Details"/>
        /// </summary>
        public string FailureMessage { get; set; } = Resources.Attributes.StringLengthDefaultFailureMessage;

        /// <summary>
        /// Override of <see cref="ValidationAttribute.Validate(object)"/>
        /// </summary>
        /// <remarks>
        /// This method returns <c>true</c> if the <paramref name="obj"/> is <c>null</c>.  
        /// It is assumed the <see cref="RequiredPropertyAttribute"/> is used if the value may not be <c>null</c>.
        /// </remarks>
        /// <param name="obj">The string to validate</param>
        /// <returns>
        /// <see cref="ElementaryConclusion"/> with <c>IsValid</c> flag set to <c>true</c> 
        /// if the string length falls between min and max, inclusive. Otherwise, the flag is set to <c>false</c> and
        /// <see cref="ElementaryConclusion.Details"/> contains <see cref="FailureMessage"/>
        /// </returns>
        /// <exception cref="ArgumentException">Thrown if the current attribute is ill-formed.</exception>
        public override ElementaryConclusion Validate(object obj)
        {
            if (this.Min == DEFAULT_MIN && this.Max == DEFAULT_MAX)
            {
                throw new ArgumentException(Resources.Attributes.ArgumentExceptionConstraintNotSpecified);
            }
            if (this.Min > this.Max)
            {
                throw new ArgumentException(Resources.Attributes.ArgumentExceptionInvalidConstraints);
            }

            if (obj == null)
            {
                return new ElementaryConclusion(isValid: true);
            }

            int length;
            try
            {
                length = ((string)obj).Length;
            }
            catch
            {
                throw new ArgumentException(Resources.Attributes.ArgumentExceptionArgumentIsNotAString);
            }

            if ((length < this.Min) || (this.Max != 0 && length > this.Max))
            {
                return new ElementaryConclusion(isValid: false, this.FailureMessage);
            }

            return new ElementaryConclusion(isValid: true);
        }
    }
}
