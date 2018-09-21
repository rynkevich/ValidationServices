using System;

namespace ValidationServices.Fluent.Validators.StringLength
{
    public class MinLengthValidator : LengthValidator
    {
        public MinLengthValidator(int min) : base(min, MAX_NOT_SPECIFIED)
        {
        }

        public MinLengthValidator(Func<object, int> min) : base(min, obj => MAX_NOT_SPECIFIED)
        {
        }
    }
}
