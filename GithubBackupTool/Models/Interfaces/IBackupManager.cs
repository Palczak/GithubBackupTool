
namespace GithubBackupTool.Models.Interfaces
{
    public interface IBackupManager
    {
        public void CreateBackup(Repository repository);

        public void RestoreBackup(Repository repository);
    }
}
