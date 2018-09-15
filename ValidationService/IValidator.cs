using ValidationService.Results;

namespace ValidationService
{
    /// <summary>
    /// Interface to be implemented by any class that provides object validation.
    /// </summary>
    interface IValidator
    {
        /// <summary>
        /// Gets the object indicating whether or not the specified <paramref name="obj"/> is valid
        /// with respect to the current validator.
        /// </summary>
        /// <param name="obj">The object to validate</param>
        /// <returns>
        /// <see cref="ElementaryConclusion"/> with <c>IsValid</c> flag set to <c>true</c> 
        /// if the <paramref name="obj"/> is acceptable. Otherwise, the flag is set to <c>false</c> and
        /// <see cref="ElementaryConclusion.Details"/> contains a problem report. 
        /// </returns>
        ElementaryConclusion Validate(object obj);
    }
}
