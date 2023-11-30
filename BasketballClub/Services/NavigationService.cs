using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using BasketballClub.Views;

namespace BasketballClub.Services
{
    public  class NavigationService: INavigationService {
        public Frame? Frame { get; set; }
        public void NavigateToPlayerCreationView() {
            var playerCreationView = new PlayerCreationView();
            Frame?.Navigate(playerCreationView);
        }

        public void NavigateToMainView() {
            var mainView = new MainView();
            Frame?.Navigate(mainView);
        }
    }
}
