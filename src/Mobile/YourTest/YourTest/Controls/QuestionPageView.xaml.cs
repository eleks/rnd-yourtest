using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace YourTest.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QuestionPageView : Grid
    {
        public QuestionPageView()
        {
            InitializeComponent();
        }
    }
}
