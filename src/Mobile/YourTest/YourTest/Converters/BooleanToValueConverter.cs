using System;
using System.Globalization;
using Xamarin.Forms;
namespace YourTest.Converters
{
    public class BooleanToValueConverter<TValue> : IValueConverter
    {
        public TValue TrueValue { get; set; }
        public TValue FalseValue { get; set; }


        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((bool)value) ? TrueValue : FalseValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
