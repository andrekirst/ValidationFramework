using System;
using System.Collections.Generic;

namespace ValidationFramework
{
    /// <summary>
    /// Interface for the validator
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IValidator<T>
    {
        /// <summary>
        /// Gets or sets whether the caching is enabled
        /// </summary>
        bool EnableCaching { get; set; }

        /// <summary>
        /// Gets the validation as readonly
        /// </summary>
        IReadOnlyCollection<AbstractValidation<T>> Validations { get; }

        /// <summary>
        /// The vvent if the cache item is added
        /// </summary>
        event EventHandler<CacheItemAddedEventArgs<T>> CacheItemAdded;

        /// <summary>
        /// The event if the cache item is used
        /// </summary>
        event EventHandler<CacheItemUsedEventArgs<T>> CacheItemUsed;

        /// <summary>
        /// Adds a new validation to a list
        /// </summary>
        /// <param name="validation">The validation for <see cref="T"/></param>
        void AddValidation(AbstractValidation<T> validation);

        /// <summary>
        /// Adds a list of <see cref="AbstractValidation{T}"/>
        /// </summary>
        /// <param name="validations">The list</param>
        void AddValidation(IEnumerable<AbstractValidation<T>> validations);

        /// <summary>
        /// Clears the internal list
        /// </summary>
        void ClearValidations();

        /// <summary>
        /// Validates the validations and returns a list of <see cref="ValidationResponse"/>
        /// </summary>
        /// <param name="values">The values to validate</param>
        /// <returns>A list of <see cref="ValidationResponse"/></returns>
        IEnumerable<ValidationResponse> ValidateList(IEnumerable<T> values);

        /// <summary>
        /// Validates the validations and returns a list of <see cref="ValidationResponse"/>
        /// </summary>
        /// <param name="value">The value to validate</param>
        /// <returns>A list of <see cref="ValidationResponse"/></returns>
        IEnumerable<ValidationResponse> ValidateSingleValue(T value);

        /// <summary>
        /// Validates the validations and returns a list of <see cref="ValidationResponse"/>
        /// </summary>
        /// <param name="value">The value to validate</param>
        /// <param name="wherePredicate">A filterpredicate</param>
        /// <returns>A list of <see cref="ValidationResponse"/></returns>
        IEnumerable<ValidationResponse> ValidateWithFilter(
            T value, Func<AbstractValidation<T>,
                bool> wherePredicate = null);

        /// <summary>
        /// Validates the validations and returns a list of <see cref="ValidationResponse"/>
        /// </summary>
        /// <param name="value">The value to validate</param>
        /// <param name="nameFilter">A filter that does filter the validations by the <see cref="Validation{T}.Name"/> of the validation</param>
        /// <returns>A list of <see cref="ValidationResponse"/></returns>
        IEnumerable<ValidationResponse> ValidateWithNameFilter(
            T value,
            string nameFilter = null);

        /// <summary>
        /// If set to true, the validation method returns only errors. Else errors and successful items.
        /// </summary>
        bool ReturnOnlyErrors { get; set; }
    }
}