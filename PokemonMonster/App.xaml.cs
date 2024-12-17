using System.Windows;
using PokemonMonster.Repositories;
using PokemonMonster.Views;

namespace PokemonMonster
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var mainWindow = new LoginView();
            mainWindow.Show();

            base.OnStartup(e);
        }

        //---------------------------------------Database connection from Window (failed)-----------------------------------------
        //public static string UserConnectionString { get; private set; }

        //protected override void OnStartup(StartupEventArgs e)
        //{
        //    base.OnStartup(e);

        //    var connectionView = new DatabaseConnectionView();
        //    if (connectionView.ShowDialog() == true)
        //    {
        //        UserConnectionString = connectionView.ConnectionString;
        //        var mainWindow = new LoginView(); 
        //        mainWindow.Show();
        //    }
        //    else
        //    {
        //        Shutdown();
        //    }
        //}
    }
}
