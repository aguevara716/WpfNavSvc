using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Threading.Tasks;

namespace AG.Wpf.NavigationService.Tests.App.ViewModels
{
    public abstract class View3ViewModelBase : ViewModelBase
    {
        #region Variables
        private bool continueLooping;
        protected readonly IWindowNavigationService windowNavService;
        #endregion

        #region Binding variables
        protected DateTime currentTime;
        public DateTime CurrentTime
        {
            get { return currentTime; }
            set { Set(nameof(CurrentTime), ref currentTime, value); }
        }
        #endregion

        #region Commands
        public RelayCommand LoadedCommand { get; protected set; }
        public RelayCommand UnloadedCommand { get; protected set; }
        public RelayCommand BackCommand { get; protected set; }
        public RelayCommand ForwardCommand { get; protected set; }
        public RelayCommand DialogCommand { get; protected set; }
        public RelayCommand WindowCommand { get; protected set; }
        #endregion

        #region Constructors
        public View3ViewModelBase(IWindowNavigationService wns)
        {
            windowNavService = wns;
            CurrentTime = DateTime.Now;

            LoadedCommand = new RelayCommand(LoadedExecuted);
            UnloadedCommand = new RelayCommand(UnloadedExecuted);
            ForwardCommand = new RelayCommand(ForwardExecuted, ForwardCanExecute);
            BackCommand = new RelayCommand(BackExecuted, BackCanExecute);
            DialogCommand = new RelayCommand(DialogExecuted);
            WindowCommand = new RelayCommand(WindowExecuted);
        }
        #endregion

        #region Private methods
        #endregion

        #region Commands CanExecute
        protected abstract bool BackCanExecute();
        protected abstract bool ForwardCanExecute();
        #endregion

        #region Commands Executed
        protected async void LoadedExecuted()
        {
            continueLooping = true;
            while (continueLooping == true)
            {
                CurrentTime = DateTime.Now;
                await Task.Delay(1000);
            }
        }

        protected void UnloadedExecuted()
        {
            continueLooping = false;
        }

        protected abstract void BackExecuted();
        protected abstract void ForwardExecuted();
        protected void DialogExecuted()
        {
            windowNavService.OpenWindow("window", false, true);
        }

        protected void WindowExecuted()
        {
            windowNavService.OpenWindow("window", false, false);
        }
        #endregion
    }
}