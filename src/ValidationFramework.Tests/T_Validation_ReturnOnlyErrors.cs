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

        [Fact]
        [Trait(name: "Issue", value: "1")]
        public void ReturnOnlyError_true_Erwarte_1_Eintrag_von_2_Validierungen_EnableCaching_false()
        {
            Validator<int> validator = new Validator<int>()
            {
                ReturnOnlyErrors = true,
                EnableCaching = false
            };
            validator.AddValidation(val1);
            validator.AddValidation(val2);

            int actual = validator.Validate(3).Count();
            int expected = 1;

            Assert.Equal(expected: expected, actual: actual);
        }

        [Fact]
        [Trait(name: "Issue", value: "1")]
        public void ReturnOnlyError_true_Erwarte_1_Eintrag_von_2_Validierungen_EnableCaching_true()
        {
            Validator<int> validator = new Validator<int>()
            {
                ReturnOnlyErrors = true,
                EnableCaching = true
            };
            validator.AddValidation(val1);
            validator.AddValidation(val2);

            int actual = validator.Validate(3).Count();
            int expected = 1;

            Assert.Equal(expected: expected, actual: actual);
        }

        [Fact]
        [Trait(name: "Issue", value: "1")]
        public void ReturnOnlyError_false_Erwarte_e_Eintraege_von_2_Validierungen_EnableCaching_false()
        {
            Validator<int> validator = new Validator<int>()
            {
                ReturnOnlyErrors = false,
                EnableCaching = false
            };
            validator.AddValidation(val1);
            validator.AddValidation(val2);

            int actual = validator.Validate(3).Count();
            int expected = 2;

            Assert.Equal(expected: expected, actual: actual);
        }

        [Fact]
        [Trait(name: "Issue", value: "1")]
        public void ReturnOnlyError_false_Erwarte_2_Eintraege_von_2_Validierungen_EnableCaching_true()
        {
            Validator<int> validator = new Validator<int>()
            {
                ReturnOnlyErrors = false,
                EnableCaching = true
            };
            validator.AddValidation(val1);
            validator.AddValidation(val2);

            int actual = validator.Validate(3).Count();
            int expected = 2;

            Assert.Equal(expected: expected, actual: actual);
        }
    }
}
