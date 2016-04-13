namespace AG.Wpf.NavigationService.Tests.App.ViewModels
{
    public class Page3ViewModel : View3ViewModelBase
    {
        private readonly IFrameNavigationService navService;

        public Page3ViewModel(IFrameNavigationService fns, IWindowNavigationService wns)
            : base(wns)
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
