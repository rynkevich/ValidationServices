using System.Collections.Generic;

namespace ValidationServices.Fluent.UnitTests.TestEntities
{
    public class ServiceTestEntity
    {
        public string StringField = "A string";

        public string WhitespaceString { get; } = "  ";
        public string NullString { get; } = null;
        public string EightCharString { get; } = "A string";
        public string NineCharString { get; } = "123456789";

        public int Seven { get; } = 7;
        public int Eight { get; } = 8;
        public int Nine { get; } = 9;
        public int Ten { get; } = 10;

        public int GetFive()
        {
            return 5;
        }
    }
}
