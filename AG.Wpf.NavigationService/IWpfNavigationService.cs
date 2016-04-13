﻿using GalaSoft.MvvmLight.Views;

namespace AG.Wpf.NavigationService
{
    /// <summary>
    /// An interface defining how opening Windows and passing parameters to them
    /// should be performed in WPF following the MVVM design pattern.
    /// </summary>
    public interface IWpfNavigationService : INavigationService
    {
        object WindowParameter { get; }
        
        void OpenWindow(string key, bool isTopMost, bool isDialog);
        void OpenWindow(string key, object parameter, bool isTopMost, bool isDialog);
    }
}
