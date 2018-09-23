using System;

namespace ValidationServices.Fluent.Internal
{
    /// <summary>
    /// Extension methods for internal use.
    /// </summary>
    public static class Extensions
    { 
        /// <summary>
        /// Throws <see cref="ArgumentNullException"/> if obj is null.
        /// </summary>
        /// <param name="obj">The object to check</param>
        /// <param name="paramName">The object name to include in exception</param>
        /// <param name="message">The message to include in exception</param>
        internal static void Guard(this object obj, string paramName, string message = null)
        {
            if (obj == null)
            {
                throw (message == null) ? new ArgumentNullException(paramName) : new ArgumentNullException(paramName, message);
            }
        }

#pragma warning disable 1591
        public static Func<object, int> CoerceToNonGeneric<T>(this Func<T, int> function)
        {
            return x => function((T)x);
        }

        public static Func<object, object> CoerceToNonGeneric<T, K>(this Func<T, K> function)
        {
            return x => function((T)x);
        }
#pragma warning restore 1591
    }
}
