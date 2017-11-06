using System;

namespace ValidationFramework
{
    /// <summary>
    /// Abstract class for a validation
    /// </summary>
    /// <typeparam name="T">The type that you want to validate</typeparam>
    public abstract class AbstractValidation<T>
    {
        /// <summary>
        /// Gets the name of the validation
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// Gets the validation-function
        /// </summary>
        public abstract Func<T, bool> ValidationFunction { get; }

        /// <summary>
        /// Gets the message if the validation is not successful
        /// </summary>
        public abstract string MessageOnError { get; }

        /// <summary>
        /// Gets the orignal value that qou want to validate. Important for caching
        /// </summary>
        public abstract Func<T, object> OriginalValue { get; }

        /// <summary>
        /// Gets the message if the validation is successful
        /// </summary>
        public abstract string MessageOnSuccess { get; }

        /// <summary>
        /// The result of the execution of <see cref="ValidationFunction"/>
        /// </summary>
        /// <param name="value">the input value</param>
        /// <returns>Returns the output of the execution of <see cref="ValidationFunction"/></returns>
        public bool IsValid(T value) => ValidationFunction(arg: value);

        /// <summary>
        /// Returns the value of <see cref="Name"/>
        /// </summary>
        /// <returns><see cref="Name"/></returns>
        public override string ToString()
        {
            return $"Name: {Name}";
        }
    }
}
