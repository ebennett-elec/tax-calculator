using System;
using System.Globalization;
using Xamarin.Forms;

namespace TaxCalculator.Converters
{
    public class IsRequiredConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //I realize its not the greatest of examples for a converter.
            var text = (string)parameter ?? "";
            if ((bool)value == true)
            {
                return text + " (Required)";
            }
            return text;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

