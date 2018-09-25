using System;
using System.Windows.Input;
using Prism.Commands;
using Prism.Services;
using Xamarin.Forms;
using YourTest.Manager;
using YourTest.Models;
using System.Linq;
using System.Collections.Generic;
using MvvmHelpers;
using Xamarin.Forms.Internals;
using System.Diagnostics;

namespace YourTest.ViewModels.ActiveTest
{
    public class MixedRealityQuestionViewModel : BaseQuestionViewModel
    {
        private const int Port = 8888;

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

            _hololensCommunicationManager.StartListening(LocalIPAddress, Port);
        }

        public HololensConnectionStatus Status
        {
            get => _status;
            set => SetProperty(ref _status, value);
        }

        public ObservableRangeCollection<SelectableItemViewModel> TestSteps { get; } = new ObservableRangeCollection<SelectableItemViewModel>();

        public String LocalIPAddress => _ipAddressManager.GetIPAddress();

        public ICommand ResetCommand { get; set; }
        public ICommand AnswerSelectedCommand { get; set; }

        public override IList<string> PossibleAnswers
        {
            get => base.PossibleAnswers;
            set
            {
                base.PossibleAnswers = value;
                CreateItemViewModels();
            }
        }

        public Boolean HasAnswer
        {
            get => _hasAnswer;
            private set => SetProperty(ref _hasAnswer, value);
        }

        private void CreateItemViewModels()
        {
            TestSteps.ReplaceRange(PossibleAnswers.Select(x => new SelectableItemViewModel { Text = x }));
        }

        private void OnReset()
        {
            Status = HololensConnectionStatus.WaitingForPairing;

            _hololensCommunicationManager.StopListening();
            _hololensCommunicationManager.StartListening(LocalIPAddress, Port);
        }

        private void OnHololensMessageReceived(Object sender, String message)
        {
            message = message.TrimEnd('\n', '\r');


            var testFinished = String.Empty;

            if (message == testFinished)
            {
                Answer = String.Join(";", TestSteps.Select(st => new MixedStatus(st.Text, st.IsSelected).ToString()));
                HasAnswer = true;
                AnswerSelectedCommand?.Execute(Answer);
                _hololensCommunicationManager.StopListening();

                return;
            }

            try
            {
                var update = MixedStatus.Parse(message);

                TestSteps
                    .First(x => x.Text == update.Element)
                    .IsSelected = update.InPlace;
            }
            catch
            {
                Debug.WriteLine($"Failed to parse MixedStatus: {message}");
            }

        }

        private void OnHololensClientConnected(object sender, EventArgs e) => Status = HololensConnectionStatus.Paired;

        private HololensConnectionStatus _status;
        private IIPAddressManager _ipAddressManager;
        private IPageDialogService _pageDialogService;
        private IHololensCommunicationManager _hololensCommunicationManager;
        private bool _hasAnswer;


        private struct MixedStatus
        {
            private const char Separator = ':';

            public String Element { get; private set; }
            public Boolean InPlace { get; private set; }

            public static MixedStatus Parse(String val)
            {
                var parts = val.Split(Separator);
                var element = parts[0];
                var inPlace = Boolean.Parse(parts[1]);

                return new MixedStatus(element, inPlace);
            }

            public MixedStatus(String element, Boolean inPlace)
            {
                Element = element;
                InPlace = inPlace;
            }

            public override string ToString()
            {
                return $"{Element}{Separator}{InPlace.ToString()}";
            }
        }
    }
}
