using System.Collections.Generic;
using System.Linq;

namespace ValidationService.Results {
    public class GeneralConclusion : ValidationConclusion {
        public List<string> Details { get; protected set; }

        public GeneralConclusion(bool isValid, List<string> details = null) {
            this.IsValid = isValid;
            this.Details = details;
        }

        public static GeneralConclusion operator +(GeneralConclusion a, GeneralConclusion b) {
            bool isValid = a.IsValid && b.IsValid;

            if (a.Details == null && b.Details == null) {
                return new GeneralConclusion(isValid);
            }

            if (a.Details == null) {
                return new GeneralConclusion(isValid, new List<string>(b.Details));
            } else if (b.Details == null) {
                return new GeneralConclusion(isValid, new List<string>(a.Details));
            } else {
                return new GeneralConclusion(isValid, a.Details.Concat(b.Details).ToList<string>());
            }
        }

        public static GeneralConclusion operator +(GeneralConclusion a, ElementaryConclusion b) {
            bool isValid = a.IsValid && b.IsValid;
            List<string> details = null;

            if (a.Details != null) {
                details = new List<string>(a.Details);
            }

            if (b.Details != null) {
                details = details ?? new List<string>();
                details.Add(b.Details);
            }

            return new GeneralConclusion(isValid, details);
        }
    }
}
