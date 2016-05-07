using AG.Wpf.NavigationService.Tests.App.Data;
using AG.Wpf.NavigationService.WindowNav;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;

namespace AG.Wpf.NavigationService.Tests.App.ViewModels
{
    public class View1ViewModel : ViewModelBase
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
        public RelayCommand OpenDialogCommand { get; private set; }
        public RelayCommand OpenWindowCommand { get; private set; }
        #endregion

        #region Constructors
        public View1ViewModel(IDataService d, INavigationService v, IWindowNavigationService w)
        {
            viewNavService = v;
            windowNavService = w;

            this.Name = d.GetName();

            LoadedCommand = new RelayCommand(LoadedExecuted);
            NextCommand = new RelayCommand(NextExecuted, NextCanExecute);
            BackCommand = new RelayCommand(NextExecuted, NextCanExecute);
            ForwardCommand = new RelayCommand(ForwardExecuted, ForwardCanExecute);
            OpenDialogCommand = new RelayCommand(OpenDialogExecuted);
            OpenWindowCommand = new RelayCommand(OpenWindowExecuted);
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
            viewNavService.NavigateTo(typeof(View2ViewModel).Name, Name);
        }

        private void BackExecuted()
        {
            viewNavService.GoBack();
        }

        private void ForwardExecuted()
        {
            viewNavService.GoForward();
        }

        private void OpenDialogExecuted()
        {
            windowNavService.OpenWindow("window", false, true);
        }

        private void OpenWindowExecuted()
        {
            windowNavService.OpenWindow("window", false, false);
        }
        #endregion
    }
}
