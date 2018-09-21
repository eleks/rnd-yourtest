using System;
using System.Windows.Input;
using Prism.Commands;
using Prism.Services;
using Xamarin.Forms;
using YourTest.Manager;
using YourTest.Models;
using System.Linq;

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

        public Boolean Fan { get => _fan; set => SetProperty(ref _fan, value); }
        private bool _fan;

        public Boolean FuelHose { get => _fuelHose; set => SetProperty(ref _fuelHose, value); }
        private bool _fuelHose;

        public Boolean MainPipes { get => _mainPipes; set => SetProperty(ref _mainPipes, value); }
        private bool _mainPipes;

        public Boolean RearPipes { get => _rearPipes; set => SetProperty(ref _rearPipes, value); }
        private bool _rearPipes;

        public Boolean Dynamos { get => _dynamos; set => SetProperty(ref _dynamos, value); }
        private bool _dynamos;

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
            //Device.BeginInvokeOnMainThread(() => _pageDialogService.DisplayAlertAsync("From HoloLens", message, "OK"));
            //Answer = answer;
            //HasAnswer = true;
            //AnswerSelectedCommand?.Execute(answer);

            var element = answer.Split(':').FirstOrDefault();
            var position = Boolean.Parse(answer.Split(':').LastOrDefault());

            switch (element)
            {
                case "FAN":
                    Fan = position;
                    break;
                case "main_pipes":
                    MainPipes = position;
                    break;
                case "Pipes_rear":
                    RearPipes = position;
                    break;
                case "fuel_hose":
                    FuelHose = position;
                    break;
                case "dynamos":
                    Dynamos = position;
                    break;
                default:
                    break;
            }
        }

        private void OnHololensClientConnected(object sender, EventArgs e) => Status = HololensConnectionStatus.Paired;

        private HololensConnectionStatus _status;
        private IIPAddressManager _ipAddressManager;
        private IPageDialogService _pageDialogService;
        private IHololensCommunicationManager _hololensCommunicationManager;
        private bool _hasAnswer;
    }
}
