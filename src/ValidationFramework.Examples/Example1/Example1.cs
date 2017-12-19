using System;
using System.Collections.Generic;
using System.Text;

namespace ValidationFramework.Examples.Example1
{
    public static class Example1
    {
        public static void RunExample()
        {
            /*
             * Simple example to validate an integer value with an inline validation. If the value is smaller than 3 then occurs a error.
             */

            Console.WriteLine($"# Start {nameof(Example1)}");
            Console.WriteLine($"Simple example to validate an integer value with an inline validation. If the value is smaller than 3 then occurs a error.");

            // Create new Validator with type int
            Validator<int> validator = new Validator<int>()
                // Add a new (inline) validation
                .Add(new Validation<int>(
                    messageOnError: "Value is smaller than 3",
                    messageOnSuccess: "Value is equal or greather than 3",
                    name: "CHECK_VALUE_EQUAL_OR_GREATHER_THAN_3",
                    originalValue: (i) => i,
                    validationFunction: (i) => i >= 3));

            int valuetoCheck = 2;

            Console.WriteLine("Validate the value");
            var responses = validator.ValidateSingleValue(value: valuetoCheck);

            foreach (var response in responses)
            {
                Console.WriteLine($"{response.Message}");
            }

            Console.WriteLine($"# End {nameof(Example1)}");
        }
    }
}
