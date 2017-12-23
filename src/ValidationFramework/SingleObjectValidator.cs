using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ValidationFramework
{
    /// <summary>
    /// Validator-Klasse für die Überprüfung eines Objektes für den Typ <see cref="T"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SingleObjectValidator<T>
    {
        private readonly List<AbstractValidation<T>> _validations = new List<AbstractValidation<T>>();

        /// <summary>
        /// Default constructor
        /// </summary>
        public SingleObjectValidator()
        {
        }

        /// <inheritdoc />
        public event EventHandler<CacheItemUsedEventArgs<T>> CacheItemUsed;

        /// <inheritdoc />
        public event EventHandler<CacheItemAddedEventArgs<T>> CacheItemAdded;

        /// <inheritdoc />
        public IReadOnlyCollection<AbstractValidation<T>> Validations => _validations.AsReadOnly();

        /// <inheritdoc />
        public bool EnableCaching { get; set; } = true;

        /// <inheritdoc />
        public bool ReturnOnlyErrors { get; set; } = false;

        /// <summary>
        /// The internal cache
        /// </summary>
        protected Dictionary<string, bool> Cache { get; set; } = new Dictionary<string, bool>();

        /// <inheritdoc />
        public ReadOnlyCollection<ValidationResponse> ValidateSingleValue(T value)
        {
            return ValidateWithFilter(
                value: value,
                wherePredicate: null);
        }

        /// <inheritdoc />
        public ReadOnlyCollection<ValidationResponse> ValidateWithNameFilter(
            T value,
            string nameFilter = null)
        {
            Func<AbstractValidation<T>, bool> wherePredicate = null;

            if (!string.IsNullOrEmpty(nameFilter))
            {
                wherePredicate = (i) => i.Name == nameFilter;
            }

            return ValidateWithFilter(
                value: value,
                wherePredicate: wherePredicate);
        }

        /// <inheritdoc />
        public ReadOnlyCollection<ValidationResponse> ValidateWithFilter(
            T value,
            Func<AbstractValidation<T>, bool> wherePredicate = null)
        {
            wherePredicate = wherePredicate ?? new Func<AbstractValidation<T>, bool>((i) => true);

            if (EnableCaching)
            {
                return ValidateWithCachingEnabled(value: value, wherePredicate: wherePredicate);
            }
            else
            {
                return ValidateWithCachingDisabled(value: value, wherePredicate: wherePredicate);
            }
        }

        private ReadOnlyCollection<ValidationResponse> ValidateWithCachingDisabled(
            T value,
            Func<AbstractValidation<T>, bool> wherePredicate)
        {
            List<ValidationResponse> responses = new List<ValidationResponse>();

            foreach (AbstractValidation<T> validation in Validations.Where(predicate: wherePredicate))
            {
                bool valid = validation.IsValid(value: value);

                AddResponse(
                    valid: valid,
                    returnOnlyErrors: ReturnOnlyErrors,
                    validation: validation,
                    value: value,
                    responses: responses);
            }

            return responses.AsReadOnly();
        }

        private void AddResponse(
            bool valid,
            bool returnOnlyErrors,
            AbstractValidation<T> validation,
            T value,
            List<ValidationResponse> responses)
        {
            if ((!valid && returnOnlyErrors) || !returnOnlyErrors)
            {
                responses.Add(item: CreateValidationResponse(
                                            valid: valid,
                                            validation: validation,
                                            value: value));
            }
        }

        private ReadOnlyCollection<ValidationResponse> ValidateWithCachingEnabled(
            T value,
            Func<AbstractValidation<T>, bool> wherePredicate)
        {
            List<ValidationResponse> responses = new List<ValidationResponse>();

            foreach (AbstractValidation<T> validation in Validations.Where(predicate: wherePredicate))
            {
                object originalValue = validation.OriginalValue(arg: value);

                bool valid = false;

                if (originalValue == null)
                {
                    valid = validation.IsValid(value: value);
                }
                else
                {
                    int hashCode = originalValue.GetHashCode();

                    string cacheKey = $"[{validation.Name}][{hashCode}]";

                    if (Cache.ContainsKey(key: cacheKey))
                    {
                        valid = Cache[key: cacheKey];

                        OnCacheItemUsed(eventArgs: new CacheItemUsedEventArgs<T>(
                                                cacheKey: cacheKey,
                                                originalValue: originalValue,
                                                valid: valid,
                                                validation: validation));
                    }
                    else
                    {
                        valid = validation.IsValid(value: value);
                        Cache.Add(key: cacheKey, value: valid);

                        OnCacheItemAdded(eventArgs: new CacheItemAddedEventArgs<T>(
                                                cacheKey: cacheKey,
                                                originalValue: originalValue,
                                                valid: valid,
                                                validation: validation));
                    }
                }

                AddResponse(
                    valid: valid,
                    returnOnlyErrors: ReturnOnlyErrors,
                    validation: validation,
                    value: value,
                    responses: responses);
            }

            return responses.AsReadOnly();
        }

        /// <inheritdoc />
        public void AddValidation(AbstractValidation<T> validation)
        {
            if (!Validations.Any(a => a.Name == validation.Name))
            {
                _validations.Add(item: validation);
            }
        }

        /// <inheritdoc />
        public void AddValidation(IEnumerable<AbstractValidation<T>> validations)
        {
            foreach (AbstractValidation<T> validation in validations)
            {
                AddValidation(validation: validation);
            }
        }

        private void OnCacheItemAdded(CacheItemAddedEventArgs<T> eventArgs)
        {
            CacheItemAdded?.Invoke(
                sender: this,
                e: new CacheItemAddedEventArgs<T>(
                    cacheKey: eventArgs.CacheKey,
                    originalValue: eventArgs.OriginalValue,
                    valid: eventArgs.Valid,
                    validation: eventArgs.Validation));
        }

        private void OnCacheItemUsed(CacheItemUsedEventArgs<T> eventArgs)
        {
            CacheItemUsed?.Invoke(
                sender: this,
                e: new CacheItemUsedEventArgs<T>(
                    cacheKey: eventArgs.CacheKey,
                    originalValue: eventArgs.OriginalValue,
                    valid: eventArgs.Valid,
                    validation: eventArgs.Validation));
        }

        private ValidationResponse CreateValidationResponse(
            bool valid,
            AbstractValidation<T> validation,
            T value)
        {
            return new ValidationResponse(
                type: valid ? ValidationResponseType.Success : ValidationResponseType.Error,
                name: validation.Name,
                message: valid ? validation.MessageOnSuccess : validation.MessageOnError,
                originalValue: validation.OriginalValue(arg: value));
        }
    }
}
