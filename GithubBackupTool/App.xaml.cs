﻿using GithubBackupTool.Infractructure;
using GithubBackupTool.Infrastructure;
using GithubBackupTool.Infrastructure.Interfaces;
using GithubBackupTool.Infrastructure.WebServices;
using GithubBackupTool.Models;
using GithubBackupTool.Models.Interfaces;
using GithubBackupTool.Models.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;

namespace GithubBackupTool
{
    public partial class App : Application
    {
        public static IHost? AppHost { get; private set; }

        public App()
        {
            AppHost = Host.CreateDefaultBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<MainWindow>();
                    services.AddTransient<IRepositoryService, RepositoryService>();
                    services.AddSingleton<IGithubHttpClient, GithubHttpClient>();
                    services.AddTransient<IIssuesEncryptor, AesIssuesEncryptor>();
                    services.AddTransient<IEncryptionKeyProvider, MockEncryptionKeyProvider>();
                    services.AddTransient<IBackupManager, BackupManager>();
                    services.AddTransient<IIssueService, IssueService>();
                    services.AddTransient<IUserService, UserService>();
                    services.AddTransient<IBackupRepository, BackupRepository>();
                    services.AddTransient<BackupContext>();
                })
                .Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await AppHost!.StartAsync();

            var startupForm = AppHost.Services.GetRequiredService<MainWindow>();
            startupForm.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await AppHost!.StopAsync();
            base.OnExit(e);
        }

    }
}
