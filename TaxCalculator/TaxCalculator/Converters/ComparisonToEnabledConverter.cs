using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;

namespace TaxCalculator.Converters
{
    public class ComparisonToEnabledConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var trueText = (string)parameter;
            var otherText = (string)value;

            if (string.IsNullOrEmpty(trueText) || string.IsNullOrEmpty(otherText))
            {
                return false;
            }

            List<string> list = trueText.Split(',').ToList();

            var allow = false;

            foreach(var text in list)
            {
                if (text == otherText)
                {
                    allow = true;
                    break;
                }
            }

            return allow;

            //return trueText.ToUpper() == otherText.ToUpper();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

