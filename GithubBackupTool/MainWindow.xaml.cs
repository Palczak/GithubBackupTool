using GithubBackupTool.Infrastructure;
using GithubBackupTool.Infrastructure.Interfaces;
using GithubBackupTool.Models;
using GithubBackupTool.Models.Interfaces;
using GithubBackupTool.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GithubBackupTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IRepositoryService _repositoryService;
        private readonly IGithubHttpClient _githubHttpClient;
        private readonly IBackupManager _backupManager;
        private readonly IBackupRepository _backupRepository;

        private IEnumerable<Repository> _repositories = new List<Repository>();
        private IEnumerable<Backup> _backups = new List<Backup>();

        public MainWindow(IRepositoryService repositoryService, IGithubHttpClient githubHttpClient, IBackupManager backupManager, IBackupRepository backupRepository)
        {
            _repositoryService = repositoryService;
            _githubHttpClient = githubHttpClient;
            _backupManager = backupManager;
            _backupRepository = backupRepository;

            BackupContext.EnsureTableExist();

            InitializeComponent();
        }

        private async void RefreshRepositories_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _repositories = await _repositoryService.GetRepositories();
                RepositoriesView.ItemsSource = _repositories;
                ClearErrorMessage();
            }
            catch (Exception ex)
            {
                MessageTextBox.Text = ex.Message;
            }
        }

        private void BearerTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            _githubHttpClient.Bearer = ((TextBox)sender).Text;
        }

        private void ClearErrorMessage()
        {
            MessageTextBox.Text = "";
            MessageTextBox.Foreground = Brushes.DarkRed;
        }

        private async void RepositoryItem_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                var castedSender = (TextBlock)sender;
                var repository = (Repository)castedSender.DataContext;

                await _backupManager.CreateBackup(repository);

                RefreshBackups();
            }
            catch (Exception ex)
            {
                MessageTextBox.Text = ex.Message;
            }
        }

        private void BackupItem_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                var castedSender = (StackPanel)sender;
                var textBlock = (TextBlock)castedSender.Children[0];
                var backup = (Backup)textBlock.DataContext;

                _backupManager.RestoreBackup(backup);
                MessageTextBox.Text = "Backup restored successfully";
                MessageTextBox.Foreground = Brushes.DarkGreen;
            }
            catch (Exception ex)
            {
                MessageTextBox.Text = ex.Message;
            }
        }

        private void BackupsView_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshBackups();
        }

        private void RefreshBackups()
        {
            _backups = _backupRepository.GetLatestBackups();
            BackupsView.ItemsSource = _backups;
        }
    }
}
