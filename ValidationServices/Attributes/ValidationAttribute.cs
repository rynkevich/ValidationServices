using System;
using ValidationServices.Results;

namespace ValidationServices.Attributes
{
    /// <summary>
    /// Base class for all validation attributes.
    /// <para><see cref="Validate(object)"/> should be overridden to implement validation logic.</para>
    /// </summary>
    public abstract class ValidationAttribute : Attribute
    {
        /// <summary>
        /// Gets the object indicating whether or not the specified <paramref name="objectToValidate"/> is valid
        /// with respect to the current validation attribute.
        /// </summary>
        /// <param name="objectToValidate">The object to validate</param>
        /// <returns>
        /// <see cref="ElementaryConclusion"/> with <c>IsValid</c> flag set to <c>true</c> 
        /// if the <paramref name="objectToValidate"/> is acceptable. Otherwise, the flag is set to <c>false</c> and
        /// <see cref="ElementaryConclusion.Details"/> contains a problem report. 
        /// </returns>
        public abstract ElementaryConclusion Validate(object objectToValidate);
    }
}
