using AG.Wpf.NavigationService.WindowNav;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Threading.Tasks;

namespace AG.Wpf.NavigationService.Tests.App.ViewModels
{
    public class View3ViewModel : ViewModelBase
    {
        #region Variables
        private bool continueLooping;
        private readonly INavigationService viewNavService;
        private readonly IWindowNavigationService windowNavService;
        #endregion

        #region Binding variables
        private DateTime currentTime;
        public DateTime CurrentTime
        {
            get { return currentTime; }
            set { Set(nameof(CurrentTime), ref currentTime, value); }
        }
        #endregion

        #region Commands
        public RelayCommand LoadedCommand { get; private set; }
        public RelayCommand UnloadedCommand { get; private set; }
        public RelayCommand BackCommand { get; private set; }
        public RelayCommand ForwardCommand { get; private set; }
        public RelayCommand DialogCommand { get; private set; }
        public RelayCommand WindowCommand { get; private set; }
        #endregion

        #region Constructors
        public View3ViewModel(INavigationService v, IWindowNavigationService w)
        {
            this.viewNavService = v;
            this.windowNavService = w;

            CurrentTime = DateTime.Now;

            LoadedCommand = new RelayCommand(LoadedExecuted);
            UnloadedCommand = new RelayCommand(UnloadedExecuted);
            ForwardCommand = new RelayCommand(ForwardExecuted, ForwardCanExecute);
            BackCommand = new RelayCommand(BackExecuted, BackCanExecute);
            DialogCommand = new RelayCommand(DialogExecuted);
            WindowCommand = new RelayCommand(WindowExecuted);
        }
        #endregion

        #region Commands CanExecute
        private bool BackCanExecute()
        {
            return viewNavService.CanGoBack();
        }

        private bool ForwardCanExecute()
        {
            return viewNavService.CanGoForward();
        }
        #endregion

        #region Commands Executed
        private async void LoadedExecuted()
        {
            continueLooping = true;
            while (continueLooping == true)
            {
                CurrentTime = DateTime.Now;
                await Task.Delay(1000);
            }
        }

        private void UnloadedExecuted()
        {
            continueLooping = false;
        }

        private void BackExecuted()
        {
            viewNavService.GoBack();
        }

        private void ForwardExecuted()
        {
            viewNavService.GoForward();
        }
        private void DialogExecuted()
        {
            windowNavService.OpenWindow("window", false, true);
        }

        private void WindowExecuted()
        {
            windowNavService.OpenWindow("window", false, false);
        }
        #endregion
    }
}
