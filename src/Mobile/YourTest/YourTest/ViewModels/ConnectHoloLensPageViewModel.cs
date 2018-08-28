using System;
using YourTest.Manager;

namespace YourTest.ViewModels
{
    public class ConnectHoloLensPageViewModel : ViewModelBase
    {
        public ConnectHoloLensPageViewModel(IIPAddressManager ipAddressManager)
        {
            _ipAddressManager = ipAddressManager;
            LocalIPAddress = _ipAddressManager.GetIPAddress();
        }

        public String LocalIPAddress
        {
            get => _localIPAddress;
            set => SetProperty(ref _localIPAddress, value);
        }

        private String _localIPAddress;
        private IIPAddressManager _ipAddressManager;
    }
}
