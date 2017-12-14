using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ValidationFramework
{
    /// <summary>
    /// Class with extension methods for <see cref="Validator{T}"/>
    /// </summary>
    public static class ValidatorExtensions
    {
        /// <summary>
        /// Enables the caching
        /// </summary>
        /// <typeparam name="T">The Type to validate</typeparam>
        /// <param name="validator">The validator</param>
        /// <returns>Returns the Instance of <see cref="Validator{T}"/> with <see cref="Validator{T}.EnableCaching"/> set on true</returns>
        /// <exception cref="InvalidOperationException">Occurs if the instance of <see cref="Validator{T}"/> is null</exception>
        public static Validator<T> EnableCaching<T>(this Validator<T> validator)
        {
            if (validator == null)
            {
                throw new InvalidOperationException(message: "Validator is null");
            }

            validator.EnableCaching = true;

            return validator;
        }

        /// <summary>
        /// Disables the caching
        /// </summary>
        /// <typeparam name="T">The Type to validate</typeparam>
        /// <param name="validator">The validator</param>
        /// <returns>Returns the Instance of <see cref="Validator{T}"/> with <see cref="Validator{T}.EnableCaching"/> set on false</returns>
        /// <exception cref="InvalidOperationException">Occurs if the instance of <see cref="Validator{T}"/> is null</exception>
        public static Validator<T> DisableCaching<T>(this Validator<T> validator)
        {
            if (validator == null)
            {
                throw new InvalidOperationException(message: "Validator is null");
            }

            validator.EnableCaching = false;

            return validator;
        }

        /// <summary>
        /// Only Errors are returning if validating is called />
        /// </summary>
        /// <typeparam name="T">The Type to validate</typeparam>
        /// <param name="validator">The validator</param>
        /// <returns>Returns the Instance of <see cref="Validator{T}"/> with <see cref="Validator{T}.ReturnOnlyErrors"/> set on true</returns>
        /// <exception cref="InvalidOperationException">Occurs if the instance of <see cref="Validator{T}"/> is null</exception>
        public static Validator<T> EnableReturnOnlyErrors<T>(this Validator<T> validator)
        {
            if (validator == null)
            {
                throw new InvalidOperationException(message: "Validator is null");
            }

            validator.ReturnOnlyErrors = true;

            return validator;
        }

        /// <summary>
        /// All items are returning if validating is called />
        /// </summary>
        /// <typeparam name="T">The Type to validate</typeparam>
        /// <param name="validator">The validator</param>
        /// <returns>Returns the Instance of <see cref="Validator{T}"/> with <see cref="Validator{T}.ReturnOnlyErrors"/> set on false</returns>
        /// <exception cref="InvalidOperationException">Occurs if the instance of <see cref="Validator{T}"/> is null</exception>
        public static Validator<T> DisableReturnOnlyErrors<T>(this Validator<T> validator)
        {
            if (validator == null)
            {
                throw new InvalidOperationException(message: "Validator is null");
            }

            validator.ReturnOnlyErrors = false;

            return validator;
        }

        /// <summary>
        /// Adds a new <see cref="AbstractValidation{T}"/> to the <see cref="Validator{T}" />
        /// </summary>
        /// <typeparam name="T">The Type to validate</typeparam>
        /// <param name="validator">The validator</param>
        /// <param name="validation">The validation to add</param>
        /// <returns>Returns the Instance of <see cref="Validator{T}"/></returns>
        /// <exception cref="InvalidOperationException">Occurs if the instance of <see cref="Validator{T}"/> is null</exception>
        public static Validator<T> Add<T>(this Validator<T> validator, AbstractValidation<T> validation)
        {
            if (validator == null)
            {
                throw new InvalidOperationException(message: "Validator is null");
            }

            validator.AddValidation(validation: validation);

            return validator;
        }

        /// <summary>
        /// Adds a <see cref="IEnumerable{AbstractValidation}"/> to the <see cref="Validator{T}" />
        /// </summary>
        /// <typeparam name="T">The Type to validate</typeparam>
        /// <param name="validator">The validator</param>
        /// <param name="validations">The validations to add</param>
        /// <returns>Returns the Instance of <see cref="Validator{T}"/></returns>
        /// <exception cref="InvalidOperationException">Occurs if the instance of <see cref="Validator{T}"/> is null</exception>
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
