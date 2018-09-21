using System;

namespace ValidationServices.Fluent.Internal
{
    public static class Extensions
    {
        public static Func<object, int> CoerceToNonGeneric<T>(this Func<T, int> function)
        {
            return x => function((T)x);
        }
    }
}
