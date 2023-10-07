using GithubBackupTool.Models;
using GithubBackupTool.Models.Repositories;
using System;
using System.IO;

namespace GithubBackupTool.Infractructure
{
    public class BackupRepository : IBackupRepository
    {

        private string BackupDirectory =>  Path.Combine(Directory.GetCurrentDirectory(), "Backups");

        public void CreateBackupRecord(string repositoryName)
        {
            throw new System.NotImplementedException();
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
