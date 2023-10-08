using GithubBackupTool.Infrastructure.Interfaces;
using GithubBackupTool.Models.Interfaces;
using GithubBackupTool.Models.Repositories;
using System;
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

            var backupCreationTime = DateTime.UtcNow;
            var encryptedIssues = _encryptor.Encrypt(issues);
            _backupRepository.SaveBackupToFile(repository, encryptedIssues, backupCreationTime);
            _backupRepository.CreateBackupRecord(repository.Name, backupCreationTime);
        }

        public async Task RestoreBackup(Backup backup)
        {
            await _repositoryService.CreateRepository(backup.RepositoryName + backup.Id);
            var encryptedIssues = _backupRepository.ReadBackupFromFile(backup);
            var issues = _encryptor.Decrypt(encryptedIssues);

            await _issueService.PostIssues(backup.RepositoryName+backup.Id, issues);
        }
    }
}
