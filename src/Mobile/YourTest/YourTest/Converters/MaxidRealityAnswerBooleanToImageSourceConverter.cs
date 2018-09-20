using System;
using System.Globalization;
using Xamarin.Forms;

namespace YourTest.Converters
{
    public class MaxidRealityAnswerBooleanToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (Boolean)value ? ImageSource.FromFile("true") : ImageSource.FromFile("false");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
