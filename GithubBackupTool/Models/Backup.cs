using System;

namespace GithubBackupTool.Models
{
    public class Backup
    {
        public int Id { get; set; }
        public string RepositoryName { get; set; }
        public DateTime BackupUTC { get; set; }
    }
}
