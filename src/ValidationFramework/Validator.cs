using System;
using System.Collections.Generic;
using System.Linq;

namespace ValidationFramework
{
    public class Validator<T> : IValidator<T>
    {
        private List<AbstractValidation<T>> _validations = new List<AbstractValidation<T>>();

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

        protected Dictionary<string, bool> Cache { get; set; } = new Dictionary<string, bool>();

        /// <inheritdoc />
        public IEnumerable<ValidationResponse> Validate(T value)
        {
            return Validate(
                value: value,
                wherePredicate: null);
        }

        /// <inheritdoc />
        public IEnumerable<ValidationResponse> Validate(T value, string nameFilter = null)
        {
            Func<AbstractValidation<T>, bool> wherePredicate = null;

            if (!string.IsNullOrEmpty(nameFilter))
            {
                wherePredicate = (i) => i.Name == nameFilter;
            }

            return Validate(
                value: value,
                wherePredicate: wherePredicate);
        }

        /// <inheritdoc />
        public IEnumerable<ValidationResponse> Validate(IEnumerable<T> values)
        {
            foreach (T value in values)
            {
                IEnumerable<ValidationResponse> responses = Validate(value: value);

                foreach (ValidationResponse response in responses)
                {
                    yield return response;
                }
            }

            yield break;
        }

        /// <inheritdoc />
        public IEnumerable<ValidationResponse> Validate(T value, Func<AbstractValidation<T>, bool> wherePredicate = null)
        {
            wherePredicate = wherePredicate ?? new Func<AbstractValidation<T>, bool>((i) => true);

            foreach (AbstractValidation<T> validation in Validations.Where(predicate: wherePredicate))
            {
                if (!EnableCaching || validation.OriginalValue == null)
                {
                    bool valid = validation.IsValid(value: value);

                    yield return CreateValidationResponse(
                        valid: valid,
                        validation: validation,
                        value: value);
                }
                else
                {
                    object originalValue = validation.OriginalValue(arg: value);

                    if (originalValue == null)
                    {
                        bool valid = validation.IsValid(value: value);

                        yield return CreateValidationResponse(
                            valid: valid,
                            validation: validation,
                            value: value);
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

                            yield return CreateValidationResponse(
                                valid: valid,
                                validation: validation,
                                value: value);
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

                            yield return CreateValidationResponse(
                                valid: valid,
                                validation: validation,
                                value: value);
                        }
                    }
                }
            }

            yield break;
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

        /// <inheritdoc />
        public void AddValidation(AbstractValidation<T> validation)
        {
            if (!Validations.Any(a => a.Name == validation.Name))
            {
                _validations.Add(item: validation);
            }
            else
            {
                UpdateValidation(validation: validation);
            }
        }

        /// <inheritdoc />
        public void UpdateValidation(AbstractValidation<T> validation)
        {
            if (Validations.Any(predicate: v => v.Name == validation.Name))
            {
                AbstractValidation<T> foundValidation = _validations.First(predicate: f => f.Name == validation.Name);
                foundValidation = validation;
            }
            else
            {
                throw new ArgumentException(
                    message: $"Validation {validation.Name} not found",
                    paramName: nameof(validation));
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
    }
}
