using System.Collections.Generic;
using System.Threading.Tasks;

namespace GithubBackupTool.Models.Repositories
{
    public interface IIssueService
    {
        public Task<IEnumerable<Issue>> GetIssues(Repository repository);
        public Task PostIssues(string repository, IEnumerable<Issue> issues);
    }
}
