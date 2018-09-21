﻿using ValidationServices.Results;

namespace ValidationServices.Fluent.Validators
{
    public interface IPropertyValidator
    {
        string FailureMessage { get; set; }
        ElementaryConclusion Validate(object objectToValidate, object propertyToValidate);
    }
}
