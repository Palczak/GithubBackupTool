using GithubBackupTool.Infrastructure;
using GithubBackupTool.Models;
using GithubBackupTool.Models.Repositories;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GithubBackupTool.Infractructure
{
    public class BackupRepository : IBackupRepository
    {
        private readonly BackupContext _backupRecordContext;

        public BackupRepository(BackupContext backupRecordContext)
        {
            _backupRecordContext = backupRecordContext;
        }

        private string BackupDirectory => Path.Combine(Directory.GetCurrentDirectory(), "Backups");

        public void CreateBackupRecord(string repositoryName, DateTime backupCreationTime)
        {
            var backup = new Backup
            {
                RepositoryName = repositoryName,
                BackupUTC = backupCreationTime
            };
            _backupRecordContext.Add(backup);
            _backupRecordContext.SaveChanges();
        }

        public IEnumerable<Backup> GetLatestBackups()
        {
            var result = _backupRecordContext.Backups.GroupBy(p => p.RepositoryName).Select(p => p.FirstOrDefault(w => w.BackupUTC == p.Max(m => m.BackupUTC))).ToList().OrderBy(p => p.RepositoryName);
            return result;
        }

        public byte[] ReadBackupFromFile(Backup backup)
        {
            var backupFilePath = GenerateBackupFilePathAndName(backup.RepositoryName, backup.BackupUTC);

            return File.ReadAllBytes(backupFilePath);
        }

        public void SaveBackupToFile(Repository repository, byte[] value, DateTime backupCreationTime)
        {
            EnsureBackupFolderExist();
            var backupFilePath = GenerateBackupFilePathAndName(repository.Name, backupCreationTime);

            File.WriteAllBytes(backupFilePath, value);
        }

        private string GenerateBackupFilePathAndName(string repositoryName, DateTime backupCreationTime)
        {
            var backupFileName = $"{repositoryName}-{backupCreationTime}".Replace(":", ".");
            return Path.Combine(BackupDirectory, backupFileName);
        }

        private void EnsureBackupFolderExist()
        {
            if (!Directory.Exists(BackupDirectory))
            {
                Directory.CreateDirectory(BackupDirectory);
            }

        }
    }
}
