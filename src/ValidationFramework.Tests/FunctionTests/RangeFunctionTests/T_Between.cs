using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using ValidationFramework.Functions;

namespace ValidationFramework.Tests.FunctionTests.RangeFunctionTests
{
    public class T_Between
    {
        [Theory(DisplayName = "TestBetweenDouble")]
        [Trait(name: "Function", value: "Between")]
        [Trait(name: "Issue", value: "5")]
        [InlineData(3, 2, 4, true)]
        [InlineData(5, 2, 4, false)]
        public void TestBetweenDouble(double input, double start, double end, bool expectedValue)
        {
            bool actual = input.Between(start: start, end: end);

            Assert.Equal(expected: expectedValue, actual: actual);
        }

        [Theory(DisplayName = "TestBetweenFloat")]
        [Trait(name: "Function", value: "Between")]
        [Trait(name: "Issue", value: "5")]
        [InlineData(3, 2, 4, true)]
        [InlineData(5, 2, 4, false)]
        public void TestBetweenFloat(float input, float start, float end, bool expectedValue)
        {
            bool actual = input.Between(start: start, end: end);

            Assert.Equal(expected: expectedValue, actual: actual);
        }

        [Theory(DisplayName = "TestBetweenInt")]
        [Trait(name: "Function", value: "Between")]
        [Trait(name: "Issue", value: "5")]
        [InlineData(3, 2, 4, true)]
        [InlineData(5, 2, 4, false)]
        public void TestBetweenInt(int input, int start, int end, bool expectedValue)
        {
            bool actual = input.Between(start: start, end: end);

            Assert.Equal(expected: expectedValue, actual: actual);
        }

        [Theory(DisplayName = "TestBetweenDateTime")]
        [Trait(name: "Function", value: "Between")]
        [Trait(name: "Issue", value: "5")]
        [MemberData(nameof(TestBetweenDateTimeData))]
        public void TestBetweenDateTime(DateTime input, DateTime start, DateTime end, bool expectedValue)
        {
            bool actual = input.Between(start: start, end: end);

            Assert.Equal(expected: expectedValue, actual: actual);
        }

        public static IEnumerable<object[]> TestBetweenDateTimeData()
        {
            return new List<object[]>()
            {
                new object[] { new DateTime(2017, 12, 12), new DateTime(2017, 1, 1), new DateTime(2017, 12, 31), true },
                new object[] { new DateTime(2017, 12, 12), new DateTime(2017, 1, 1), new DateTime(2017, 11, 11), false }
            };
        }
    }
}
