using GithubBackupTool.Models;
using System.Collections.Generic;

namespace GithubBackupTool.Infrastructure.Interfaces
{
    public interface IIssuesEncryptor
    {
        public byte[] Encrypt(IEnumerable<Issue> value);

        public IEnumerable<Issue> Decrypt(byte[] encryptedValue);
    }
}
