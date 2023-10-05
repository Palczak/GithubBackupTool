using System.Collections.Generic;
using GithubBackupTool.Infractructure.Interfaces;
using GithubBackupTool.Models;

namespace GithubBackupTool.Infractructure
{
    public class IssueService : IIssueService
    {
        public IEnumerable<Issue> GetIssues(string repositoryName)
        {
            throw new System.NotImplementedException();
        }

        public void PostIssues(string repositoryName, IEnumerable<Issue> issues)
        {
            throw new System.NotImplementedException();
        }
    }
}
