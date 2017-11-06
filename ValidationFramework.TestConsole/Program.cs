using System;
using System.Collections.Generic;
using ValidationFramework.TestConsole.Examples.Example1;
using ValidationFramework.TestConsole.Examples.Example1.Model;
using ValidationFramework.TestConsole.Examples.Example1.Validations;

namespace ValidationFramework.TestConsole
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            ShowExamples();
        }

        private static void ShowExamples()
        {
            // Example1 - Full Example
            ProductValidator productValidator = new ProductValidator();

            Product validProduct = new Product() { Name = "Name of product" };
            Product invalidProduct = new Product() { Name = "" };

            List<Product> products = new List<Product>()
            {
                validProduct,
                invalidProduct
            };

            var responses = productValidator.Validate(values: products);

            foreach (ValidationResponse response in responses)
            {
                Console.WriteLine(value: response.Message);
            }

            // Example1 - Testable

            AbstractValidation<Product> validation = new ProductValidationNameNotBeEmpty();
            bool actual = validation.IsValid(validProduct);
            bool expected = true;

            //Assert.AreEqual(expected, actual);
        }
    }
}
