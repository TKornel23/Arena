namespace Arena.Domain;

[Serializable]
public class InitializingFailedException : Exception
{
    public InitializingFailedException()
    {
    }

    public InitializingFailedException(string? message) : base(message)
    {
    }

    public InitializingFailedException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected InitializingFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}