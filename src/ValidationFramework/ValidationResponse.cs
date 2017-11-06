namespace ValidationFramework
{
    public class ValidationResponse
    {
        public ValidationResponse()
        {
        }

        public ValidationResponse(string message, string name, object originalValue, ValidationResponseType type)
        {
            Message = message;
            Name = name;
            OriginalValue = originalValue;
            Type = type;
        }

        public string Message { get; protected set; }

        public string Name { get; protected set; }

        public object OriginalValue { get; protected set; }

        public ValidationResponseType Type { get; protected set; }
    }
}
