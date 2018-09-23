using System.Collections.Generic;

namespace ValidationServices.Fluent.UnitTests.TestEntities
{
    public class ValidatorsTestEntity
    {
        public string NullString { get; } = null;
        public string EightCharString { get; } = "A string";

        public object NullObject = null;
        public static object SomeObject { get; } = 47;
        public object SameObject { get; } = SomeObject;

        public string EmptyString { get; } = "";
        public string WhitespaceString { get; } = "  ";
        public List<int> EmptyList { get; } = new List<int>();
        public string NotEmptyString { get; } = "str";
        public List<int> NotEmptyList { get; } = new List<int> { 1, 2, 3 };

        public int Five { get; } = 5;
        public int Eight { get; } = 8;
        public int Nine { get; } = 9;
        public int Ten { get; } = 10;
        public int AnotherTen { get; } = 10;
    }
}
