namespace Innovision.Core.Application.Common.Exceptions;

public class AddException : Exception
{
    public AddException(string name, object errorMessage)
        : base($"Adding Entity '{name}' Error: ({errorMessage}).") { }
}
