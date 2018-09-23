using ValidationServices.Results;

namespace ValidationServices.Service
{
    /// <summary>
    /// Base class for object validation services
    /// <para><see cref="Validate{T}(T, string)"/> should be overridden to implement validation logic.</para>
    /// </summary>
    public abstract class BaseValidationService
    {
        /// <summary>
        /// Gets or sets a flag indicating whether the validation should be accomplished recursively.
        /// </summary>
        abstract public bool IsRecursiveValidation { get; set; }

        /// <summary>
        /// Gets the object indicating whether or not the specified <paramref name="objectToValidate"/> is valid
        /// </summary>
        /// <typeparam name="T">The type of object to be validated</typeparam>
        /// <param name="objectToValidate">The object to validate</param>
        /// <param name="objectName">The object name. 
        /// Used to print full qualified property names to <see cref="ServiceConclusion.Details"/></param>
        /// <returns>
        /// <see cref="ServiceConclusion"/> with <c>IsValid</c> flag set to <c>true</c> 
        /// if the <paramref name="objectToValidate"/> is acceptable. Otherwise, the flag is set to <c>false</c> and
        /// <see cref="ServiceConclusion.Details"/> contains a report on problems.
        /// </returns>
        public abstract ServiceConclusion Validate<T>(T objectToValidate, string objectName = "") where T : class;
    }
}
    