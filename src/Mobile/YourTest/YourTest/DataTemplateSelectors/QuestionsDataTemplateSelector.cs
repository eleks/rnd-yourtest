using System;
using Xamarin.Forms;
using YourTest.ViewModels.ActiveTest;

namespace YourTest.DataTemplateSelectors
{
    public class QuestionsDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate MixedRealityQuestionTemplate { get; set; }
        public DataTemplate TextQuestionTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item is MixedRealityQuestionViewModel)
            {
                return MixedRealityQuestionTemplate;
            }
            if (item is TextQuestionViewModel)
            {
                return TextQuestionTemplate;
            }

            return null;
        }
    }
}
