using AG.Wpf.NavigationService.Tests.App.Data;
using AG.Wpf.NavigationService.WindowNav;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;

namespace AG.Wpf.NavigationService.Tests.App.ViewModels
{
    public class View2ViewModel : ViewModelBase
    {
        #region Variables
        private readonly INavigationService viewNavService;
        private readonly IWindowNavigationService windowNavService;
        #endregion

        #region Binding variables
        private string name;
        public string Name
        {
            get { return name; }
            set { Set(nameof(Name), ref name, value); }
        }
        #endregion

        #region Commands
        public RelayCommand LoadedCommand { get; private set; }
        public RelayCommand NextCommand { get; private set; }
        public RelayCommand BackCommand { get; private set; }
        public RelayCommand ForwardCommand { get; private set; }
        public RelayCommand DialogCommand { get; private set; }
        public RelayCommand WindowCommand { get; private set; }
        #endregion

        #region Constructors
        public View2ViewModel(IDataService d, INavigationService v, IWindowNavigationService w)
        {
            viewNavService = v;
            windowNavService = w;

            this.Name = d.GetName();

            LoadedCommand = new RelayCommand(LoadedExecuted);
            BackCommand = new RelayCommand(BackExecuted, BackCanExecute);
            ForwardCommand = new RelayCommand(ForwardExecuted, ForwardCanExecute);
            NextCommand = new RelayCommand(NextExecuted, NextCanExecute);
            DialogCommand = new RelayCommand(DialogExecuted);
            WindowCommand = new RelayCommand(WindowExecuted);
        }
        #endregion

        #region Commands CanExecute
        private bool NextCanExecute()
        {
            return String.IsNullOrEmpty(Name) == false;
        }

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
        private void LoadedExecuted()
        {
            Name = viewNavService.ViewParameter as String;
        }

        private void NextExecuted()
        {
            viewNavService.NavigateTo(typeof(View3ViewModel).Name);
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
