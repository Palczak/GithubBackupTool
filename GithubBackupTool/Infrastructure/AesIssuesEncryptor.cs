using GithubBackupTool.Infrastructure.Interfaces;
using GithubBackupTool.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;

namespace GithubBackupTool.Infrastructure
{
    public class AesIssuesEncryptor : IIssuesEncryptor
    {
        private readonly IEncryptionKeyProvider _encryptionKeyProvider;

        public AesIssuesEncryptor(IEncryptionKeyProvider encryptionKeyProvider)
        {
            _encryptionKeyProvider = encryptionKeyProvider;
        }

        public IEnumerable<Issue> Decrypt(byte[] encryptedValue)
        {
            string plaintext;
            using (Aes aes = Aes.Create())
            {
                aes.IV = _encryptionKeyProvider.IV;
                aes.Key = _encryptionKeyProvider.Key;

                // Create a decryptor to perform the stream transform.
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                // Create the streams used for decryption.
                using MemoryStream msDecrypt = new MemoryStream(encryptedValue);
                using CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
                using StreamReader srDecrypt = new StreamReader(csDecrypt);

                plaintext = srDecrypt.ReadToEnd();
            }
            return JsonConvert.DeserializeObject<IEnumerable<Issue>>(plaintext)!;
        }

        public byte[] Encrypt(IEnumerable<Issue> value)
        {
            var serializedIssues = JsonConvert.SerializeObject(value);
            byte[] encryptedValue;

            using (Aes aes = Aes.Create())
            {
                aes.IV = _encryptionKeyProvider.IV;
                aes.Key = _encryptionKeyProvider.Key;
                var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                using MemoryStream msEncrypt = new MemoryStream();
                using CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);
                using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                {
                    swEncrypt.Write(serializedIssues);
                }
                encryptedValue = msEncrypt.ToArray();
            }
            return encryptedValue;
        }
    }
}
