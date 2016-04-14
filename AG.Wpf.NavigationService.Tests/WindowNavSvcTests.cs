using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AG.Wpf.NavigationService.WindowNav;

namespace AG.Wpf.NavigationService.Tests
{
    [TestClass]
    public class WindowNavSvcTests
    {
        private WindowNavigationService navSvc;

        [TestInitialize]
        public void BeforeEach()
        {
            navSvc = new WindowNavigationService();
        }

        [TestMethod]
        public void TestConfiguringWindowDuplicateKeys()
        {
            try
            {
                navSvc.ConfigureWindow(typeof(Window1).Name, () => new Window1());
                navSvc.ConfigureWindow(typeof(Window1).Name, () => new Window2());
                Assert.Fail("The exception wasn't thrown");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        [TestMethod]
        [Ignore]
        public void TestConfiguringWindowDuplicateViews()
        {
            try
            {
                navSvc.ConfigureWindow(typeof(Window1).Name, () => new Window1());
                navSvc.ConfigureWindow(typeof(Window1).Name + "-1", () => new Window1());
                Assert.Fail("The exception wasn't thrown");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private class Window1 : System.Windows.Window { }
        private class Window2 : System.Windows.Window { }
        private class Window3 : System.Windows.Window { }
    }
}
