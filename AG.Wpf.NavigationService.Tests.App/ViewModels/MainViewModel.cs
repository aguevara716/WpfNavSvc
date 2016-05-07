using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace AG.Wpf.NavigationService.Tests.App.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        #region Binding variables
        public INavigationService ViewNavService { get; private set; }
        #endregion

        #region Commands
        public RelayCommand LoadedCommand { get; private set; }
        #endregion

        #region Constructors
        public MainViewModel(INavigationService v)
        {
            ViewNavService = v;
            LoadedCommand = new RelayCommand(LoadedExecuted);
        }
        #endregion

        #region Commands Executed
        private void LoadedExecuted()
        {
            ViewNavService.NavigateTo(typeof(View1ViewModel).Name);
        }
        #endregion

    }
}