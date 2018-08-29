using System;
using System.Windows.Input;
using Prism.Commands;
using Prism.Services;
using Xamarin.Forms;
using YourTest.Manager;
using YourTest.Models;

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

            ResetCommand = new DelegateCommand(OnReset);

            Status = HololensConnectionStatus.WaitingForPairing;
        }

        public HololensConnectionStatus Status
        {
            get => _status;
            set => SetProperty(ref _status, value);
        }

        public String LocalIPAddress => _ipAddressManager.GetIPAddress();

        public ICommand ResetCommand { get; set; }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            _hololensCommunicationManager.MessageReceived += OnHololensMessageReceived;
            _hololensCommunicationManager.ClientConnected += OnHololensClientConnected;

            _hololensCommunicationManager.StartListening(LocalIPAddress, 8888);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            _hololensCommunicationManager.StopListening();

            _hololensCommunicationManager.MessageReceived -= OnHololensMessageReceived;
            _hololensCommunicationManager.ClientConnected -= OnHololensClientConnected;
        }

        private void OnReset()
        {
            Status = HololensConnectionStatus.WaitingForPairing;

            _hololensCommunicationManager.StopListening();
            _hololensCommunicationManager.StartListening(LocalIPAddress, 8888);
        }

        private void OnHololensMessageReceived(Object sender, String message)
        {
            Device.BeginInvokeOnMainThread(() => _pageDialogService.DisplayAlertAsync("From HoloLens", message, "OK"));
        }

        private void OnHololensClientConnected(object sender, EventArgs e)
        {
            Status = HololensConnectionStatus.Paired;
        }

        private HololensConnectionStatus _status;
        private IIPAddressManager _ipAddressManager;
        private IPageDialogService _pageDialogService;
        private IHololensCommunicationManager _hololensCommunicationManager;
    }
}
