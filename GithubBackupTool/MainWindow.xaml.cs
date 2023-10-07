using GithubBackupTool.Infrastructure;
using GithubBackupTool.Infrastructure.Interfaces;
using GithubBackupTool.Models;
using GithubBackupTool.Models.Interfaces;
using GithubBackupTool.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

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

        private IEnumerable<Repository> _repositories = new List<Repository>();

        public MainWindow(IRepositoryService repositoryService, IGithubHttpClient githubHttpClient, IBackupManager backupManager)
        {
            _repositoryService = repositoryService;
            _githubHttpClient = githubHttpClient;
            _backupManager = backupManager;

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
                ErrorTextBox.Text = ex.Message;
            }
        }

        private void BearerTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            _githubHttpClient.Bearer = ((TextBox)sender).Text;
        }

        private void ClearErrorMessage()
        {
            ErrorTextBox.Text = "";
        }

        private void TextBlock_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                _backupManager.CreateBackup(_repositories.First());
            }
            catch (Exception ex)
            {
                ErrorTextBox.Text = ex.Message;
            }
        }
    }
}
