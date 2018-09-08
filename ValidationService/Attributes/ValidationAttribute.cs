using System;
using ValidationService.Results;

namespace ValidationService.Attributes
{
    public abstract class ValidationAttribute : Attribute, IValidator
    {
        public abstract ElementaryConclusion Validate(object obj);
    }
}
