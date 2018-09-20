using System;
using System.Globalization;
using Xamarin.Forms;
using CarouselView.FormsPlugin.Abstractions;
using YourTest.ViewModels.ActiveTest;
namespace YourTest.Converters
{
    public class ConfirmationButtonEnableConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var idex = (Int32)value;
            var control = (CarouselViewControl)parameter;
            var vm = (ActiveTestPageViewModel)control.BindingContext;

            if (idex == vm.Test?.Questions?.Count - 1)
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
