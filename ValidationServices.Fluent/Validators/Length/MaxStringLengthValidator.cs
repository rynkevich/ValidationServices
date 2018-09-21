using System;

namespace ValidationServices.Fluent.Validators.Length
{
    public class MaxStringLengthValidator : StringLengthValidator
    {
        public MaxStringLengthValidator(int max) : base(0, max)
        {
        }

        public MaxStringLengthValidator(Func<object, int> max) : base(obj => 0, max)
        {
        }
    }
}
