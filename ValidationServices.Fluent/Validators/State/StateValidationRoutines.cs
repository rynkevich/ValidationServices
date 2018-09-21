using System.Collections;
using System.Linq;

namespace ValidationServices.Fluent.Validators.State
{
    static class StateValidationRoutines
    {
        public static bool IsEmptyString(object value)
        {
            return (value is string str) && string.IsNullOrWhiteSpace(str);
        }

        public static bool IsEmptyCollection(object value)
        {
            return value is IEnumerable collection && !collection.Cast<object>().Any();
        }
    }
}
