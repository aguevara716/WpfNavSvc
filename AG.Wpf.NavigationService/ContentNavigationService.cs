﻿using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace AG.Wpf.NavigationService
{
    public class ContentNavigationService : ObservableObject, IContentNavigationService
    {
        #region Variables
        private readonly Func<ContentControl> CONTENT_GETTER;
        private readonly Dictionary<string, Func<UserControl>> viewsByKey = new Dictionary<string, Func<UserControl>>();
        private readonly Dictionary<string, Func<Window>> windowsByKey = new Dictionary<string, Func<Window>>();
        private readonly Stack<Tuple<string, object>> backStack = new Stack<Tuple<string, object>>();
        private readonly Stack<Tuple<string, object>> forwardStack = new Stack<Tuple<string, object>>();
        private ContentControl targetContent;

        public object ViewParameter { get; private set; }
        public object WindowParameter { get; private set; }
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

        private void UpdatePageKey()
        {
            CurrentPageKey = (GetTargetContent().Content as UserControl).DataContext.GetType().Name;
        }

        private void PushCurrentViewToStack(NavigationType navType)
        {
            if(String.IsNullOrEmpty(CurrentPageKey) == false)
            {
                var currentTuple = new Tuple<string, object>(CurrentPageKey, ViewParameter);
                switch (navType)
                {
                    case NavigationType.Back:
                        forwardStack.Push(currentTuple);
                        break;
                    case NavigationType.Move:
                    case NavigationType.Forward:
                        backStack.Push(currentTuple);
                        break;
                }
            }
        }

        private void GetViewFromStack(NavigationType navType)
        {
            Tuple<string, object> nextView = null;
            if (navType == NavigationType.Back)
                nextView = backStack.Pop();
            else if (navType == NavigationType.Forward)
                nextView = forwardStack.Pop();

            if (nextView != null)
                NavigateTo(nextView.Item1, nextView.Item2, navType);
        }

        private void NavigateTo(string pageKey, object parameter, NavigationType navType)
        {
            lock(viewsByKey)
            {
                PushCurrentViewToStack(navType);
                if (viewsByKey.ContainsKey(pageKey) == false)
                    throw new ArgumentException($"No such page: {pageKey}. Did you forget to call the Configure method?", nameof(pageKey));
                GetTargetContent().Content = viewsByKey[pageKey].Invoke();
                UpdatePageKey();
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
            GetViewFromStack(NavigationType.Back);
        }

        public bool CanGoForward()
        {
            return forwardStack.Any();
        }

        public void GoForward()
        {
            GetViewFromStack(NavigationType.Forward);
        }

        public void NavigateTo(string pageKey)
        {
            NavigateTo(pageKey, null, NavigationType.Move);
        }

        public void NavigateTo(string pageKey, object parameter)
        {
            NavigateTo(pageKey, parameter, NavigationType.Move);
        }

        public void OpenWindow(string key, bool isTopMost, bool isDialog)
        {
            OpenWindow(key, null, isTopMost, isDialog);
        }

        public void OpenWindow(string key, object parameter, bool isTopMost, bool isDialog)
        {
            lock (windowsByKey)
            {
                if (windowsByKey.ContainsKey(key) == false)
                    throw new ArgumentException($"No such window: {key}. Did you forget to call the Configure method?", nameof(key));
                WindowParameter = parameter;
                var window = windowsByKey[key].Invoke();
                window.Topmost = isTopMost;
                //TODO figure out how to get the owner 
                if (isDialog == true)
                    window.ShowDialog();
                else
                    window.Show();
            }
        }

        public void ConfigureView(string key, Func<UserControl> ctor)
        {
            lock (viewsByKey)
            {
                if (viewsByKey.ContainsKey(key) == true)
                    throw new ArgumentException($"This key has already been used: {key}", nameof(key));
                if (viewsByKey.Any(v => v.Value == ctor) == true)
                    throw new ArgumentException($"This type has already been configured with key {viewsByKey.First(v => v.Value == ctor).Key}", nameof(ctor));
                viewsByKey.Add(key, ctor);
            }
        }

        public void ConfigureWindow(string key, Func<Window> ctor)
        {
            lock (windowsByKey)
            {
                if (windowsByKey.ContainsKey(key) == true)
                    throw new ArgumentException($"This key has already been used: {key}", nameof(key));
                if (windowsByKey.Any(w => w.Value == ctor) == true)
                    throw new ArgumentException($"This type has already been configured with key {windowsByKey.First(v => v.Value == ctor).Key}", nameof(ctor));
                windowsByKey.Add(key, ctor);
            }
        }
        #endregion

    }
}
