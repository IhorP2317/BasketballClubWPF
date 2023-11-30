using BasketballClub.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using BasketballClub.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace BasketballClub {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {
        public App() {
            InitializeServices();
        }
        public static IServiceProvider Services { get; private set; } = null!;

        private void InitializeServices() {
            var services = new ServiceCollection();

            services.AddSingleton<CommonHttpClientService>();
            services.AddSingleton<IPlayerService, PlayerService>();
            services.AddSingleton<ITeamService, TeamService>();
            services.AddSingleton<INavigationService, NavigationService>();

            services.AddSingleton<MainViewModel>();
            services.AddSingleton<PlayerCreationViewModel>();
            services.AddSingleton<ShellViewModel>();

            Services = services.BuildServiceProvider();
        }
    }
}
