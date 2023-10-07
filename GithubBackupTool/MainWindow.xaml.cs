using GithubBackupTool.Infrastructure.Interfaces;
using GithubBackupTool.Models.Repositories;
using System;
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
        public MainWindow(IRepositoryService repositoryService, IGithubHttpClient githubHttpClient)
        {
            _repositoryService = repositoryService;
            _githubHttpClient = githubHttpClient;
            InitializeComponent();
        }

        private async void RefreshRepositories_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var repositories = await _repositoryService.GetRepositories();
                ErrorTextBox.Text = "";
            }
            catch(Exception ex)
            {
                ErrorTextBox.Text = ex.Message;
            }
        }

        private void BearerTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            _githubHttpClient.Bearer = ((TextBox)sender).Text;
        }
    }
}
