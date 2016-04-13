using AG.Wpf.NavigationService.Tests.App.Data;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace AG.Wpf.NavigationService.Tests.App.ViewModels
{
    public abstract class View1ViewModelBase : ViewModelBase
    {
        #region Variables
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
        #endregion

        #region Constructors
        public View1ViewModelBase(IDataService data)
        {
            this.Name = data.GetName();
            LoadedCommand = new RelayCommand(LoadedExecuted);
            BackCommand = new RelayCommand(BackExecuted, BackCanExecute);
            ForwardCommand = new RelayCommand(ForwardExecuted, ForwardCanExecute);
            NextCommand = new RelayCommand(NextExecuted, NextCanExecute);
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
        #endregion
    }
}
