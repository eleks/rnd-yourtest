using System;
using System.Globalization;
using Xamarin.Forms;
using YourTest.Models;

namespace YourTest.Converters
{
    public class HololensConnectionStatusToTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var hololensConnectionStatus = (HololensConnectionStatus)value;
            if (hololensConnectionStatus == HololensConnectionStatus.WaitingForPairing)
            {
                return $"Open HoloLens companion app and scan this qr code in order to be connected";
            }
            else
            {
                return "Paired with HoloLens";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
