using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Ecommercer.Aplication.Services.Cripto
{
    public class CriptoSenha
    {
        private const int SaltSize = 16; 
        private const int KeySize = 32; 
        private const int Iterations = 10000; 

        public static string Encrypt(string text, string password)
        {
            var salt = GenerateSalt();
            var key = DeriveKey(password, salt);

            using (var aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.GenerateIV();
                var iv = aesAlg.IV;

                using (var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, iv))
                using (var msEncrypt = new MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    using (var swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(text);
                    }

                    var encryptedContent = msEncrypt.ToArray();
                    var hmac = ComputeHmac(encryptedContent, key);
                    var result = new byte[salt.Length + iv.Length + hmac.Length + encryptedContent.Length];

                    Buffer.BlockCopy(salt, 0, result, 0, salt.Length);
                    Buffer.BlockCopy(iv, 0, result, salt.Length, iv.Length);
                    Buffer.BlockCopy(hmac, 0, result, salt.Length + iv.Length, hmac.Length);
                    Buffer.BlockCopy(encryptedContent, 0, result, salt.Length + iv.Length + hmac.Length, encryptedContent.Length);

                    return Convert.ToBase64String(result);
                }
            }
        }

        public static string Decrypt(string encryptedText, string password)
        {
            var fullCipher = Convert.FromBase64String(encryptedText);

            var salt = new byte[SaltSize];
            var iv = new byte[16];
            var hmac = new byte[32];
            var cipher = new byte[fullCipher.Length - SaltSize - iv.Length - hmac.Length];

            Buffer.BlockCopy(fullCipher, 0, salt, 0, salt.Length);
            Buffer.BlockCopy(fullCipher, salt.Length, iv, 0, iv.Length);
            Buffer.BlockCopy(fullCipher, salt.Length + iv.Length, hmac, 0, hmac.Length);
            Buffer.BlockCopy(fullCipher, salt.Length + iv.Length + hmac.Length, cipher, 0, cipher.Length);

            var key = DeriveKey(password, salt);
            var computedHmac = ComputeHmac(cipher, key);

            if (!CompareHashes(hmac, computedHmac))
                throw new CryptographicException("HMAC validation failed");

            using (var aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;

                using (var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV))
                using (var msDecrypt = new MemoryStream(cipher))
                using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                using (var srDecrypt = new StreamReader(csDecrypt))
                {
                    return srDecrypt.ReadToEnd();
                }
            }
        }

        private static byte[] GenerateSalt()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                var salt = new byte[SaltSize];
                rng.GetBytes(salt);
                return salt;
            }
        }

        private static byte[] DeriveKey(string password, byte[] salt)
        {
            using (var rfc2898 = new Rfc2898DeriveBytes(password, salt, Iterations))
            {
                return rfc2898.GetBytes(KeySize);
            }
        }

        private static byte[] ComputeHmac(byte[] data, byte[] key)
        {
            using (var hmac = new HMACSHA256(key))
            {
                return hmac.ComputeHash(data);
            }
        }

        private static bool CompareHashes(byte[] hash1, byte[] hash2)
        {
            if (hash1.Length != hash2.Length)
                return false;

            for (int i = 0; i < hash1.Length; i++)
            {
                if (hash1[i] != hash2[i])
                    return false;
            }
            return true;
        }
    }
}
