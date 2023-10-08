using GithubBackupTool.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.SQLite;

namespace GithubBackupTool.Infrastructure
{
    public class BackupContext : DbContext
    {
        private const string DatabaseName = "GithubBackup.sqlite";
        private const string ConnectionString = $"Data Source={DatabaseName};";
        public DbSet<Backup> Backups { get; set; }

        public BackupContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite(ConnectionString);

        public static void EnsureTableExist()
        {
            SQLiteConnection dbConnection = new SQLiteConnection(ConnectionString);
            dbConnection.Open();

            string sql = "Create Table IF NOT EXISTS Backups (Id INTEGER PRIMARY KEY AUTOINCREMENT, RepositoryName TEXT, BackupUTC INTEGER)";
            SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
            command.ExecuteNonQuery();

            dbConnection.Close();
        }

    }
}
