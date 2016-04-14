using AG.Wpf.NavigationService.Tests.App.Data;
using AG.Wpf.NavigationService.WindowNav;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace AG.Wpf.NavigationService.Tests.App.ViewModels
{
    public abstract class View1ViewModelBase : ViewModelBase
    {
        #region Variables
        protected readonly IWindowNavigationService windowNavService;
        #endregion

        #region Binding variables
        protected string name;
        public string Name
        {
            get { return name; }
            set { Set(nameof(Name), ref name, value); }
        }
        #endregion

        #region Commands
        public RelayCommand LoadedCommand { get; protected set; }
        public RelayCommand NextCommand { get; protected set; }
        public RelayCommand BackCommand { get; protected set; }
        public RelayCommand ForwardCommand { get; protected set; }
        public RelayCommand DialogCommand { get; protected set; }
        public RelayCommand WindowCommand { get; protected set; }
        #endregion

        #region Constructors
        public View1ViewModelBase(IDataService data, IWindowNavigationService wns)
        {
            windowNavService = wns;
            this.Name = data.GetName();
            LoadedCommand = new RelayCommand(LoadedExecuted);
            BackCommand = new RelayCommand(BackExecuted, BackCanExecute);
            ForwardCommand = new RelayCommand(ForwardExecuted, ForwardCanExecute);
            NextCommand = new RelayCommand(NextExecuted, NextCanExecute);
            DialogCommand = new RelayCommand(DialogExecuted);
            WindowCommand = new RelayCommand(WindowExecuted);
        }
        #endregion

        #region Commands CanExecute
        protected abstract bool NextCanExecute();
        protected abstract bool BackCanExecute();
        protected abstract bool ForwardCanExecute();
        #endregion

        #region Commands Executed
        protected abstract void LoadedExecuted();
        protected abstract void NextExecuted();
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
