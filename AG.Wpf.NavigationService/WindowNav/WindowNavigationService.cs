using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace AG.Wpf.NavigationService.WindowNav
{
    public class WindowNavigationService : IWindowNavigationService
    {
        private readonly Func<Window> MAIN_WINDOW_GETTER;
        private readonly Dictionary<string, Func<Window>> windowsByKey = new Dictionary<string, Func<Window>>();
        private Window mainWindow;
        public object WindowParameter { get; private set; }

        public WindowNavigationService(Func<Window> mainWindowGetter = null)
        {
            MAIN_WINDOW_GETTER = mainWindowGetter;
        }

        private Window GetMainWindow()
        {
            if (mainWindow == null && MAIN_WINDOW_GETTER != null)
                mainWindow = MAIN_WINDOW_GETTER();
            return mainWindow;
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
                window.Owner = GetMainWindow();
                if (isDialog == true)
                    window.ShowDialog();
                else
                    window.Show();
            }
        }

        public void ConfigureWindow(string key, Func<Window> ctor)
        {
            lock (windowsByKey)
            {
                if (windowsByKey.ContainsKey(key) == true)
                    throw new ArgumentException($"This key has already been used: {key}", nameof(key));
                if (windowsByKey.Any(w => w.Value == ctor) == true)
                    throw new ArgumentException($"This type has already been configured with key \"{windowsByKey.First(v => v.Value == ctor).Key}\"", nameof(ctor));
                windowsByKey.Add(key, ctor);
            }
        }

    }
}
