using System;
using System.Collections.Generic;
using System.Text;
using ValidationFramework.Functions;
using Xunit;

namespace ValidationFramework.Tests.FunctionTests.StringFunctionTests
{
    public class T_Empty
    {
        [Theory]
        [InlineData(null, false)]
        [InlineData("", true)]
        [InlineData("a", false)]
        [Trait(name: "Issue", value: "5")]
        [Trait(name: "Function", value: "IsEmpty")]
        public void TestIsEmpty(string input, bool expectedValue)
        {
            bool actual = input.IsEmpty();

            Assert.Equal(expected: expectedValue, actual: actual);
        }
    }
}
