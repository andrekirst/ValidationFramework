using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ValidationFramework
{
    public static class ValidatorExtensions
    {
        public static Validator<T> EnableCaching<T>(this Validator<T> validator)
        {
            if (validator == null)
            {
                throw new InvalidOperationException(message: "Validator is null");
            }

            validator.EnableCaching = true;

            return validator;
        }

        public static Validator<T> DisableCaching<T>(this Validator<T> validator)
        {
            if (validator == null)
            {
                throw new InvalidOperationException(message: "Validator is null");
            }

            validator.EnableCaching = false;

            return validator;
        }

        public static Validator<T> EnableReturnOnlyErrors<T>(this Validator<T> validator)
        {
            if (validator == null)
            {
                throw new InvalidOperationException(message: "Validator is null");
            }

            validator.ReturnOnlyErrors = true;

            return validator;
        }

        public static Validator<T> DisableReturnOnlyErrors<T>(this Validator<T> validator)
        {
            if (validator == null)
            {
                throw new InvalidOperationException(message: "Validator is null");
            }

            validator.ReturnOnlyErrors = false;

            return validator;
        }

        public static Validator<T> Add<T>(this Validator<T> validator, AbstractValidation<T> validation)
        {
            if (validator == null)
            {
                throw new InvalidOperationException(message: "Validator is null");
            }

            validator.AddValidation(validation: validation);

            return validator;
        }

        public static Validator<T> Add<T>(this Validator<T> validator, IEnumerable<AbstractValidation<T>> validations)
        {
            if (validator == null)
            {
                throw new InvalidOperationException(message: "Validator is null");
            }

            validator.AddValidation(validations: validations);

            return validator;
        }

        /// <summary>
        /// Returns true if all validations are successful
        /// </summary>
        /// <param name="validationResponse">The list of the response of the validations</param>
        /// <returns>True if all responses are successful</returns>
        public static bool IsAllValid(this IEnumerable<ValidationResponse> validationResponse)
        {
            return validationResponse.All(predicate: (val) => val.Type == ValidationResponseType.Success);
        }
    }
}
