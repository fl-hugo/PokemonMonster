using System.Windows;

namespace PokemonMonster.Views
{
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private void NavigateToRegisterClick(object sender, RoutedEventArgs e)
        {
            var registerView = new RegisterView();
            Application.Current.MainWindow = registerView;
            registerView.Show();
            this.Close();
        }
    }
}
