using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GithubBackupTool.Infrastructure.Interfaces;
using GithubBackupTool.Models;
using GithubBackupTool.Models.Repositories;
using Newtonsoft.Json;

namespace GithubBackupTool.Infrastructure.WebServices
{
    public class IssueService : IIssueService
    {
        private readonly HttpClient _httpClient;

        public IssueService(IGithubHttpClient githubHttpClient)
        {
            _httpClient = githubHttpClient.Client;
        }

        public async Task<IEnumerable<Issue>> GetIssues(Repository repository)
        {
            var httpUrl = $"/repos/{repository.Owner.Login}/{repository.Name}/issues";
            IEnumerable<Issue> result;

            HttpResponseMessage response = await _httpClient.GetAsync(httpUrl, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var rawResponse = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<IEnumerable<Issue>>(rawResponse)!;
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
            return result;
        }

        public async Task PostIssues(Repository repository, IEnumerable<Issue> issues)
        {
            throw new NotImplementedException();
        }
    }
}
