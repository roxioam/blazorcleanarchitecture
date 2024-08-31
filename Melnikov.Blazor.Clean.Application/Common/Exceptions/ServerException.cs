using System.Net;

namespace Melnikov.Blazor.Clean.Application.Common.Exceptions;

public class ServerException(string message, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
    : Exception(message)
{
    public IEnumerable<string> ErrorMessages { get; } = new[] { message };

    public HttpStatusCode StatusCode { get; } = statusCode;
}