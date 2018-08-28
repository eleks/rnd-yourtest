using System;
using Xamarin.Forms;
using YourTest.Manager;
using Prism.Services;

namespace YourTest.ViewModels
{
    public class ConnectHoloLensPageViewModel : ViewModelBase
    {
        public ConnectHoloLensPageViewModel(IIPAddressManager ipAddressManager,
                                            IPageDialogService pageDialogService,
                                            IHololensCommunicationManager hololensCommunicationManager)
        {
            _ipAddressManager = ipAddressManager;
            _pageDialogService = pageDialogService;
            _hololensCommunicationManager = hololensCommunicationManager;
        }

        public String LocalIPAddress => _ipAddressManager.GetIPAddress();

        private IIPAddressManager _ipAddressManager;
        private IPageDialogService _pageDialogService;
        private IHololensCommunicationManager _hololensCommunicationManager;
    }
}
