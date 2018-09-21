namespace ValidationServices.Fluent.UnitTests.TestEntities.Validators
{
    public class StringLengthValidatorsTestEntity
    {
        public string NullString { get; } = null;
        public string EightCharString { get; } = "A string";

        public int Five { get; } = 5;
        public int Eight { get; } = 8;
        public int Nine { get; } = 9;
        public int Ten { get; } = 10;
    }
}
