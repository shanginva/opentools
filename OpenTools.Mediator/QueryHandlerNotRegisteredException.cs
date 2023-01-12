using System.Runtime.Serialization;

namespace OpenTools.Mediator;

public class QueryHandlerNotRegisteredException
    : Exception
{
    public QueryHandlerNotRegisteredException()
    {
    }

    public QueryHandlerNotRegisteredException(string? message) : base(message)
    {
    }

    public QueryHandlerNotRegisteredException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected QueryHandlerNotRegisteredException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
