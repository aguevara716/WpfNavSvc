namespace AG.Wpf.NavigationService.Tests.App.ViewModels
{
    public class Page3ViewModel : View3ViewModelBase
    {
        #region Variables
        private readonly IFrameNavigationService navService;
        #endregion

        #region Binding variables
        #endregion

        #region Commands
        
        #endregion

        #region Constructors
        public Page3ViewModel(IFrameNavigationService fns)
            : base()
        {
            navService = fns;
            //BackCommand = new RelayCommand(BackExecuted, BackCanExecute);
            //ForwardCommand = new RelayCommand(ForwardExecuted, ForwardCanExecute);
        }
        #endregion

        #region Commands CanExecute
        protected override bool BackCanExecute()
        {
            return navService.CanGoBack();
        }

        protected override bool ForwardCanExecute()
        {
            return navService.CanGoForward();
        }
        #endregion

        #region Commands Executed
        protected override void BackExecuted()
        {
            navService.GoBack();
        }

        protected override void ForwardExecuted()
        {
            navService.GoForward();
        }
        #endregion
    }
}
