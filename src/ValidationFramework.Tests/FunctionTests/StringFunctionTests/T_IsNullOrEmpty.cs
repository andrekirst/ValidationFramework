using System;
using System.Collections.Generic;
using System.Text;
using ValidationFramework.Functions;
using Xunit;

namespace ValidationFramework.Tests.FunctionTests.StringFunctionTests
{
    public class T_IsNullOrEmpty
    {
        [Theory(DisplayName = "TestIsNullOrEmpty")]
        [InlineData(null, true)]
        [InlineData("", true)]
        [InlineData("a", false)]
        [Trait(name: "Issue", value: "5")]
        [Trait(name: "Function", value: "IsNullOrEmpty")]
        public void TestIsNullOrEmpty(string input, bool expectedValue)
        {
            bool actual = input.IsNullOrEmpty();

            Assert.Equal(expected: expectedValue, actual: actual);
        }
    }
}
