using GithubBackupTool.Infrastructure.Interfaces;
using GithubBackupTool.Models;
using GithubBackupTool.Models.Repositories;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace GithubBackupTool.Infrastructure.WebServices
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;

        public UserService(IGithubHttpClient githubHttpClient)
        {
            _httpClient = githubHttpClient.Client;
        }

        public async Task<User> GetAuthenticatedUser()
        {
            var httpUrl = "/user";

            User result;

            HttpResponseMessage response = await _httpClient.GetAsync(httpUrl, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var rawResponse = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<User>(rawResponse)!;
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
            return result;
        }
    }
}
