using System.Net.Sockets;
using System.Text;
using Innovision.Core.Application.Common.Interfaces;
using Innovision.Core.Application.Common.Models;
using Innovision.Core.Application.Exceptions;
using HappyPlay.Upload.Application.Common;
using Renci.SshNet;
using Renci.SshNet.Common;

namespace Innovision.Core.Application.Common.Services;

public class FileService : IFileService
{
    private readonly ISftpConfig _sftpConfig;
    private PasswordConnectionInfo _passwordConnectionInfo;

    public FileService(ISftpConfig sftpConfig)
    {
        _sftpConfig = sftpConfig;
        _passwordConnectionInfo = new PasswordConnectionInfo(_sftpConfig.Host, _sftpConfig.Username, _sftpConfig.Password);
    }

    public async Task<string> UploadImage(string base64Image)
    {
        Crypto crypto = new();

        string encrypt = crypto.Encrypt(base64Image);
        string fileName = $@"{Guid.NewGuid()}.enc";

        using SftpClient client = new(_passwordConnectionInfo);

        try
        {
            client.Connect();
            if (client.IsConnected)
            {
                byte[] byteArray = Encoding.UTF8.GetBytes(encrypt);

                using MemoryStream stream = new(byteArray);
                client.UploadFile(stream, $"{_sftpConfig.Folder}/" + fileName, Console.WriteLine);
            }
        }
        catch (Exception e) when (e is SshConnectionException || e is SocketException || e is ProxyException)
        {
            throw new FileServiceException("Upload", $"Error connecting to server: {e.Message}");
        }
        catch (SshAuthenticationException e)
        {
            throw new FileServiceException("Upload", $"Failed to authenticate: {e.Message}");
        }
        catch (SftpPermissionDeniedException e)
        {
            throw new FileServiceException("Upload", $"Operation denied by the server: {e.Message}");
        }
        catch (SshException e)
        {
            throw new FileServiceException("Upload", $"Sftp Error: {e.Message}");
        }
        finally
        {
            client.Disconnect();
        }

        return fileName;
    }

    public async Task<Base64FileResponse> GetBase64Image(string fileName)
    {
        string imageResult = string.Empty;
        Crypto crypto = new();

        using SftpClient client = new(_passwordConnectionInfo);

        try
        {
            client.Connect();
            if (client.IsConnected)
            {
                var serverFile = $"{_sftpConfig.Folder}/" + fileName;
                MemoryStream msStream = new();
                client.DownloadFile(serverFile, msStream);

                var data = Encoding.UTF8.GetString(msStream.ToArray());
                imageResult = crypto.Decrypt(data);
            }
        }
        catch (Exception e) when (e is SshConnectionException || e is SocketException || e is ProxyException)
        {
            throw new FileServiceException("Download", $"Error connecting to server: {e.Message}");
        }
        catch (SshAuthenticationException e)
        {
            throw new FileServiceException("Download", $"Failed to authenticate: {e.Message}");
        }
        catch (SftpPermissionDeniedException e)
        {
            throw new FileServiceException("Download", $"Operation denied by the server: {e.Message}");
        }
        catch (SshException e)
        {
            throw new FileServiceException("Download", $"Sftp Error: {e.Message}");
        }
        finally
        {
            client.Disconnect();
        }

        return new Base64FileResponse(fileName, imageResult);
    }
}