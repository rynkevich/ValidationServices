using ValidationService.Results;

namespace ValidationService {
    interface IValidator {
        ElementaryConclusion Validate(object obj);
    }
}
