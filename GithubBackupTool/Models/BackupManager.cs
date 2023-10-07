using GithubBackupTool.Infractructure.Interfaces;
using GithubBackupTool.Infrastructure.Interfaces;
using GithubBackupTool.Models.Repositories;

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

        public void CreateBackup(Repository repository)
        {
            var issues = _issueService.GetIssues(repository);
            var encryptedIssues = _encryptor.Encrypt(issues);
            _backupRepository.SaveBackupToFile(encryptedIssues);
            _backupRepository.CreateBackupRecord(repository.Name);
        }

        public void RestoreBackup()
        {
            

        }
    }
}
