using System;

namespace ValidationFramework.Examples.Example2
{
    public class ValidationCheckIntValueEqualOrGreatherThan3 : AbstractValidation<int>
    {
        public override string Name => "CHECK_VALUE_EQUAL_OR_GREATHER_THAN_3";

        public override Func<int, bool> ValidationFunction => (i) => i >= 3;

        public override string MessageOnError => "Value is smaller than 3";

        public override Func<int, object> OriginalValue => (i) => i;

        public override string MessageOnSuccess => throw new NotImplementedException();
    }
}
