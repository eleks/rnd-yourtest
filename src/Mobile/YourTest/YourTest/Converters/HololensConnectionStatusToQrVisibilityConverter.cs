using System;
using System.Globalization;
using Xamarin.Forms;
using YourTest.Models;

namespace YourTest.Converters
{
    public class HololensConnectionStatusToQrVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var hololensConnectionStatus = (HololensConnectionStatus)value;
            return hololensConnectionStatus == HololensConnectionStatus.WaitingForPairing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
