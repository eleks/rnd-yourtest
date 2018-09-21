using System;
using System.Globalization;
using Xamarin.Forms;
using CarouselView.FormsPlugin.Abstractions;
namespace YourTest.Converters
{
    public class ConfirmationButtonEnableConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var idex = (Int32)value;
            var control = (CarouselViewControl)parameter;

            if (idex == control.ItemsSource?.GetCount() - 1)
            {
                return true;
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
