namespace Innovision.Core.Application.Exceptions;

public class FileServiceException : Exception
{
    public FileServiceException(string process, string message)
        : base($"Error in File '{process}'. {message}") { }
}