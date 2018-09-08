using ValidationService.Results;

namespace ValidationService
{
    public abstract class ValidationService
    {
        public bool IsRecursiveValidation { get; protected set; }
        public abstract GeneralConclusion Validate<T>(T obj, string objName = "") where T : class;
    }
}
