using System.Collections.Generic;
using ValidationService.Attributes;

namespace ValidationServiceTests.TestEntities {
    public class StringLengthConstraintFoobar {
        [StringLengthConstraint(Min = 5, Max = 10)]
        public string Ok1 { get; }

        [StringLengthConstraint(Min = 5, Max = 10)]
        public string Ok2 { get; } = "1234567";
        [StringLengthConstraint(Min = 5, Max = 10)]
        public string ShortNotOk2 { get; } = "123";
        [StringLengthConstraint(Min = 5, Max = 10)]
        public string LongNotOk2 { get; } = "12345678901";

        [StringLengthConstraint(Max = 10)]
        public string Ok3 { get; } = "123";
        [StringLengthConstraint(Max = 10)]
        public string NotOk3 { get; } = "12345678901";

        [StringLengthConstraint(Min = 5)]
        public string Ok4 { get; } = "12345678901";
        [StringLengthConstraint(Min = 5)]
        public string NotOk4 { get; } = "123";

        [StringLengthConstraint(Max = 7)]
        public HashSet<int> NotOk5 { get; } = new HashSet<int>();

        [StringLengthConstraint(Min = 77, Max = 7)]
        public string NotOk6 { get; } = "1234567";

        [StringLengthConstraint]
        public string NotOk7 { get; }
    }
}
