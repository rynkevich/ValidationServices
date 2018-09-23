using System;
using ValidationServices.Results;

namespace ValidationServices.Attributes
{
    /// <summary>
    /// Validation attribute to indicate a property that a property value is required.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public class RequiredPropertyAttribute : ValidationAttribute
    {

        /// <summary>
        /// Gets or sets a flag indicating whether the attribute should allow empty strings.
        /// A string that consists of whitespace, as well as zero length string, is considered empty.
        /// </summary>
        public bool AllowEmptyStrings { get; set; } = false;

        /// <summary>
        /// Gets or sets a message that will be returned by <see cref="RequiredPropertyAttribute.Validate(object)"/>
        /// in <see cref="ElementaryConclusion.Details"/>
        /// </summary>
        public string FailureMessage { get; set; } = Resources.Attributes.RequiredPropertyDefaultFailureMessage;

        /// <summary>
        /// Override of <see cref="ValidationAttribute.Validate(object)"/>
        /// </summary>
        /// <param name="objectToValidate">The object to validate</param>
        /// <returns>
        /// <see cref="ElementaryConclusion"/> with <c>IsValid</c> flag set to <c>true</c> 
        /// if the <paramref name="objectToValidate"/> is <c>null</c> or an empty string. Otherwise, the flag is set to <c>false</c> and
        /// <see cref="ElementaryConclusion.Details"/> contains <see cref="FailureMessage"/>.
        /// If <see cref="RequiredPropertyAttribute.AllowEmptyStrings"/>
        /// then <c>false</c> is returned in <c>IsValid</c> only if <paramref name="objectToValidate"/> is <c>null</c>.
        /// </returns>
        public override ElementaryConclusion Validate(object objectToValidate)
        {
            if (objectToValidate == null)
            {
                return new ElementaryConclusion(isValid: false, this.FailureMessage);
            }

            if (!this.AllowEmptyStrings)
            {
                if (objectToValidate is string stringObject && stringObject.Trim().Length == 0)
                {
                    return new ElementaryConclusion(isValid: false, this.FailureMessage);
                }
            }

            return new ElementaryConclusion(isValid: true);
        }
    }
}
