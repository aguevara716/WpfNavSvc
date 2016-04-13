using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Controls;
using System.Collections.Generic;

namespace AG.Wpf.NavigationService.Tests
{
    [TestClass]
    public class FrameNavSvcTests
    {
        private Frame targetFrame;
        private FrameNavigationService navSvc;
        private readonly Dictionary<string, string> NAV_DICT = new Dictionary<string, string>()
        {
            { typeof(Page1ViewModel).Name, "Page1.xaml" },
            { typeof(Page2ViewModel).Name, "Page2.xaml" },
            { typeof(Page3ViewModel).Name, "Page3.xaml" },
        };

        [TestInitialize]
        public void BeforeEach()
        {
            targetFrame = new Frame();
            navSvc = new FrameNavigationService(() => { return targetFrame; });

            foreach (var set in NAV_DICT)
            {
                navSvc.ConfigurePage(set.Key, set.Value);
            }
        }

        #region ConfigurePage Tests
        [TestMethod]
        public void TestConfiguringDuplicateKeys()
        {
            try
            {
                navSvc.ConfigurePage(typeof(Page1ViewModel).Name, "Page1.xaml");
                Assert.Fail("The exception wasn't thrown");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentException));
            }
        }

        [TestMethod]
        public void TestConfiguringDuplicatePages()
        {
            try
            {
                navSvc.ConfigurePage(typeof(Page1ViewModel).Name + "-1", "Page1.xaml");
                Assert.Fail("The exception wasn't thrown");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentException));
            }
        }
        #endregion

        #region NavigateTo Tests
        [TestMethod]
        public void TestNavigateToGoodParams()
        {
            Action<Type> nav = (vmType) =>
            {
                var param = $"Model: {vmType.Name}";
                navSvc.NavigateTo(vmType.Name, param);

                Assert.AreEqual(NAV_DICT[vmType.Name], targetFrame.Source.OriginalString);
                Assert.AreEqual(vmType.Name, navSvc.CurrentPageKey);
                Assert.AreEqual(param, navSvc.ViewParameter);
            };
            Assert.IsFalse(navSvc.CanGoForward());
            Assert.IsFalse(navSvc.CanGoBack());

            nav(typeof(Page1ViewModel));
            Assert.IsFalse(navSvc.CanGoForward());
            Assert.IsFalse(navSvc.CanGoBack());

            nav(typeof(Page2ViewModel));
            Assert.IsTrue(navSvc.CanGoBack());
            Assert.IsFalse(navSvc.CanGoForward());

            nav(typeof(Page3ViewModel));
            Assert.IsTrue(navSvc.CanGoBack());
            Assert.IsFalse(navSvc.CanGoForward());
        }

        [TestMethod]
        public void TestNavigateToWithInvalidKey()
        {
            try
            {
                navSvc.NavigateTo("This view doesn't exist");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentException));
            }
        }
        #endregion

        #region Forward/Back Tests
        [TestMethod]
        public void TestGoBack()
        {
            var uc1Model = "uc1 model";
            navSvc.NavigateTo(typeof(Page1ViewModel).Name, uc1Model);
            navSvc.NavigateTo(typeof(Page2ViewModel).Name);

            navSvc.GoBack();

            Assert.AreEqual(NAV_DICT[typeof(Page1ViewModel).Name], targetFrame.Source.OriginalString);
            Assert.AreEqual(typeof(Page1ViewModel).Name, navSvc.CurrentPageKey);
            Assert.AreEqual(uc1Model, navSvc.ViewParameter);
        }

        [TestMethod]
        public void TestGoForward()
        {
            var uc2Model = "uc2 model";
            navSvc.NavigateTo(typeof(Page1ViewModel).Name);
            navSvc.NavigateTo(typeof(Page2ViewModel).Name, uc2Model);

            navSvc.GoBack();
            navSvc.GoForward();

            Assert.AreEqual(NAV_DICT[typeof(Page2ViewModel).Name], targetFrame.Source.OriginalString);
            Assert.AreEqual(typeof(Page2ViewModel).Name, navSvc.CurrentPageKey);
            Assert.AreEqual(uc2Model, navSvc.ViewParameter);
        }
        #endregion

        internal sealed class Page1ViewModel { }
        internal sealed class Page2ViewModel { }
        internal sealed class Page3ViewModel { }
    }
}
