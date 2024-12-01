using System.Windows;
using PokemonMonster.Views;
using Microsoft.Extensions.DependencyInjection;
using PokemonMonster.ViewModels;
using PokemonMonster.Services;

namespace PokemonMonster
{
    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;

        public App()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddSingleton<LoginView>(provider => new LoginView
            {
                DataContext = provider.GetRequiredService<LoginViewModel>()
            });
            services.AddSingleton<LoginViewModel>();
            services.AddSingleton<RegisterViewModel>();
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<Func<Type, ViewModelBase>>(serviceProvider => 
                viewModelType => (ViewModelBase)serviceProvider.GetRequiredService(viewModelType));

            _serviceProvider = services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var mainWindow = new MainWindow();
            var loginView = _serviceProvider.GetRequiredService<LoginView>();

            mainWindow.MainContentControl.Content = loginView;

            mainWindow.Show();
            base.OnStartup(e);
        }
    }
}
