using System.Collections.Generic;
using ValidationFramework.Functions;
using Xunit;

namespace ValidationFramework.Tests.FunctionTests.ListFunctionTests
{
    public class T_In
    {
        [Fact(DisplayName = "TestTestBetween_2_In_1_2_3_Erwarte_True")]
        [Trait(name: "Function", value: "In")]
        [Trait(name: "Issue", value: "5")]
        public void TestTestBetween_2_In_1_2_3_Erwarte_True()
        {
            List<int> list1 = new List<int>() { 1, 2, 3 };
            int number = 2;

            bool actual = number.In(list1);
            bool expected = true;

            Assert.Equal(expected: expected, actual: actual);
        }

        [Fact(DisplayName = "TestTestBetween_4_In_1_2_3_Erwarte_False")]
        [Trait(name: "Function", value: "In")]
        [Trait(name: "Issue", value: "5")]
        public void TestTestBetween_4_In_1_2_3_Erwarte_False()
        {
            List<int> list1 = new List<int>() { 1, 2, 3 };
            int number = 4;

            bool actual = number.In(list1);
            bool expected = false;

            Assert.Equal(expected: expected, actual: actual);
        }
    }
}
