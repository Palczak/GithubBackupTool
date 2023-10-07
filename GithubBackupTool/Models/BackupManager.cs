using GithubBackupTool.Infrastructure.Interfaces;
using GithubBackupTool.Models.Interfaces;
using GithubBackupTool.Models.Repositories;
using System.Threading.Tasks;

namespace GithubBackupTool.Models
{
    public class BackupManager : IBackupManager
    {
        private readonly IRepositoryService _repositoryService;
        private readonly IIssueService _issueService;
        private readonly IBackupRepository _backupRepository;
        private readonly IIssuesEncryptor _encryptor;

        public BackupManager(IRepositoryService repositoryService, IIssueService issueService, IBackupRepository backupRepository, IIssuesEncryptor encryptor)
        {
            _repositoryService = repositoryService;
            _issueService = issueService;
            _backupRepository = backupRepository;
            _encryptor = encryptor;
        }

        public async Task CreateBackup(Repository repository)
        {
            var issues = await _issueService.GetIssues(repository);
            var encryptedIssues = _encryptor.Encrypt(issues);
            _backupRepository.SaveBackupToFile(repository, encryptedIssues);
            _backupRepository.CreateBackupRecord(repository.Name);
        }

        public async void RestoreBackup(Repository repository)
        {
            await _repositoryService.CreateRepository(repository.Name);

            var encryptedIssues = _backupRepository.ReadBackupFromFile();
            var issues = _encryptor.Decrypt(encryptedIssues);
            await _issueService.PostIssues(repository, issues);
        }
    }
}
