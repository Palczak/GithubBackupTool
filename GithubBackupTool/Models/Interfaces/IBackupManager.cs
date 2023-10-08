
using System.Threading.Tasks;

namespace GithubBackupTool.Models.Interfaces
{
    public interface IBackupManager
    {
        public Task CreateBackup(Repository repository);

        public Task RestoreBackup(Backup repository);
    }
}
