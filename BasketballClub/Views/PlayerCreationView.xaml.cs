using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BasketballClub.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace BasketballClub.Views {
    /// <summary>
    /// Interaction logic for PlayerCreationView.xaml
    /// </summary>
    public partial class PlayerCreationView : Page
    {
        public PlayerCreationView()
        {
            ViewModel = App.Services.GetRequiredService<PlayerCreationViewModel>();
            InitializeComponent();
            DataContext = ViewModel;
        }


        public PlayerCreationViewModel ViewModel { get; }

      
       
    }
}
