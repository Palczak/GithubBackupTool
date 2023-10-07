using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GithubBackupTool.Infrastructure;
using GithubBackupTool.Models;
using GithubBackupTool.Models.Repositories;

namespace GithubBackupTool.Infractructure
{
    public class IssueService : IIssueService
    {
        private readonly HttpClient _httpClient = new GithubHttpClient().Client;

        public async Task<IEnumerable<Issue>> GetIssues(Repository repository)
        {
            throw new NotImplementedException();
            var httpUrl = $"repos/{repository.Owner.Login}/{repository.Name}/issues";

            HttpResponseMessage response = await _httpClient.GetAsync(httpUrl);
            if (response.IsSuccessStatusCode)
            {
                //json convertion etc

            }
            else
            {
                throw new Exception("Fail");
            }
            return (IEnumerable<Issue>)response.Content;
        }

        public async Task PostIssues(Repository repository, IEnumerable<Issue> issues)
        {
            throw new NotImplementedException();
        }
    }
}
