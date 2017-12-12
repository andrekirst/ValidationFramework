using System;
using System.Collections.Generic;
using System.Text;
using ValidationFramework.Functions;
using Xunit;

namespace ValidationFramework.Tests.FunctionTests.StringFunctionTests
{
    public class T_IsIPv4Address
    {
        [Theory(DisplayName = "TestIsIPv4Address")]
        [InlineData("192.168.0.1", true)]
        [InlineData("192.168.00.01", true)]
        [InlineData("192.168.000.001", true)]
        [InlineData("192168000001", false)]
        [InlineData("a", false)]
        [InlineData("192-168-0-1", false)]
        [InlineData("192,168,0,1", false)]
        [InlineData("192.168.0.1a", false)]
        [InlineData(null, false)]
        [InlineData("", false)]
        [Trait(name: "Function", value: "IsIPv4Address")]
        [Trait(name: "Issue", value: "5")]
        public void TestIsIPv4Address(string input, bool expectedValue)
        {
            bool actual = input.IsIPv4Address();

            Assert.Equal(expected: expectedValue, actual: actual);
        }
    }
}
