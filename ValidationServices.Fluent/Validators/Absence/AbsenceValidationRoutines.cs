using System.Collections;
using System.Linq;

namespace ValidationServices.Fluent.Validators.Absence
{
    /// <summary>
    /// Class with routines which are used by absence validators.
    /// </summary>
    static class AbsenceValidationRoutines
    {
        /// <summary>
        /// Checks if specified <paramref name="value"/> is an empty string.
        /// </summary>
        /// <param name="value">The value to check</param>
        /// <returns></returns>
        public static bool IsEmptyString(object value)
        {
            return (value is string str) && string.IsNullOrWhiteSpace(str);
        }

        /// <summary>
        /// Checks if specified <paramref name="value"/> is an empty collection.
        /// </summary>
        /// <param name="value">The value to check</param>
        /// <returns></returns>
        public static bool IsEmptyCollection(object value)
        {
            return value is IEnumerable collection && !collection.Cast<object>().Any();
        }
    }
}
