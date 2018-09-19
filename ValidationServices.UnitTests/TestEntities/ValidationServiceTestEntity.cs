using ValidationServices.Attributes;

namespace ValidationServices.UnitTests.TestEntities
{
    public class ValidationServiceTestEntity
    {
        [RangeConstraint(Min = 0, Max = 9)]
        public int Digit { get; private set; }

        [RangeConstraint(Max = -1)]
        public int NegativeInteger { get; set; }

        [StringLengthConstraint(Min = 1, Max = 1)]
        public string OneCharString { get; set; }

        [RequiredProperty]
        public object RequiredObject { get; set; }

        [RequiredProperty(AllowEmptyStrings = false)]
        public string NotEmptyString { get; set; }

        public object SomeObject { get; set; }

        public ValidationServiceTestEntity(int digit, int negativeInteger,
            string oneCharString, object requiredObject,
            string notEmptyString, object someObject)
        {
            this.Digit = digit;
            this.NegativeInteger = negativeInteger;
            this.OneCharString = oneCharString;
            this.RequiredObject = requiredObject;
            this.NotEmptyString = notEmptyString;
            this.SomeObject = someObject;
        }
    }
}
