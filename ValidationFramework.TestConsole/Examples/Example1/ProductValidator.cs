using System;
using System.Collections.Generic;
using System.Text;
using ValidationFramework.TestConsole.Examples.Example1.Model;
using ValidationFramework.TestConsole.Examples.Example1.Validations;

namespace ValidationFramework.TestConsole.Examples.Example1
{
    public class ProductValidator : Validator<Product>
    {
        public ProductValidator()
        {
            AddValidation(new ProductValidationNameNotBeEmpty());
        }
    }
}
