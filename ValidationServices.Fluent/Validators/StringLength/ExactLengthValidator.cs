using System;

namespace ValidationServices.Fluent.Validators.StringLength
{
    public class ExactLengthValidator : LengthValidator
    {
        public ExactLengthValidator(int length) : base(length, length)
        {
        }

        public ExactLengthValidator(Func<object, int> length) : base(length, length)
        {
        }
    }
}
