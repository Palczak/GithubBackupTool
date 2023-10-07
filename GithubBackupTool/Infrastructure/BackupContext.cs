using GithubBackupTool.Infrastructure.Interfaces;
using GithubBackupTool.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data.SQLite;

namespace GithubBackupTool.Infrastructure
{
    public class BackupContext : DbContext
    {
        private string DatabaseName => "GithubBackup.sqlite";
        private string ConnectionString => $"Data Source={DatabaseName};";
        public DbSet<Backup> Backups { get; set; }

        public BackupContext()
        {
            EnsureTableExist();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite(ConnectionString);

        private void EnsureTableExist()
        {
            SQLiteConnection.CreateFile(DatabaseName);

            string connectionString = ConnectionString;
            SQLiteConnection m_dbConnection = new SQLiteConnection(connectionString);
            m_dbConnection.Open();

            string sql = "Create Table IF NOT EXISTS Backups (Id INTEGER PRIMARY KEY AUTOINCREMENT, RepositoryName TEXT, BackupUTC INTEGER)";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

            m_dbConnection.Close();
        }

    }
}
