using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text;
using Ykotika.Application.Interfaces;

namespace Ykotika.Security
{
    public class Encryptor
        (IOptions<EncryptionOptions> options)
        : IEncryptor
    {
        private readonly EncryptionOptions _options = options.Value;

        public string Encrypt(string value)
        {
            using var aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(_options.Key.PadRight(32)[..32]);
            aes.IV = Encoding.UTF8.GetBytes(_options.AesIv.PadRight(16)[..16]);

            using var encryptor = aes.CreateEncryptor();
            var tokenBytes = Encoding.UTF8.GetBytes(value);
            var encryptedBytes = encryptor.TransformFinalBlock(tokenBytes, 0, tokenBytes.Length);

            return Convert.ToBase64String(encryptedBytes);
        }
        public string Decrypt(string value)
        {
            using var aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(_options.Key.PadRight(32)[..32]);
            aes.IV = Encoding.UTF8.GetBytes(_options.AesIv.PadRight(16)[..16]);

            using var decryptor = aes.CreateDecryptor();
            var encryptedBytes = Convert.FromBase64String(value);
            var decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);

            return Encoding.UTF8.GetString(decryptedBytes);
        }
    }

    public class EncryptionOptions
    {
        public required string Key { get; set; }
        public required string AesIv { get; set; }
    }
}
