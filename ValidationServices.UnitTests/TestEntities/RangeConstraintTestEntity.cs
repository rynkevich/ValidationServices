using ValidationServices.Attributes;

namespace ValidationServices.UnitTests.TestEntities
{
    public class RangeConstraintTestEntity
    {
        [RangeConstraint(Min = "AC", Max = "AG")]
        public string Ok1 { get; }

        [RangeConstraint(Min = "AC", Max = "AG")]
        public string Ok2 { get; } = "AE";
        [RangeConstraint(Min = "AC", Max = "AG")]
        public string SmallNotOk2 { get; } = "AA";
        [RangeConstraint(Min = "AC", Max = "AG")]
        public string LargeNotOk2 { get; } = "AL";

        [RangeConstraint(Max = 77)]
        public uint Ok3 { get; } = 11;
        [RangeConstraint(Max = 77)]
        public uint NotOk3 { get; } = 999;

        [RangeConstraint(Min = 5.56)]
        public double Ok4 { get; } = 5.91;
        [RangeConstraint(Min = 5.56)]
        public double NotOk4 { get; } = -4.99;

        [RangeConstraint]
        public string NotOk5 { get; }

        [RangeConstraint(Min = 13, Max = 0)]
        public int NotOk6 { get; }

        [RangeConstraint(Min = 13, Max = "AD")]
        public int NotOk7 { get; }

        [RangeConstraint(Min = 3, Max = 7)]
        public string NotOk8 { get; } = "a string";
    }
}
