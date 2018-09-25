using System;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YourTest.ViewModels.ActiveTest;

namespace YourTest.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MixedRealityQuestionPageView : ContentView
    {
        public MixedRealityQuestionPageView()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty AnswerSelectedCommandProperty =
            BindableProperty.Create(
                nameof(AnswerSelectedCommand),
                typeof(ICommand),
                typeof(MixedRealityQuestionPageView),
                default(ICommand),
                propertyChanged: AnswerSelectedCommandChanged);

        public static readonly BindableProperty BarcodeValueProperty =
            BindableProperty.Create(
                nameof(BarcodeValue),
                typeof(String),
                typeof(MixedRealityQuestionPageView),
                default(String), propertyChanged: HandleBindingPropertyChangedDelegate);

        public ICommand AnswerSelectedCommand
        {
            get => (ICommand)GetValue(AnswerSelectedCommandProperty);
            set => SetValue(AnswerSelectedCommandProperty, value);
        }

        public String BarcodeValue
        {
            get => (String)GetValue(BarcodeValueProperty);
            set => SetValue(BarcodeValueProperty, value);
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            var vm = (MixedRealityQuestionViewModel)BindingContext;
            if (vm == null)
            {
                return;
            }
            vm.AnswerSelectedCommand = AnswerSelectedCommand;
        }

        private static void HandleBindingPropertyChangedDelegate(BindableObject bindable, object oldValue, object newValue)
        {
            ((MixedRealityQuestionPageView)bindable).barcodeImageView.BarcodeValue = (String)newValue;
        }


        private static void AnswerSelectedCommandChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var vm = (MixedRealityQuestionViewModel)((MixedRealityQuestionPageView)bindable).BindingContext;
            if (vm == null)
            {
                return;
            }
            vm.AnswerSelectedCommand = (ICommand)newValue;
        }

    }
}
