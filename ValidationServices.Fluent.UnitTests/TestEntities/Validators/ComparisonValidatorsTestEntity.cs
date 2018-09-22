namespace ValidationServices.Fluent.UnitTests.TestEntities.Validators
{
    public class ComparisonValidatorsTestEntity
    {
        public string NullString { get; } = null;
        public string FooString { get; } = "Foo";

        public object NullObject = null;
        public static object SomeObject { get; } = 47;
        public object SameObject { get; } = SomeObject;

        public int Five { get; } = 5;
        public int Eight { get; } = 8;
        public int Nine { get; } = 9;
        public int Ten { get; } = 10;
    }
}
