using System;

namespace ValidationServices.Fluent.Validators.Length
{
    /// <summary>
    /// Validator that verifies that length of a string property equal to specified value.
    /// </summary>
    public class ExactStringLengthValidator : StringLengthValidator
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ExactStringLengthValidator"/> class.
        /// </summary>
        /// <param name="length">The required string length</param>
        public ExactStringLengthValidator(int length) : base(length, length)
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="ExactStringLengthValidator"/> class.
        /// </summary>
        /// <param name="length">The lambda expression that provides the required string length</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="length"/> is null</exception>
        public ExactStringLengthValidator(Func<object, int> length) : base(length, length)
        {
        }
    }
}
