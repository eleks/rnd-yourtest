using System;
using Prism.Mvvm;

namespace YourTest.ViewModels
{
    public abstract class ViewModelBase : BindableBase
    {

        private bool _isBusy;
        public Boolean IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }
    }
}

