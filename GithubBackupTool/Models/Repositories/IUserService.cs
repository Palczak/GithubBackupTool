using System.Threading.Tasks;

namespace GithubBackupTool.Models.Repositories
{
    public interface IUserService
    {
        public Task<User> GetAuthenticatedUser();
    }
}
