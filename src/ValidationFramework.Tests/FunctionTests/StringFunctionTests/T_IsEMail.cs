﻿using System;
using System.Collections.Generic;
using System.Text;
using ValidationFramework.Functions;
using Xunit;

namespace ValidationFramework.Tests.FunctionTests.StringFunctionTests
{
    public class T_IsEMail
    {
        [Theory(DisplayName = "TestIsEMail")]
        [InlineData("test@test.de", true)]
        [InlineData("testtest.de", false)]
        [InlineData("testtestde", false)]
        [InlineData(null, false)]
        [InlineData("", false)]
        [Trait(name: "Function", value: "IsEMail")]
        [Trait(name: "Issue", value: "5")]
        public void TestIsEMail(string input, bool expectedValue)
        {
            bool actual = input.IsEMail();

            Assert.Equal(expected: expectedValue, actual: actual);
        }
    }
}
