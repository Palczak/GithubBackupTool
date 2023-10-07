namespace GithubBackupTool.Infrastructure.Interfaces
{
    public interface IEncryptionKeyProvider
    {
        public byte[] Key { get; }

        public byte[] IV { get; }
    }
}
