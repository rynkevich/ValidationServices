using System;

namespace ValidationServices.Fluent.Validators.Length
{
    public class MaxStringLengthValidator : StringLengthValidator
    {
        public MaxStringLengthValidator(int max) : base(0, max)
        {
        }

        public MaxStringLengthValidator(Func<object, int> maxFunc) : base(obj => 0, maxFunc)
        {
        }
    }
}
