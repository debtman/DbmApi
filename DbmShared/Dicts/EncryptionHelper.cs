using System.Security.Cryptography;
using System.Text;

namespace DbmApi.Common
{
    public static class EncryptionHelper
    {
        public static string EncryptData(string plaintext, string key)
        {
            using Aes aes = Aes.Create();
            aes.Key = SHA256.HashData(Encoding.UTF8.GetBytes(key)); // Derive encryption key from token
            aes.GenerateIV(); // Create a unique IV for each encryption

            using var encryptor = aes.CreateEncryptor();
            byte[] plaintextBytes = Encoding.UTF8.GetBytes(plaintext);
            byte[] encryptedBytes = encryptor.TransformFinalBlock(plaintextBytes, 0, plaintextBytes.Length);

            return Convert.ToBase64String(aes.IV) + ":" + Convert.ToBase64String(encryptedBytes); // Send IV along with encrypted data
        }

        public static string DecryptData(string encryptedInput, string key)
        {
            using Aes aes = Aes.Create();            
            aes.Key = SHA256.HashData(Encoding.UTF8.GetBytes(key));

            var parts = encryptedInput.Split(':');
            byte[] iv = Convert.FromBase64String(parts[0]);
            byte[] encryptedBytes = Convert.FromBase64String(parts[1]);

            aes.IV = iv;
            using var decryptor = aes.CreateDecryptor();
            byte[] decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);

            return Encoding.UTF8.GetString(decryptedBytes);
        }

        public static byte[] ConvertBearerToKey(string bearerToken)
        {
            return SHA256.HashData(Encoding.UTF8.GetBytes(bearerToken));
        }
    }
}
