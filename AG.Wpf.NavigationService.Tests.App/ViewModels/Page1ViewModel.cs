using AG.Wpf.NavigationService.FrameNav;
using AG.Wpf.NavigationService.Tests.App.Data;
using AG.Wpf.NavigationService.WindowNav;
using System;

namespace AG.Wpf.NavigationService.Tests.App.ViewModels
{
    public class Page1ViewModel : View1ViewModelBase
    {
        private readonly IFrameNavigationService navService;

        public Page1ViewModel(IDataService d, IFrameNavigationService fns, IWindowNavigationService wns)
            : base(d, wns)
        {
            navService = fns;
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
            navService.NavigateTo(typeof(Page2ViewModel).Name, Name);
        }
    }
}
