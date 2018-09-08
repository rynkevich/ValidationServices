using ValidationService.Results;

namespace ValidationService
{
    public abstract class ValidationService
    {
        public bool IsRecursiveValidation { get; protected set; }
        public abstract GeneralConclusion Validate(object obj, string objName = "");
    }
}
