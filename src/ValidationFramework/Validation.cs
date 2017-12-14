using System;

namespace ValidationFramework
{
    /// <summary>
    /// A generic implementation for <see cref="AbstractValidation{T}"/>
    /// </summary>
    /// <typeparam name="T">The type to validate</typeparam>
    public class Validation<T> : AbstractValidation<T>
    {
        private readonly string _messageOnError;
        private readonly string _messageOnSuccess;
        private readonly string _name;
        private readonly Func<T, object> _originalValue;
        private readonly Func<T, bool> _validationFunction;

        public Validation(string messageOnError, string messageOnSuccess, string name, Func<T, object> originalValue, Func<T, bool> validationFunction)
        {
            _messageOnError = messageOnError;
            _messageOnSuccess = messageOnSuccess;
            _name = name;
            _originalValue = originalValue;
            _validationFunction = validationFunction;
        }

        /// <inheritdoc />
        public override string MessageOnError => _messageOnError;

        /// <inheritdoc />
        public override string MessageOnSuccess => _messageOnSuccess;

        /// <inheritdoc />
        public override string Name => _name;

        /// <inheritdoc />
        public override Func<T, object> OriginalValue => _originalValue;

        /// <inheritdoc />
        public override Func<T, bool> ValidationFunction => _validationFunction;
    }
}
