using System.Collections.Generic;
using Xunit;

namespace ValidationFramework.Tests
{
    public class T_ValidatorExtensions
    {
        [Fact(DisplayName = "TestFluentDesignEnableCaching")]
        [Trait(name: "Issue", value: "6")]
        [Trait(name: "Function", value: "EnableCaching")]
        public void TestFluentDesignEnableCaching()
        {
            Validator<int> validator = new Validator<int>()
                .EnableCaching();

            bool expected = true;
            bool actual = validator.EnableCaching;

            Assert.Equal(expected: expected, actual: actual);
        }

        [Fact(DisplayName = "TestFluentDesignDisableCaching")]
        [Trait(name: "Issue", value: "6")]
        [Trait(name: "Function", value: "DisableCaching")]
        public void TestFluentDesignDisableCaching()
        {
            Validator<int> validator = new Validator<int>()
                .DisableCaching();

            bool expected = false;
            bool actual = validator.EnableCaching;

            Assert.Equal(expected: expected, actual: actual);
        }

        [Fact(DisplayName = "TestFluentDesignEnableReturnOnlyErrors")]
        [Trait(name: "Issue", value: "6")]
        [Trait(name: "Function", value: "EnableReturnOnlyErrors")]
        public void TestFluentDesignEnableReturnOnlyErrors()
        {
            Validator<int> validator = new Validator<int>()
                .EnableReturnOnlyErrors();

            bool expected = true;
            bool actual = validator.ReturnOnlyErrors;

            Assert.Equal(expected: expected, actual: actual);
        }

        [Fact(DisplayName = "TestFluentDesignDisableReturnOnlyErrors")]
        [Trait(name: "Issue", value: "6")]
        [Trait(name: "Function", value: "DisableReturnOnlyErrors")]
        public void TestFluentDesignDisableReturnOnlyErrors()
        {
            Validator<int> validator = new Validator<int>()
                .DisableReturnOnlyErrors();

            bool expected = false;
            bool actual = validator.ReturnOnlyErrors;

            Assert.Equal(expected: expected, actual: actual);
        }

        [Fact(DisplayName = "TestFluentDesignAdd")]
        [Trait(name: "Issue", value: "6")]
        [Trait(name: "Function", value: "Add")]
        public void TestFluentDesignAdd()
        {
            Validator<int> validator = new Validator<int>()
                .Add(new Validation<int>(
                    messageOnError: "ERR",
                    messageOnSuccess: "SUC",
                    name: "MOD",
                    originalValue: (i) => i,
                    validationFunction: (i) => i % 2 == 0));

            Assert.Equal(expected: 1, actual: validator.Validations.Count);
        }

        [Fact(DisplayName = "TestFluentDesignAddIEnumerable")]
        [Trait(name: "Issue", value: "6")]
        [Trait(name: "Function", value: "Add")]
        public void TestFluentDesignAddIEnumerable()
        {
            List<AbstractValidation<int>> list = new List<AbstractValidation<int>>();
            list.Add(new Validation<int>(
                    messageOnError: "ERR",
                    messageOnSuccess: "SUC",
                    name: "MOD",
                    originalValue: (i) => i,
                    validationFunction: (i) => i % 2 == 0));

            list.Add(new Validation<int>(
                    messageOnError: "ERR",
                    messageOnSuccess: "SUC",
                    name: "MOD2",
                    originalValue: (i) => i,
                    validationFunction: (i) => i + 1 % 2 == 0));

            Validator<int> validator = new Validator<int>()
                .Add(validations: list);

            Assert.Equal(expected: 2, actual: validator.Validations.Count);
        }

        [Fact(DisplayName = "TestIsAllValid_2_Error_Expect_False")]
        [Trait(name: "Issue", value: "10")]
        [Trait(name: "Function", value: "IsAllValid")]
        public void TestIsAllValid_2_Error_Expect_False()
        {
            List<AbstractValidation<int>> list = new List<AbstractValidation<int>>();
            list.Add(new Validation<int>(
                    messageOnError: "ERR",
                    messageOnSuccess: "SUC",
                    name: "MOD",
                    originalValue: (i) => i,
                    validationFunction: (i) => i < 3));

            list.Add(new Validation<int>(
                    messageOnError: "ERR",
                    messageOnSuccess: "SUC",
                    name: "MOD2",
                    originalValue: (i) => i,
                    validationFunction: (i) => i < 4));

            Validator<int> validator = new Validator<int>()
                .Add(validations: list);

            bool actual = validator.Validate(value: 5).IsAllValid();
            bool expected = false;

            Assert.Equal(expected: expected, actual: actual);
        }

        [Fact(DisplayName = "TestIsAllValid_0_Error_Expect_True")]
        [Trait(name: "Issue", value: "10")]
        [Trait(name: "Function", value: "IsAllValid")]
        public void TestIsAllValid_0_Error_Expect_True()
        {
            List<AbstractValidation<int>> list = new List<AbstractValidation<int>>();
            list.Add(new Validation<int>(
                    messageOnError: "ERR",
                    messageOnSuccess: "SUC",
                    name: "MOD",
                    originalValue: (i) => i,
                    validationFunction: (i) => i < 3));

            list.Add(new Validation<int>(
                    messageOnError: "ERR",
                    messageOnSuccess: "SUC",
                    name: "MOD2",
                    originalValue: (i) => i,
                    validationFunction: (i) => i < 4));

            Validator<int> validator = new Validator<int>()
                .Add(validations: list);

            bool actual = validator.Validate(value: 2).IsAllValid();
            bool expected = true;

            Assert.Equal(expected: expected, actual: actual);
        }
    }
}
