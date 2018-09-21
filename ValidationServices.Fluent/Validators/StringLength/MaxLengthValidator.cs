using System;

namespace ValidationServices.Fluent.Validators.StringLength
{
    public class MaxLengthValidator : LengthValidator
    {
        public MaxLengthValidator(int max) : base(0, max)
        {
        }

        public MaxLengthValidator(Func<object, int> max) : base(obj => 0, max)
        {
        }
    }
}
