namespace Innovision.Core.Application.Common.Interfaces
{
    public interface ISftpConfig
    {
        string Host { get; set; }
        string Username { get; set; }
        string Password { get; set; }
        string Folder { get; set; }
    }
}
