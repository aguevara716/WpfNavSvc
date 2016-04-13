﻿using AG.Wpf.NavigationService.Tests.App.Data;
using System;

namespace AG.Wpf.NavigationService.Tests.App.ViewModels
{
    public class Page1ViewModel : View1ViewModelBase
    {
        #region Variables
        private readonly IFrameNavigationService navService;
        #endregion

        #region Binding variables
        #endregion

        #region Commands
        #endregion

        #region Constructors
        public Page1ViewModel(IDataService d, IFrameNavigationService fns)
            : base(d)
        public Page1ViewModel(IDataService d, IFrameNavigationService fns, IWindowNavigationService wns)
            : base(d, wns)
        {
            navService = fns;
            //BackCommand = new RelayCommand(BackExecuted, BackCanExecute);
            //ForwardCommand = new RelayCommand(ForwardExecuted, ForwardCanExecute);
            //NextCommand = new RelayCommand(NextExecuted, NextCanExecute);
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

        protected override bool NextCanExecute()
        {
            return String.IsNullOrEmpty(Name) == false;
        }
        #endregion

        #region Commands Executed
        protected override void LoadedExecuted()
        {
            Name = navService.ViewParameter as String;
        }

        protected override void BackExecuted()
        {
            navService.GoBack();
        }

        protected override void ForwardExecuted()
        {
            navService.GoForward();
        }

        protected override void NextExecuted()
        {
            navService.NavigateTo(typeof(Page2ViewModel).Name, Name);
        }
        #endregion
    }
}
