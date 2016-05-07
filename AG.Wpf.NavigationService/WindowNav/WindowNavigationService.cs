using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace AG.Wpf.NavigationService.WindowNav
{
    public class WindowNavigationService : IWindowNavigationService
    {
        #region Variables
        private readonly Func<Window> MAIN_WINDOW_GETTER;
        private readonly Dictionary<string, Type> windowsByKey = new Dictionary<string, Type>();
        private Window mainWindow;
        public object WindowParameter { get; private set; }
        #endregion

        #region Constructors
        public WindowNavigationService(Func<Window> mainWindowGetter = null)
        {
            MAIN_WINDOW_GETTER = mainWindowGetter;
        }
        #endregion

        #region Private methods
        private Window GetMainWindow()
        {
            if (mainWindow == null && MAIN_WINDOW_GETTER != null)
                mainWindow = MAIN_WINDOW_GETTER();
            return mainWindow;
        }
        #endregion

        #region Public methods
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
                var type = windowsByKey[key];
                var window = type.GetConstructor(Type.EmptyTypes).Invoke(null) as Window;
                window.Topmost = isTopMost;
                window.Owner = GetMainWindow();
                if (isDialog == true)
                    window.ShowDialog();
                else
                    window.Show();
            }
        }

        public void ConfigureWindow<T>(string key) where T : Window
        {
            lock (windowsByKey)
            {
                if (windowsByKey.ContainsKey(key) == true)
                    throw new ArgumentException($"This key has already been used: \"{key}\"", nameof(key));
                if (windowsByKey.ContainsValue(typeof(T)) == true)
                    throw new ArgumentException($"This type has already been configured with key \"{windowsByKey.First(t => t.Value == typeof(T)).Key}\"", nameof(T));
                windowsByKey.Add(key, typeof(T));
            }
        }
        #endregion

    }
}
