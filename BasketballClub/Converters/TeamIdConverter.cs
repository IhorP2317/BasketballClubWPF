using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace BasketballClub.Converters
{
    public class TeamIdConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            // Convert int? to string for display
            return value?.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            if (string.IsNullOrWhiteSpace(value as string)) {
                return null;
            }

            // Convert string to int?
            if (int.TryParse(value as string, out int result)) {
                return result;
            }

            return null;
        }
    }
}
