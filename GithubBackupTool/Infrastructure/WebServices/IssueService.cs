using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
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
        private readonly IUserService _userService;

        public IssueService(IGithubHttpClient githubHttpClient, IUserService userService)
        {
            _httpClient = githubHttpClient.Client;
            _userService = userService;
        }

        public async Task<IEnumerable<Issue>> GetIssues(Repository repository)
        {
            var httpUrl = $"/repos/{repository.Owner.Login}/{repository.Name}/issues?state=all&per_page=100";
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

        public async Task PostIssues(string repository, IEnumerable<Issue> issues)
        {
            var authenticatedUser = await _userService.GetAuthenticatedUser();
            foreach (var issue in issues)
            {
                var httpUrl = $"/repos/{authenticatedUser.Login}/{repository}/issues";

                var jsonContent = JsonConvert.SerializeObject(new { title = issue.Title, body = issue.Body });
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync(httpUrl, content).ConfigureAwait(false);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(response.StatusCode.ToString());
                }
            }
        }
    }
}
