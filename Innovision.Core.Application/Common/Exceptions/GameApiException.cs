namespace Innovision.Core.Application.Exceptions;

public class GameApiException : Exception
{
    public GameApiException(string type, string message)
        : base($"Error Type '{type}' with errro Message {message}") { }
}