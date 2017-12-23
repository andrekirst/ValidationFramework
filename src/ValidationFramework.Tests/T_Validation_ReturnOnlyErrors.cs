using System.Linq;
using Xunit;

namespace ValidationFramework.Tests
{
    public class T_Validation_ReturnOnlyErrors
    {
        private Validation<int> val1 = new Validation<int>(
                messageOnError: "E1",
                messageOnSuccess: "S1",
                name: "Val1",
                originalValue: (i) => i,
                validationFunction: (i) => i < 3);

        private Validation<int> val2 = new Validation<int>(
                messageOnError: "E2",
                messageOnSuccess: "S2",
                name: "Val2",
                originalValue: (i) => i,
                validationFunction: (i) => i < 5);

        [Theory(DisplayName = "Test_ReturnOnlyErrors_2_Validierungen")]
        [Trait(name: "Issue", value: "2")]
        [InlineData(true, false, 3, 1)]
        [InlineData(true, true, 3, 1)]
        [InlineData(false, false, 3, 2)]
        [InlineData(false, true, 3, 2)]
        [InlineData(true, false, 6, 2)]
        [InlineData(true, true, 6, 2)]
        [InlineData(false, false, 6, 2)]
        [InlineData(false, true, 6, 2)]
        public void Test_ReturnOnlyErrors_2_Validierungen(bool returnOnlyError, bool enableCaching, int inputValue, int expectedCount)
        {
            SingleObjectValidator<int> validator = new SingleObjectValidator<int>()
            {
                ReturnOnlyErrors = returnOnlyError,
                EnableCaching = enableCaching
            };
            validator.AddValidation(val1);
            validator.AddValidation(val2);

            int actual = validator.ValidateSingleValue(inputValue).Count();

            Assert.Equal(expected: expectedCount, actual: actual);
        }
    }
}
