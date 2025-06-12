namespace Innovision.Core.Application.Exceptions;

public class CoreIdentityApiException : Exception
{
    public CoreIdentityApiException(string type, string message)
        : base($"Error Type '{type}' with errro Message {message}") { }
}