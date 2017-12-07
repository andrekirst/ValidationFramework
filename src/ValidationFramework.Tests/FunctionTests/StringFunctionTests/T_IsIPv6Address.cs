using System;
using System.Collections.Generic;
using System.Text;
using ValidationFramework.Functions;
using Xunit;

namespace ValidationFramework.Tests.FunctionTests.StringFunctionTests
{
    public class T_IsIPv6Address
    {
        [Theory(DisplayName = "TestIsIPv6Address")]
        [InlineData("FE80:0000:0000:0000:0202:B3FF:FE1E:8329", true)]
        [InlineData("FE80::0202:B3FF:FE1E:8329", true)]
        [InlineData("2001:cdba:0000:0000:0000:0000:3257:9652", true)]
        [InlineData("2001:cdba:0:0:0:0:3257:9652", true)]
        [InlineData("2001:cdba::3257:9652", true)]
        [InlineData("192.168.0.1", false)]
        [InlineData("192.168.00.01", false)]
        [InlineData("192.168.000.001", false)]
        [InlineData("192168000001", false)]
        [InlineData("a", false)]
        [InlineData("192-168-0-1", false)]
        [InlineData("192,168,0,1", false)]
        [InlineData("192.168.0.1a", false)]
        [InlineData(null, false)]
        [InlineData("", false)]
        [Trait(name: "Function", value: "IsIPv4Address")]
        [Trait(name: "Issue", value: "5")]
        public void TestIsIPv6Address(string input, bool expectedValue)
        {
            bool actual = input.IsIPv6Address();

            Assert.Equal(expected: expectedValue, actual: actual);
        }
    }
}
