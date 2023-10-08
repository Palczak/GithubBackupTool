namespace GithubBackupTool.Models
{
    public class Issue
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public User User { get; set; }
    }
}
