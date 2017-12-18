using System;
using System.Collections.Generic;
using System.Linq;

namespace ValidationFramework
{
    public class Validator<T> : IValidator<T>
    {
        private readonly List<AbstractValidation<T>> _validations = new List<AbstractValidation<T>>();

        /// <summary>
        /// Default constructor
        /// </summary>
        public Validator()
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
        public IEnumerable<ValidationResponse> ValidateSingleValue(T value)
        {
            return ValidateWithFilter(
                value: value,
                wherePredicate: null);
        }

        /// <inheritdoc />
        public IEnumerable<ValidationResponse> ValidateWithNameFilter(T value, string nameFilter = null)
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
        public IEnumerable<ValidationResponse> ValidateList(IEnumerable<T> values)
        {
            foreach (T value in values)
            {
                IEnumerable<ValidationResponse> responses = ValidateSingleValue(value: value);

                foreach (ValidationResponse response in responses)
                {
                    yield return response;
                }
            }
        }

        /// <inheritdoc />
        public IEnumerable<ValidationResponse> ValidateWithFilter(T value, Func<AbstractValidation<T>, bool> wherePredicate = null)
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

        private IEnumerable<ValidationResponse> ValidateWithCachingDisabled(T value, Func<AbstractValidation<T>, bool> wherePredicate)
        {
            foreach (AbstractValidation<T> validation in Validations.Where(predicate: wherePredicate))
            {
                bool valid = validation.IsValid(value: value);

                if (!valid && ReturnOnlyErrors)
                {
                    yield return CreateValidationResponse(
                        valid: valid,
                        validation: validation,
                        value: value);
                }
                else if (!ReturnOnlyErrors)
                {
                    yield return CreateValidationResponse(
                        valid: valid,
                        validation: validation,
                        value: value);
                }
            }
        }

        private IEnumerable<ValidationResponse> ValidateWithCachingEnabled(T value, Func<AbstractValidation<T>, bool> wherePredicate)
        {
            Func<bool, bool, bool> doYieldReturn = (valid, returonOnlyErrors) =>
            {
                return (!valid && returonOnlyErrors) || !returonOnlyErrors;
            };


            foreach (AbstractValidation<T> validation in Validations.Where(predicate: wherePredicate))
            {
                object originalValue = validation.OriginalValue(arg: value);

                if (originalValue == null)
                {
                    bool valid = validation.IsValid(value: value);

                    if (doYieldReturn(valid, ReturnOnlyErrors))
                    {
                        yield return CreateValidationResponse(
                                            valid: valid,
                                            validation: validation,
                                            value: value);
                    }
                }
                else
                {
                    int hashCode = originalValue.GetHashCode();

                    string cacheKey = $"[{validation.Name}][{hashCode}]";

                    if (Cache.ContainsKey(key: cacheKey))
                    {
                        bool valid = Cache[key: cacheKey];

                        OnCacheItemUsed(eventArgs: new CacheItemUsedEventArgs<T>(
                                                cacheKey: cacheKey,
                                                originalValue: originalValue,
                                                valid: valid,
                                                validation: validation));

                        if (doYieldReturn(valid, ReturnOnlyErrors))
                        {
                            yield return CreateValidationResponse(
                                valid: valid,
                                validation: validation,
                                value: value);
                        }
                    }
                    else
                    {
                        bool valid = validation.IsValid(value: value);
                        Cache.Add(key: cacheKey, value: valid);

                        OnCacheItemAdded(eventArgs: new CacheItemAddedEventArgs<T>(
                                                cacheKey: cacheKey,
                                                originalValue: originalValue,
                                                valid: valid,
                                                validation: validation));

                        if (doYieldReturn(valid, ReturnOnlyErrors))
                        {
                            yield return CreateValidationResponse(
                                valid: valid,
                                validation: validation,
                                value: value);
                        }
                    }
                }
            }
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

        /// <inheritdoc />
        public void ClearValidations()
        {
            _validations.Clear();
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

        private ValidationResponse CreateValidationResponse(bool valid, AbstractValidation<T> validation, T value)
        {
            return new ValidationResponse(
                type: valid ? ValidationResponseType.Success : ValidationResponseType.Error,
                name: validation.Name,
                message: valid ? validation.MessageOnSuccess : validation.MessageOnError,
                originalValue: validation.OriginalValue(arg: value));
        }
    }
}
