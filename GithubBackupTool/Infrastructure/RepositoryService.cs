using GithubBackupTool.Infrastructure.Interfaces;
using GithubBackupTool.Models;
using GithubBackupTool.Models.Repositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace GithubBackupTool.Infractructure
{
    public class RepositoryService : IRepositoryService
    {
        private readonly HttpClient _httpClient;

        public RepositoryService(IGithubHttpClient githubHttpClient)
        {
            _httpClient = githubHttpClient.Client;
        }


        public async Task CreateRepository(string name)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<Repository>> GetRepositories()
        {
            var httpUrl = "/user/repos";
            IEnumerable<Repository> result;

            HttpResponseMessage response = await _httpClient.GetAsync(httpUrl, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var rawResponse = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<IEnumerable<Repository>>(rawResponse)!;
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
            return result;
        }
    }
}
