using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
