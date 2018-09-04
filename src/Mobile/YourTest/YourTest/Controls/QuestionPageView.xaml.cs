using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace YourTest.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TextQuestionPageView : StackLayout
    {
        public TextQuestionPageView()
        {
            InitializeComponent();
        }

        void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            var answer = (String)e.SelectedItem;
            AnswerSelectedCommand?.Execute(answer);
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
    }
}
