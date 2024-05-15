namespace Arena.Domain;

[Serializable]
public class InvalidGuidException : Exception
{
    public InvalidGuidException()
    {
    }

    public InvalidGuidException(string? message) : base(message)
    {
    }

    public InvalidGuidException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected InvalidGuidException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}