using System;
using System.Globalization;
using Xamarin.Forms;
using System.Collections.Generic;
namespace YourTest.Converters
{
    public class EtalonValueToBoleanValueConverter<TEatalon, TValue> : IValueConverter
    {
        public TEatalon EtalonValue { get; set; }

        public TValue TrueValue { get; set; }
        public TValue FalseValue { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TEatalon v = (TEatalon)value;

            return EqualityComparer<TEatalon>.Default.Equals(EtalonValue, v)
                ? TrueValue
                : FalseValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
