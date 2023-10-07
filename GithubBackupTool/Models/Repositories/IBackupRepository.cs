namespace GithubBackupTool.Infractructure.Interfaces
{
    public interface IBackupRepository
    {
        public void SaveBackupToFile(object value);
        public void CreateBackupRecord(string repositoryName);
        public object ReadBackupFromFile();
    }
}
