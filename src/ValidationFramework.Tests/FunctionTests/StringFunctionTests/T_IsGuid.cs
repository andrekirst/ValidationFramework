using System;
using System.Collections.Generic;
using System.Text;
using ValidationFramework.Functions;
using Xunit;

namespace ValidationFramework.Tests.FunctionTests.StringFunctionTests
{
    public class T_IsGuid
    {
        [Theory(DisplayName = "TestIsGuid")]
        [InlineData(null, false)]
        [InlineData("", false)]
        [InlineData("d1883abb-bdf6-4515-bb14-55376c6cca70", true)]
        [InlineData("EF9E7864-BBAD-4F3B-A658-64FBC443C7FC", true)]
        [InlineData("{EF9E7864-BBAD-4F3B-A658-64FBC443C7FC}", true)]
        [Trait(name: "Function", value: "IsGuid")]
        [Trait(name: "Issue", value: "5")]
        public void TestIsGuid(string input, bool expectedValue)
        {
            bool actual = input.IsGuid();

            Assert.Equal(expected: expectedValue, actual: actual);
        }
    }
}
