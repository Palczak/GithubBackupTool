namespace GithubBackupTool.Models
{
    public class Repository
    {
        public string Name { get; set; }
        public Owner Owner { get; set; }
    }

    public class Owner
    {
        public string Login { get; set; }
    }
}
