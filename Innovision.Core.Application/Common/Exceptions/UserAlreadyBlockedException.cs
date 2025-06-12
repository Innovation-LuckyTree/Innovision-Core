namespace Innovision.Core.Application.Exceptions;

public class UserAlreadyBlockedException : Exception
{
  public UserAlreadyBlockedException(string name)
      : base($"User '{name}' is already blocked.") { }
}