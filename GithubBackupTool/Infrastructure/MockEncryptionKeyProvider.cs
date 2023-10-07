using GithubBackupTool.Infrastructure.Interfaces;

namespace GithubBackupTool.Infrastructure
{
    public class MockEncryptionKeyProvider : IEncryptionKeyProvider
    {
        public string EncrytpionKey => "123456789";
    }
}
