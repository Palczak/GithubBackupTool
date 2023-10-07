using System.Net.Http.Headers;
using System.Net.Http;
using System;
using GithubBackupTool.Infrastructure.Interfaces;

namespace GithubBackupTool.Infrastructure.WebServices
{
    public class GithubHttpClient : IGithubHttpClient
    {
        public HttpClient Client { get; }
        public string Bearer { set { Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", value); } }

        public GithubHttpClient()
        {
            Client = new HttpClient();
            Client.BaseAddress = new Uri("https://api.github.com");
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github+json"));
            Client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("GithubBackupTool", "1.0"));
        }
    }
}
