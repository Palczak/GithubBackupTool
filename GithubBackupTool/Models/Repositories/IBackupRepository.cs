using System;
using System.Collections.Generic;

namespace GithubBackupTool.Models.Repositories
{
    public interface IBackupRepository
    {
        public void SaveBackupToFile(Repository repository, byte[] value, DateTime backupCreationTime);
        public void CreateBackupRecord(string repositoryName, DateTime backupCreationTime);
        public IEnumerable<Backup> GetLatestBackups();
        public byte[] ReadBackupFromFile(Backup backup);
    }
}
