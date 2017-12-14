using System;
using System.Collections.Generic;
using System.Text;

namespace ValidationFramework.Examples.Example2
{
    public static class Example2
    {
        public static void RunExample()
        {
            /*
             * Simple example to validate an integer value with an validation in a seperate class. If the value is smaller than 3 then occurs a error.
             */

            Console.WriteLine($"# Start {nameof(Example2)}");
            Console.WriteLine($"Simple example to validate an integer value with an validation in a seperate class. If the value is smaller than 3 then occurs a error.");

            // Create new Validator with type int
            Validator<int> validator = new Validator<int>()
                // Add a new validation
                .Add(new ValidationCheckIntValueEqualOrGreatherThan3());

            int valuetoCheck = 2;

            Console.WriteLine("Validate the value");
            var responses = validator.Validate(value: valuetoCheck);

            foreach (var response in responses)
            {
                Console.WriteLine($"{response.Message}");
            }

            Console.WriteLine($"# End {nameof(Example2)}");
        }
    }
}
