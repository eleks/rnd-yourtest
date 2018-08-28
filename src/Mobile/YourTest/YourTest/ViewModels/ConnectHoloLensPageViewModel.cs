using System;
using YourTest.Manager;

namespace YourTest.ViewModels
{
    public class ConnectHoloLensPageViewModel : ViewModelBase
    {
        public ConnectHoloLensPageViewModel(IIPAddressManager ipAddressManager)
        {
            _ipAddressManager = ipAddressManager;
        }

        public String LocalIPAddress => _ipAddressManager.GetIPAddress();

        private IIPAddressManager _ipAddressManager;
    }
}
