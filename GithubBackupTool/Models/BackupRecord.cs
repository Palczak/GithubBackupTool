using System;

namespace GithubBackupTool.Models
{
    public class BackupRecord
    {
        public string RepositoryName { get; set; }
        public DateTime BackupUTC { get; set; }

        //filepath?
    }
}
