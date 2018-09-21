using System;

namespace ValidationServices.Fluent.Internal
{
    public static class Extensions
    {
        public static Func<object, int> CoerceToNonGeneric<T>(this Func<T, int> function)
        {
            return x => function((T)x);
        }

        public static Func<object, object> CoerceToNonGeneric<T, K>(this Func<T, K> function)
        {
            return x => function((T)x);
        }

        internal static void Guard(this object obj, string paramName, string message = null)
        {
            if (obj == null)
            {
                throw (message == null) ? new ArgumentNullException(paramName) : new ArgumentNullException(paramName, message);
            }
        }
    }
}
