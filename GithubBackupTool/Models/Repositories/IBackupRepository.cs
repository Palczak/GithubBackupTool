namespace GithubBackupTool.Models.Repositories
{
    public interface IBackupRepository
    {
        public void SaveBackupToFile(Repository repository, byte[] value);
        public void CreateBackupRecord(string repositoryName);
        public byte[] ReadBackupFromFile();
    }
}
