using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace AG.Wpf.NavigationService.UserControlNav
{
    public class ContentNavigationService : ObservableObject, IContentNavigationService
    {
        #region Variables
        private readonly Func<ContentControl> CONTENT_GETTER;
        private readonly Dictionary<string, Type> viewsByKey = new Dictionary<string, Type>();
        private readonly Stack<ViewPair> backStack = new Stack<ViewPair>();
        private readonly Stack<ViewPair> forwardStack = new Stack<ViewPair>();
        private ContentControl targetContent;

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
        public ContentNavigationService(Func<ContentControl> contentGetter)
        {
            CONTENT_GETTER = contentGetter;
        }
        #endregion

        #region Private methods
        private ContentControl GetTargetContent()
        {
            if (targetContent == null)
                targetContent = CONTENT_GETTER();
            return targetContent;
        }

        private void PushCurrentViewToStack(NavigationDirection navDirection)
        {
            if(String.IsNullOrEmpty(CurrentPageKey) == false)
            {
                var currentTuple = new ViewPair(CurrentPageKey, ViewParameter);
                switch (navDirection)
                {
                    case NavigationDirection.Back:
                        forwardStack.Push(currentTuple);
                        break;
                    case NavigationDirection.Next:
                        forwardStack.Clear();
                        goto case NavigationDirection.Forward;
                    case NavigationDirection.Forward:
                        backStack.Push(currentTuple);
                        break;
                }
            }
        }

        private void GetViewFromStack(NavigationDirection navDirection)
        {
            ViewPair nextView = null;
            if (navDirection == NavigationDirection.Back)
                nextView = backStack.Pop();
            else if (navDirection == NavigationDirection.Forward)
                nextView = forwardStack.Pop();

            if (nextView != null)
                NavigateTo(nextView.ViewKey, nextView.ViewParameter, navDirection);
        }

        private void NavigateTo(string pageKey, object parameter, NavigationDirection navDirection)
        {
            lock (viewsByKey)
            {
                PushCurrentViewToStack(navDirection);
                if (viewsByKey.ContainsKey(pageKey) == false)
                    throw new ArgumentException($"No such page: {pageKey}. Did you forget to call the Configure method?", nameof(pageKey));
                var type = viewsByKey[pageKey];
                var view = type.GetConstructor(Type.EmptyTypes).Invoke(null);
                GetTargetContent().Content = view;
                CurrentPageKey = pageKey;
                ViewParameter = parameter;
            }
        }
        #endregion

        #region Public methods
        public bool CanGoBack()
        {
            return backStack.Any();
        }

        public void GoBack()
        {
            GetViewFromStack(NavigationDirection.Back);
        }

        public bool CanGoForward()
        {
            return forwardStack.Any();
        }

        public void GoForward()
        {
            GetViewFromStack(NavigationDirection.Forward);
        }

        public void NavigateTo(string pageKey)
        {
            NavigateTo(pageKey, null, NavigationDirection.Next);
        }

        public void NavigateTo(string pageKey, object parameter)
        {
            NavigateTo(pageKey, parameter, NavigationDirection.Next);
        }

        public void ConfigureView<T>(string key) where T : UserControl
        {
            lock (viewsByKey)
            {
                if (viewsByKey.ContainsKey(key) == true)
                    throw new ArgumentException($"This key has already been used: \"{key}\"", nameof(key));
                if (viewsByKey.ContainsValue(typeof(T)) == true)
                    throw new ArgumentException($"This type has already been configured with key \"{viewsByKey.First(t => t.Value == typeof(T)).Key}\"", nameof(T));
                viewsByKey.Add(key, typeof(T));
            }
        }
        #endregion

    }
}
