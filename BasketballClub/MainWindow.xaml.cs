using BasketballClub.Services;
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

namespace BasketballClub {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        private readonly INavigationService navigationService;
        public MainWindow() {
            Current = this;
            navigationService = App.Services.GetRequiredService<INavigationService>();
            InitializeComponent();
            ViewModel = App.Services.GetRequiredService<ShellViewModel>();
            DataContext = ViewModel;
        }
        public static MainWindow Current { get; private set; } = null!;
        public ShellViewModel ViewModel { get; private set; }
        public ContentControl DialogsContainer => dialogsContainer;
        private void Window_Loaded(object sender, RoutedEventArgs e) {
            navigationService.Frame = rootFrame;
            navigationService.NavigateToMainView();
        }
    }
}
