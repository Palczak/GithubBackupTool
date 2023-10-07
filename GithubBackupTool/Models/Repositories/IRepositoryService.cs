using System.Collections.Generic;
using System.Threading.Tasks;

namespace GithubBackupTool.Models.Repositories
{
    public interface IRepositoryService
    {
        public Task<IEnumerable<Repository>> GetRepositories();
        public Task CreateRepository(string name);
    }
}
