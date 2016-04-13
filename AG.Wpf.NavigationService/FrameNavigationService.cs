using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace AG.Wpf.NavigationService
{
    public class FrameNavigationService : ObservableObject, IFrameNavigationService
    {
        #region Variables
        private readonly Func<Frame> FRAME_GETTER;
        private readonly Dictionary<string, Uri> pagesByKey = new Dictionary<string, Uri>();
        private Frame targetFrame;

        public object ViewParameter { get; private set; }
        #endregion

        #region Binding variables
        private string currentPageKey;
        public string CurrentPageKey
        {
            get { return currentPageKey; }
            set { Set(nameof(CurrentPageKey), ref currentPageKey, value); }
        }
        #endregion

        #region Constructors
        public FrameNavigationService(Func<Frame> frameGetter)
        {
            FRAME_GETTER = frameGetter;
        }
        #endregion

        #region Private methods
        private Frame GetTargetFrame()
        {
            if (targetFrame == null)
                targetFrame = FRAME_GETTER();
            return targetFrame;
        }

        private void UpdatePageKey()
        {
            CurrentPageKey = (GetTargetFrame().Content as Page).DataContext.GetType().Name;
        }
        #endregion

        #region Public methods
        public bool CanGoBack()
        {
            return GetTargetFrame().CanGoBack;
        }

        public void GoBack()
        {
            if (CanGoBack() == true)
                GetTargetFrame().GoBack();
        }

        public bool CanGoForward()
        {
            return GetTargetFrame().CanGoForward;
        }

        public void GoForward()
        {
            if (CanGoForward() == true)
                GetTargetFrame().GoForward();
        }

        public void NavigateTo(string pageKey)
        {
            NavigateTo(pageKey, null);
        }

        public void NavigateTo(string pageKey, object parameter)
        {
            lock (pagesByKey)
            {
                if (pagesByKey.ContainsKey(pageKey) == false)
                    throw new ArgumentException($"No such page: {pageKey}. Did you forget to call the Configure method?", nameof(pageKey));
                GetTargetFrame().Navigate(pagesByKey[pageKey]);
                CurrentPageKey = pageKey;
                ViewParameter = parameter;
            }
        }

        public void ConfigurePage(string key, string pageUri)
        {
            ConfigurePage(key, new Uri(pageUri, UriKind.Relative));
        }

        public void ConfigurePage(string key, Uri pageUri)
        {
            lock(pagesByKey)
            {
                if (pagesByKey.ContainsKey(key) == true)
                    throw new ArgumentException($"This key has already been used: {key}", nameof(key));
                if (pagesByKey.Any(p => p.Value == pageUri) == true)
                    throw new ArgumentException($"This type has already been configured with key {pagesByKey.First(p => p.Value == pageUri).Key}", nameof(pageUri));
                pagesByKey.Add(key, pageUri);
            }
        }
        #endregion
    }
}
