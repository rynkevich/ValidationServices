using System.Collections.Generic;

namespace ValidationServices.Fluent.UnitTests.TestEntities.Validators
{
    public class AbsenceValidatorsTestEntity
    {
        public object NullObject { get; } = null;
        public string EmptyString { get; } = "";
        public string WhitespaceString { get; } = "  ";
        public List<int> EmptyList { get; } = new List<int>();

        public string NotEmptyString { get; } = "str";
        public List<int> NotEmptyList { get; } = new List<int> { 1, 2, 3 };
    }
}
