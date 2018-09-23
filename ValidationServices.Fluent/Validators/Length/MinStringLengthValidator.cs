using System;

namespace ValidationServices.Fluent.Validators.Length
{
    /// <summary>
    /// Validator that verifies that length of a string property is greater than or equal to specified value.
    /// </summary>
    public class MinStringLengthValidator : StringLengthValidator
    {
        /// <summary>
        /// Initializes a new instance of <see cref="MinStringLengthValidator"/> class.
        /// </summary>
        /// <param name="min">The lower length constraint</param>
        public MinStringLengthValidator(int min) : base(min, MAX_NOT_SPECIFIED)
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="MinStringLengthValidator"/> class.
        /// </summary>
        /// <param name="minFunc">The lambda expression that returns the lower length constraint</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="minFunc"/> is null</exception>
        public MinStringLengthValidator(Func<object, int> minFunc) : base(minFunc, obj => MAX_NOT_SPECIFIED)
        {
        }
    }
}
