using AG.Wpf.NavigationService.Tests.App.Data;
using System;

namespace AG.Wpf.NavigationService.Tests.App.ViewModels
{
    public class Ctrl2ViewModel : View2ViewModelBase
    {
        private readonly IContentNavigationService navService;

        public Ctrl2ViewModel(IDataService d, IContentNavigationService cns, IWindowNavigationService wns)
            : base(d, wns)
        {
            navService = cns;
        }

        protected override bool BackCanExecute()
        {
            return navService.CanGoBack();
        }

        protected override bool ForwardCanExecute()
        {
            return navService.CanGoForward();
        }

        protected override bool NextCanExecute()
        {
            return String.IsNullOrEmpty(Name) == false;
        }

        protected override void LoadedExecuted()
        {
            Name = navService.ViewParameter as String;
        }

        protected override void BackExecuted()
        {
            navService.GoBack();
        }

        protected override void ForwardExecuted()
        {
            navService.GoForward();
        }

        protected override void NextExecuted()
        {
            navService.NavigateTo(typeof(Ctrl3ViewModel).Name, Name);
        }
    }
}
