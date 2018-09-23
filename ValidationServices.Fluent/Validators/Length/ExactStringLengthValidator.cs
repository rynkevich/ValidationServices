using System;

namespace ValidationServices.Fluent.Validators.Length
{
    public class ExactStringLengthValidator : StringLengthValidator
    {
        public ExactStringLengthValidator(int length) : base(length, length)
        {
        }

        public ExactStringLengthValidator(Func<object, int> length) : base(length, length)
        {
        }
    }
}
