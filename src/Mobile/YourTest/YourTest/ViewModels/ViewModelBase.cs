using System;
using Prism.Mvvm;
using Prism.AppModel;
using Prism.Navigation;

namespace YourTest.ViewModels
{
    public abstract class ViewModelBase : BindableBase, IPageLifecycleAware, INavigatingAware
    {

        private bool _isBusy;
        public Boolean IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        void IPageLifecycleAware.OnAppearing() => OnAppearing();
        void IPageLifecycleAware.OnDisappearing() => OnDisappearing();

        void INavigatingAware.OnNavigatingTo(NavigationParameters parameters) => OnNavigatingTo(parameters);

        protected virtual void OnAppearing() { }
        protected virtual void OnDisappearing() { }

        protected virtual void OnNavigatingTo(NavigationParameters parameters) { }

    }
}

