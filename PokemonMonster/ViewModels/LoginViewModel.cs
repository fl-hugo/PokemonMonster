using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using PokemonMonster.Models;
using PokemonMonster.Repositories;
using PokemonMonster.Views;

namespace PokemonMonster.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        //Fields
        private string _username;
        private SecureString _password;
        private string _errorMessage;
        private bool _isViewVisible = true; 
        private IUserRepository userRepository;

        //Properties
        public string Username
        {
            get { return _username; }
            set { _username = value; OnPropertyChanged(nameof(Username)); }
        }
        public SecureString Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged(nameof(Password)); }
        }
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { _errorMessage = value; OnPropertyChanged(nameof(ErrorMessage)); }
        }
        public bool IsViewVisible
        {
            get { return _isViewVisible; }
            set { _isViewVisible = value; OnPropertyChanged(nameof(IsViewVisible)); }
        }

        //Commands
        public ICommand LoginCommand { get; }
        //public RelayCommand NavigateRegisterCommand { get; set; }

        //Constructors
        public LoginViewModel() 
        {
            userRepository = new UserRepository();
            LoginCommand = new ViewModelCommand(ExectueLoginCommand, CanExecuteLoginCommand);
        }

        //Methods
        private bool CanExecuteLoginCommand(object obj)
        {
            bool validData;
            if (string.IsNullOrWhiteSpace(Username) || Username.Length < 1
                || Password == null || Password.Length < 5)
                validData = false;
            else
                validData = true;
            return validData;
        }

        private void ExectueLoginCommand(object obj)
        {
            var isValidUser = userRepository.AuthenticateUser(new NetworkCredential(Username, Password));
            if (isValidUser)
            {
                Thread.CurrentPrincipal = new GenericPrincipal(
                    new GenericIdentity(Username), null);
                IsViewVisible = false;

                Application.Current.Dispatcher.Invoke(() =>
                {
                    var monsterListView = new MonsterListView();
                    monsterListView.DataContext = new MonsterListViewModel(); 
                    Application.Current.MainWindow = monsterListView;
                    monsterListView.Show();
                });

                CloseLoginWindow();
            }
            else
            {
                ErrorMessage = "Invalid username or password";
            }
        }

        private void CloseLoginWindow()
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window is LoginView)
                {
                    window.Close();
                    break;
                }
            }
        }
    }
}
