using System.Net;

namespace Melnikov.Blazor.Clean.Application.Common.Exceptions;

public class NotFoundException : ServerException
{
    public NotFoundException(string message)
        : base(message, HttpStatusCode.NotFound)
    {
    }

    public NotFoundException(string name, object key)
        : base($"Entity \"{name}\" ({key}) was not found.", HttpStatusCode.NotFound)
    {
    }
}