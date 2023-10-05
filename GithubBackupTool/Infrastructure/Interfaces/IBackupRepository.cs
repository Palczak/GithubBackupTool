namespace GithubBackupTool.Infractructure.Interfaces
{
    public interface IBackupRepository
    {
        public void WriteBackup(object value);
        public void CreaceBackupRecord(string repositoryName);
        public object ReadBackup();
    }
}
