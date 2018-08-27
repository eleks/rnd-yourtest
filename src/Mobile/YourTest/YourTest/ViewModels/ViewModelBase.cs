using System;
using Prism.Mvvm;
using Prism.AppModel;

namespace YourTest.ViewModels
{
    public abstract class ViewModelBase : BindableBase, IPageLifecycleAware
    {

        private bool _isBusy;
        public Boolean IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        void IPageLifecycleAware.OnAppearing() => OnAppearing();
        void IPageLifecycleAware.OnDisappearing() => OnDisappearing();

        protected virtual void OnAppearing() { }
        protected virtual void OnDisappearing() { }
    }
}

