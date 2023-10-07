using System.Net.Http;

namespace GithubBackupTool.Infrastructure.Interfaces
{
    public interface IGithubHttpClient
    {
        public HttpClient Client { get; }
        public string Bearer { set; }
    }
}
