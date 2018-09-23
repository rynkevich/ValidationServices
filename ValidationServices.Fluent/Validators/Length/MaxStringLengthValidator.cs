using System;

namespace ValidationServices.Fluent.Validators.Length
{
    /// <summary>
    /// Validator that verifies that length of a string property is less than or equal to specified value.
    /// </summary>
    public class MaxStringLengthValidator : StringLengthValidator
    {
        /// <summary>
        /// Initializes a new instance of <see cref="MaxStringLengthValidator"/> class.
        /// </summary>
        /// <param name="max">The upper length constraint</param>
        public MaxStringLengthValidator(int max) : base(0, max)
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="MaxStringLengthValidator"/> class.
        /// </summary>
        /// <param name="maxFunc">The lambda expression that returns the upper length constraint</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="maxFunc"/> is null</exception>
        public MaxStringLengthValidator(Func<object, int> maxFunc) : base(obj => 0, maxFunc)
        {
        }
    }
}
