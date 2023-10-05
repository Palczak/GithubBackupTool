namespace GithubBackupTool.Infrastructure.Interfaces
{
    public interface IEncryptor
    {
        public object Encrypt(object value);

        public object Decrypt(object value);
    }
}
