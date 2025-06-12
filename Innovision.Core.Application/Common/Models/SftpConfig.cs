using Innovision.Core.Application.Common.Interfaces;

namespace Innovision.Core.Application.Common.Models
{
    public class SftpConfig : ISftpConfig
    {
        public string Host { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Folder { get; set; }
    }
}
