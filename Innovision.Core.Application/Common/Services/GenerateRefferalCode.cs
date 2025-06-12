using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace Innovision.Core.Application.Common.Services;

public static class GenerateRefferalCode
{
    public static string GenerateCode(int string_length)
    {
        using (var rng = new RNGCryptoServiceProvider())
        {
            var bit_count = string_length * 6;
            var byte_count = (bit_count + 7) / 8; // rounded up
            var bytes = new byte[byte_count];
            rng.GetBytes(bytes);
            return Regex.Replace(Convert.ToBase64String(bytes).ToUpper(), "[^a-zA-Z0-9]+", "1", RegexOptions.Compiled).ToUpper();
        }
    }
}