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
using BasketballClub.Models;

namespace BasketballClub.Controls.Dialogs
{
    /// <summary>
    /// Interaction logic for NotificationDialog.xaml
    /// </summary>
    public partial class ConfirmationDialog : UserControl {
        public ConfirmationDialog() {
            InitializeComponent();
        }

        private TaskCompletionSource<ConfirmationDialogResult> dismissTaskSource = new();

        public async Task<ConfirmationDialogResult> ShowAsync(string message) {
            var dialogsContainer = MainWindow.Current.DialogsContainer;
            dialogsContainer.Content = this;
            messageTextBlock.Text = message;
            Visibility = Visibility.Visible;
            dismissTaskSource = new TaskCompletionSource<ConfirmationDialogResult>();
            var result = await dismissTaskSource.Task;
            return result;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e) {
            CloseDialog();
            dismissTaskSource.TrySetResult(ConfirmationDialogResult.Cancelled);
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            CloseDialog();
            dismissTaskSource.TrySetResult(ConfirmationDialogResult.Confirmed);
        }
  

        private void CloseDialog() {
            Visibility = Visibility.Collapsed;
            if (MainWindow.Current.DialogsContainer.Content == this) {
                MainWindow.Current.DialogsContainer.Content = null;
            }
        }
    }
}
