﻿namespace AG.Wpf.NavigationService.FrameNav
{
    /// <summary>
    /// This is only meant to be used as a design-time navigation service.
    /// </summary>
    public class FrameDesignNavigationService : IFrameNavigationService
    {
        public string CurrentPageKey { get; set; }
        public object ViewParameter { get { return null; } }

        public bool CanGoBack() { return false; }
        public bool CanGoForward() { return false; }
        public void GoBack() { }

        public void GoForward() { }
        public void NavigateTo(string pageKey) { }
        public void NavigateTo(string pageKey, object parameter) { }
    }
}