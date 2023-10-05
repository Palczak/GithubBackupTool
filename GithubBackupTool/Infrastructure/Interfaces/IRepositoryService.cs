using System.Collections.Generic;

namespace GithubBackupTool.Infractructure.Interfaces
{
    public interface IRepositoryService
    {
        public IEnumerable<string> GetRepositories();
        public void CreateRepository(string name);
    }
}
