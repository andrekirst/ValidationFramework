namespace ValidationFramework
{
    /// <summary>
    /// The response of a validation
    /// </summary>
    public class ValidationResponse
    {
        /// <summary>
        /// Constructor to init message, name, originalValue and type
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="name">The name of the validation</param>
        /// <param name="originalValue">The original value that was validated</param>
        /// <param name="type">The response type</param>
        public ValidationResponse(string message, string name, object originalValue, ValidationResponseType type)
        {
            Message = message;
            Name = name;
            OriginalValue = originalValue;
            Type = type;
        }

        /// <summary>
        /// The message
        /// </summary>
        public string Message { get; protected set; }

        /// <summary>
        /// The name of the validation
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// The original value that was validated
        /// </summary>
        public object OriginalValue { get; protected set; }

        /// <summary>
        /// The response type
        /// </summary>
        public ValidationResponseType Type { get; protected set; }
    }
}
