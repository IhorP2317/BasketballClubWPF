using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace BasketballClub.Controls 
{
    /// <summary>
    /// Interaction logic for NumericTextBox.xaml
    /// </summary>


    public partial class NumericTextBox : UserControl {
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(double), typeof(NumericTextBox), new PropertyMetadata(0.0));

        public double Value {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public NumericTextBox() {
            InitializeComponent();
        }

        private void InputTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e) {
            // Allowing digits, decimal separator ('.' or ','), and negative sign
            if (!IsNumeric(e.Text) && e.Text != "-" && e.Text != "." && e.Text != ",") {
                e.Handled = true;
            }
        }

        private static bool IsNumeric(string text) {
            return double.TryParse(text, NumberStyles.Any, CultureInfo.CurrentCulture, out _);
        }
    }

}

