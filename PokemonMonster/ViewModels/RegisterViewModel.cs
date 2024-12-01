using System;
using System.Net;
using System.Security;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using PokemonMonster.Models;
using PokemonMonster.Repositories;
using PokemonMonster.Services;

namespace PokemonMonster.ViewModels
{
    public class RegisterViewModel : ViewModelBase
    {
        //Fields
        private string _username;
        private SecureString _password;
        private SecureString _confirmPassword;
        private string _errorMessage;
        private IUserRepository _userRepository;
        private INavigationService _navigation;
        public INavigationService Navigation
        {
            get { return _navigation; }
            set { _navigation = value; OnPropertyChange(); }
        }

        //Properties
        public string Username
        {
            get => _username;
            set { _username = value; OnPropertyChanged(nameof(Username)); }
        }

        public SecureString Password
        {
            get => _password;
            set { _password = value; OnPropertyChanged(nameof(Password)); }
        }

        public SecureString ConfirmPassword
        {
            get => _confirmPassword;
            set { _confirmPassword = value; OnPropertyChanged(nameof(ConfirmPassword)); }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set { _errorMessage = value; OnPropertyChanged(nameof(ErrorMessage)); }
        }

        //Commands
        public ICommand RegisterCommand { get; }
        public RelayCommand NavigateLoginCommand { get; set; }

        //Constructors
        public RegisterViewModel()
        {
            _userRepository = new UserRepository();
            RegisterCommand = new ViewModelCommand(ExecuteRegisterCommand, CanExecuteRegisterCommand);
        }
        public RegisterViewModel(INavigationService navService)
        {
            _userRepository = new UserRepository();
            RegisterCommand = new ViewModelCommand(ExecuteRegisterCommand, CanExecuteRegisterCommand);
            Navigation = navService;
            NavigateLoginCommand = new RelayCommand(execute: () => { Navigation.NavigateTo<LoginViewModel>(); },
                                                       canExecute: () => true);
        }

        //Methods
        private bool CanExecuteRegisterCommand(object obj)
        {
            return !string.IsNullOrWhiteSpace(Username) &&
                   Password != null && Password.Length >= 5 &&
                   ConfirmPassword != null && ConfirmPassword.Length >= 5;
        }

        private void ExecuteRegisterCommand(object obj)
        {
            if (!SecureStringEqual(Password, ConfirmPassword))
            {
                ErrorMessage = "Les mots de passe ne correspondent pas.";
                return;
            }

            try
            {
                _userRepository.Add(new UserModel
                {
                    Username = Username,
                    Password = new NetworkCredential(string.Empty, Password).Password // Convert SecureString
                });
                ErrorMessage = "Inscription réussie.";
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Une erreur s'est produite : {ex.Message}";
            }
        }

        private bool SecureStringEqual(SecureString s1, SecureString s2)
        {
            if (s1.Length != s2.Length) return false;
            var b1 = new NetworkCredential(string.Empty, s1).Password;
            var b2 = new NetworkCredential(string.Empty, s2).Password;
            return b1 == b2;
        }

        public override bool Equals(object? obj)
        {
            return obj is RegisterViewModel model &&
                   EqualityComparer<INavigationService>.Default.Equals(Navigation, model.Navigation);
        }
    }
}
