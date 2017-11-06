using System;
using System.Collections.Generic;
using System.Text;
using ValidationFramework.TestConsole.Examples.Example1.Model;

namespace ValidationFramework.TestConsole.Examples.Example1.Validations
{
    public class ProductValidationNameNotBeEmpty : AbstractValidation<Product>
    {
        public override string Name => nameof(ProductValidationNameNotBeEmpty);

        public override Func<Product, bool> ValidationFunction => (p) => !string.IsNullOrEmpty(p.Name);

        public override string MessageOnError => "Name of product can not be empty";

        public override Func<Product, object> OriginalValue => (p) => p.Name;

        public override string MessageOnSuccess => "Name of product is valid";
    }
}
