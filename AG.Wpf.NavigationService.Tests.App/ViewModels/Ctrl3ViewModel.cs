using AG.Wpf.NavigationService.UserControlNav;
using AG.Wpf.NavigationService.WindowNav;

namespace AG.Wpf.NavigationService.Tests.App.ViewModels
{
    public class Ctrl3ViewModel : View3ViewModelBase
    {
        private readonly IContentNavigationService navService;

        public Ctrl3ViewModel(IContentNavigationService cns, IWindowNavigationService wns)
            : base(wns)
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
        
        protected override void BackExecuted()
        {
            navService.GoBack();
        }

        protected override void ForwardExecuted()
        {
            navService.GoForward();
        }
    }
}
