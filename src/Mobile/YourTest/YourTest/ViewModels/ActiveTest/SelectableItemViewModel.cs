using System;
namespace YourTest.ViewModels.ActiveTest
{
    public class SelectableItemViewModel : ViewModelBase
    {
        private String _text;
        public String Text
        {
            get => _text;
            set => SetProperty(ref _text, value);
        }

        private Boolean _isSelected;
        public Boolean IsSelected
        {
            get => _isSelected;
            set => SetProperty(ref _isSelected, value);
        }
    }
}
