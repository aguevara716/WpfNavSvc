using AG.Wpf.NavigationService.Tests.App.Data;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace AG.Wpf.NavigationService.Tests.App.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        #region Variables
        private readonly IDataService dataService;
        #endregion

        #region Binding variables
        public IFrameNavigationService FrameNav { get; private set; }
        public IContentNavigationService ContentNav { get; private set; }
        #endregion

        #region Commands
        public RelayCommand LoadedCommand { get; private set; }
        #endregion

        #region Constructors
        public MainViewModel(IDataService d, IFrameNavigationService f, IContentNavigationService c)
        {
            dataService = d;
            FrameNav = f;
            ContentNav = c;

            LoadedCommand = new RelayCommand(LoadedExecuted);
        }
        #endregion

        #region Commands Executed
        private void LoadedExecuted()
        {
            FrameNav.NavigateTo(typeof(Page1ViewModel).Name);
            ContentNav.NavigateTo(typeof(Ctrl1ViewModel).Name);
        }
        #endregion

    }
}