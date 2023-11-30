using BasketballClub.ViewModels;
using Microsoft.Extensions.DependencyInjection;
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

namespace BasketballClub.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Page
    {
        public MainView() {
            ViewModel = App.Services.GetRequiredService<MainViewModel>();
            InitializeComponent();
            DataContext = ViewModel;
        }

        public MainViewModel ViewModel { get; private set; }

        private async void Page_Loaded(object sender, RoutedEventArgs e) {
            await ViewModel.InitializeAsync();
        }
    }
}
