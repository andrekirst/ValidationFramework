using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using ValidationFramework.Functions;

namespace ValidationFramework.Tests.FunctionTests.StringFunctionTests
{
    public class T_MatchesRegexp
    {
        [Theory(DisplayName = "TestMatchesRegexpData")]
        [InlineData("a", "a", true)]
        [InlineData("0", "a", false)]
        [Trait(name: "Issue", value: "5")]
        [Trait(name: "Function", value: "MatchesRegexp")]
        public void TestMatchesRegexpData(string input, string pattern, bool expectedValue)
        {
            bool actual = input.MatchesRegexp(pattern: pattern);

            Assert.Equal(expected: expectedValue, actual: actual);
        }

        [Fact(DisplayName = "TestMatchesRegexpValueIsNullExpectArgumentNullException")]
        [Trait(name: "Issue", value: "5")]
        [Trait(name: "Function", value: "MatchesRegexp")]
        public void TestMatchesRegexpValueIsNullExpectArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(testCode: () =>
            {
                string s = null;
                bool test = s.MatchesRegexp(pattern: "a-z");
            });
        }

        [Fact(DisplayName = "TestMatchesRegexpPatternIsNullExpectArgumentNullException")]
        [Trait(name: "Issue", value: "5")]
        [Trait(name: "Function", value: "MatchesRegexp")]
        public void TestMatchesRegexpPatternIsNullExpectArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(testCode: () =>
            {
                string s = "a";
                bool test = s.MatchesRegexp(pattern: null);
            });
        }
    }
}
