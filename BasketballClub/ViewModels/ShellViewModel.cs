using BasketballClub.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BasketballClub.ViewModels
{
    public class ShellViewModel : ObservableObject {
        public ICommand OpenPlayersViewCommand { get; private set; } = null!;
        public ICommand OpenPlayerCreationViewCommand { get; private set; } = null!;
        private readonly INavigationService navigationService;

        public ShellViewModel(INavigationService navigationService) {
            this.navigationService = navigationService;
            InitializeCommands();
        }


        private void InitializeCommands() {
            OpenPlayersViewCommand = new RelayCommand(navigationService.NavigateToMainView);
            OpenPlayerCreationViewCommand = new RelayCommand(navigationService.NavigateToPlayerCreationView);
        }

    }
}
