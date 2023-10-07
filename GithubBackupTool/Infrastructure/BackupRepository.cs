using GithubBackupTool.Infrastructure;
using GithubBackupTool.Models;
using GithubBackupTool.Models.Repositories;
using System;
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

        private string BackupDirectory =>  Path.Combine(Directory.GetCurrentDirectory(), "Backups");

        public void CreateBackupRecord(string repositoryName)
        {
            var backup = new Backup
            {
                RepositoryName = repositoryName,
                BackupUTC = DateTime.UtcNow
            };
            _backupRecordContext.Add(backup);
            _backupRecordContext.SaveChanges();
        }

        public byte[] ReadBackupFromFile()
        {
            throw new System.NotImplementedException();
        }

        public async void SaveBackupToFile(Repository repository, byte[] value)
        {
            EnsureBackupFolderExist();
            var backupFileName = $"{repository.Name}-{DateTime.UtcNow}.bin".Replace(":", ".");

            File.WriteAllBytes(Path.Combine(BackupDirectory, backupFileName), value);
        }

        private void EnsureBackupFolderExist()
        {
            if(!Directory.Exists(BackupDirectory))
            {
                Directory.CreateDirectory(BackupDirectory);
            }

        }
    }
}
