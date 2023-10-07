using GithubBackupTool.Infractructure.Interfaces;

namespace GithubBackupTool.Infractructure
{
    public class BackupRepository : IBackupRepository
    {
        public void CreateBackupRecord(string repositoryName)
        {
            throw new System.NotImplementedException();
        }

        public object ReadBackupFromFile()
        {
            throw new System.NotImplementedException();
        }

        public void SaveBackupToFile(object value)
        {
            throw new System.NotImplementedException();
        }
    }
}
