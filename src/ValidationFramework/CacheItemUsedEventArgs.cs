using System;

namespace ValidationFramework
{
    /// <summary>
    /// The EventArgs if a Cache-Item is used
    /// </summary>
    /// <typeparam name="T">The type that you want to validate</typeparam>
    public class CacheItemUsedEventArgs<T> : EventArgs
    {
        /// <summary>
        /// Contructor with the cacheKey, originalValue, valid and the validation
        /// </summary>
        /// <param name="cacheKey">The key of the Cache-Entry</param>
        /// <param name="originalValue">The original value</param>
        /// <param name="valid">The result</param>
        /// <param name="validation">The validation</param>
        public CacheItemUsedEventArgs(
            string cacheKey,
            object originalValue,
            bool valid,
            AbstractValidation<T> validation)
        {
            CacheKey = cacheKey;
            OriginalValue = originalValue;
            Valid = valid;
            Validation = validation;
        }

        /// <summary>
        /// Gets the key for caching
        /// </summary>
        public string CacheKey { get; protected set; }

        /// <summary>
        /// Gets the orignal value
        /// </summary>
        public object OriginalValue { get; protected set; }

        /// <summary>
        /// Gets the result
        /// </summary>
        public bool Valid { get; protected set; }

        /// <summary>
        /// Gets the validation
        /// </summary>
        public AbstractValidation<T> Validation { get; protected set; }
    }
}
