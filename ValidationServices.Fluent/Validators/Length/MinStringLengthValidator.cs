using System;

namespace ValidationServices.Fluent.Validators.Length
{
    public class MinStringLengthValidator : StringLengthValidator
    {
        public MinStringLengthValidator(int min) : base(min, MAX_NOT_SPECIFIED)
        {
        }

        public MinStringLengthValidator(Func<object, int> minFunc) : base(minFunc, obj => MAX_NOT_SPECIFIED)
        {
        }
    }
}
