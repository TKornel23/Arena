namespace Arena.Domain;

[Serializable]
public class ArenaFailedException : Exception
{
    public ArenaFailedException()
    {
    }

    public ArenaFailedException(string? message) : base(message)
    {
    }

    public ArenaFailedException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected ArenaFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}