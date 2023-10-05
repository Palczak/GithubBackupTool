using GithubBackupTool.Infractructure.Interfaces;
using GithubBackupTool.Infrastructure.Interfaces;

namespace GithubBackupTool.Models
{
    public class BackupManager
    {
        private readonly IRepositoryService _repositoryService;
        private readonly IIssueService _issueService;
        private readonly IBackupRepository _backupRepository;
        private readonly IEncryptor _encryptor;

        public BackupManager(IRepositoryService repositoryService, IIssueService issueService, IBackupRepository backupRepository, IEncryptor encryptor)
        {
            _repositoryService = repositoryService;
            _issueService = issueService;
            _backupRepository = backupRepository;
            _encryptor = encryptor;
        }

        public void CreateBackup(string repositroyName)
        {
            var issues = _issueService.GetIssues(repositroyName);
            var encryptedIssues = _encryptor.Encrypt(issues);
            _backupRepository.WriteBackup(encryptedIssues);
            _backupRepository.CreaceBackupRecord(repositroyName);
        }

        public void RestoreBackup()
        {
            

        }
    }
}
