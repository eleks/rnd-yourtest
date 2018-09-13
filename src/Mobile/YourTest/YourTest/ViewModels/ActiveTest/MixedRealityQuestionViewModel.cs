using System;
using System.Windows.Input;
using Prism.Commands;
using Prism.Services;
using Xamarin.Forms;
using YourTest.Manager;
using YourTest.Models;

namespace YourTest.ViewModels.ActiveTest
{
    public class MixedRealityQuestionViewModel : BaseQuestionViewModel
    {

        public MixedRealityQuestionViewModel(IIPAddressManager ipAddressManager,
                                             IPageDialogService pageDialogService,
                                             IHololensCommunicationManager hololensCommunicationManager)
        {
            _ipAddressManager = ipAddressManager;
            _pageDialogService = pageDialogService;
            _hololensCommunicationManager = hololensCommunicationManager;

            ResetCommand = new DelegateCommand(OnReset);

            Status = HololensConnectionStatus.WaitingForPairing;

            _hololensCommunicationManager.MessageReceived += OnHololensMessageReceived;
            _hololensCommunicationManager.ClientConnected += OnHololensClientConnected;

            _hololensCommunicationManager.StartListening(LocalIPAddress, 8888);
        }

        public HololensConnectionStatus Status
        {
            get => _status;
            set => SetProperty(ref _status, value);
        }

        public Boolean HasAnswer
        {
            get => _hasAnswer;
            private set => SetProperty(ref _hasAnswer, value);
        }

        public String LocalIPAddress => _ipAddressManager.GetIPAddress();

        public ICommand ResetCommand { get; set; }
        public ICommand AnswerSelectedCommand { get; set; }

        private void OnReset()
        {
            Status = HololensConnectionStatus.WaitingForPairing;
            HasAnswer = false;

            _hololensCommunicationManager.StopListening();
            _hololensCommunicationManager.StartListening(LocalIPAddress, 8888);
        }

        private void OnHololensMessageReceived(Object sender, String message)
        {
            var answer = message.TrimEnd('\n', '\t');
            // todo for this case create decorator with call dialog on main thread
            // Device.BeginInvokeOnMainThread(() => _pageDialogService.DisplayAlertAsync("From HoloLens", message, "OK"));
            Answer = answer;
            HasAnswer = true;
            AnswerSelectedCommand?.Execute(answer);
        }

        private void OnHololensClientConnected(object sender, EventArgs e) => Status = HololensConnectionStatus.Paired;

        private HololensConnectionStatus _status;
        private IIPAddressManager _ipAddressManager;
        private IPageDialogService _pageDialogService;
        private IHololensCommunicationManager _hololensCommunicationManager;
        private bool _hasAnswer;
    }
}
