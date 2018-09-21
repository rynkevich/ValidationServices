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
    }
}
