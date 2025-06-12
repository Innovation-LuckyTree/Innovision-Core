using System.Security.Cryptography;

namespace HappyPlay.Upload.Application.Common;

public class Crypto
{
    const string _secretKey = "wUekR1jeGMMbjpsgNFOt7Q==";
    const string _secretIV = "ADoiVYnRWF+G/Tq4Hm3GQA==";

    public string Encrypt(string plainText)
    {
        byte[] cipherBytes = EncryptByte(plainText,
            Convert.FromBase64String(_secretKey),
            Convert.FromBase64String(_secretIV));

        return Convert.ToBase64String(cipherBytes);
    }

    public string Decrypt(string encryptedText)
    {
        byte[] cipherBytes = Convert.FromBase64String(encryptedText);
        string decryptedString = DecryptByte(cipherBytes,
            Convert.FromBase64String(_secretKey),
            Convert.FromBase64String(_secretIV));

        return decryptedString;
    }

    private byte[] EncryptByte(string simpletext, byte[] key, byte[] iv)
    {
        byte[] cipheredVal;
        using (Aes aes = Aes.Create())
        {
            ICryptoTransform encryptor = aes.CreateEncryptor(key, iv);
            using MemoryStream memoryStream = new();
            using CryptoStream cryptoStream = new(memoryStream, encryptor, CryptoStreamMode.Write);
            using (StreamWriter streamWriter = new(cryptoStream))
            {
                streamWriter.Write(simpletext);
            }

            cipheredVal = memoryStream.ToArray();
        }
        return cipheredVal;
    }

    private string DecryptByte(byte[] cipheredtext, byte[] key, byte[] iv)
    {
        string respValue = string.Empty;
        using (Aes aes = Aes.Create())
        {
            ICryptoTransform decryptor = aes.CreateDecryptor(key, iv);
            using MemoryStream memoryStream = new(cipheredtext);
            using CryptoStream cryptoStream = new(memoryStream, decryptor, CryptoStreamMode.Read);
            using StreamReader streamReader = new(cryptoStream);
            respValue = streamReader.ReadToEnd();
        }
        return respValue;
    }
}
