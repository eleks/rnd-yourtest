using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YourTest.ViewModels.ActiveTest;

namespace YourTest.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TextQuestionPageView : ContentView
    {
        public TextQuestionPageView()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty AnswerSelectedCommandProperty =
            BindableProperty.Create(
                nameof(AnswerSelectedCommand),
                typeof(ICommand),
                typeof(TextQuestionPageView),
                default(ICommand));

        public ICommand AnswerSelectedCommand
        {
            get => (ICommand)GetValue(AnswerSelectedCommandProperty);
            set => SetValue(AnswerSelectedCommandProperty, value);
        }

        void Handle_Tapped(object sender, System.EventArgs e)
        {
            var frame = (Frame)sender;
            var answer = frame.BindingContext as String;
            var vm = (BaseQuestionViewModel)BindingContext;
            if (vm == null)
            {
                return;
            }
            vm.Answer = answer;
            AnswerSelectedCommand.Execute(answer);
        }

    }
}
