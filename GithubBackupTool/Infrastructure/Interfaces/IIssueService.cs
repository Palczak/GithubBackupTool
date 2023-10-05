using System.Collections.Generic;
using GithubBackupTool.Models;

namespace GithubBackupTool.Infractructure.Interfaces
{
    public interface IIssueService
    {
        public IEnumerable<Issue> GetIssues(string repositoryName);
        public void PostIssues(string repositoryName, IEnumerable<Issue> issues);
    }
}
