﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Controls;
using AG.Wpf.NavigationService.UserControlNav;

namespace AG.Wpf.NavigationService.Tests
{
    [TestClass]
    public class ContentNavSvcTests
    {
        private ContentControl targetControl;
        private ContentNavigationService navSvc;

        /// <summary>
        /// Initializes the <code>targetControl</code> and
        /// <code>navSvc</code> before each test.
        /// </summary>
        [TestInitialize]
        public void BeforeEach()
        {
            targetControl = new ContentControl();
            navSvc = new ContentNavigationService(() => { return targetControl; });

            navSvc.ConfigureView<View1>(typeof(View1).Name);
            navSvc.ConfigureView<View2>(typeof(View2).Name);
            navSvc.ConfigureView<View3>(typeof(View3).Name);
        }

        #region ConfigureView Tests
        /// <summary>
        /// Attempts to configure the navigation service with parameters that shouldn't work.
        /// This test expects to catch an exception.
        /// </summary>
        [TestMethod]
        public void TestConfigureViewDuplicateKeys()
        {
            try
            {
                navSvc.ConfigureView<View1>(typeof(View1).Name);
                Assert.Fail("The exception wasn't thrown");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Assert.IsInstanceOfType(ex, typeof(ArgumentException));
            }
        }

        [TestMethod]
        public void TestConfiguringDuplicateViews()
        {
            try
            {
                navSvc.ConfigureView<View1>(typeof(View1).Name + "-1");
                navSvc.ConfigureView<View1>(typeof(View1).Name + "-2");
                Assert.Fail("The exception wasn't thrown");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Assert.IsInstanceOfType(ex, typeof(ArgumentException));
            }
        }
        #endregion

        #region NavigateTo tests
        /// <summary>
        /// Attempts to navigate to different views. 
        /// This test will fail if the container is not displaying the 
        /// correct view, or if the current page key is incorrect.
        /// </summary>
        [TestMethod]
        public void TestNavigateToGoodParams()
        {
            Action<Type> nav = (viewType) =>
            {
                var param = $"This is {viewType}'s model";
                navSvc.NavigateTo(viewType.Name, param);

                Assert.IsInstanceOfType(targetControl.Content, viewType);
                Assert.AreEqual(viewType.Name, navSvc.CurrentPageKey);
                Assert.AreEqual(param, navSvc.ViewParameter);
            };

            Assert.IsFalse(navSvc.CanGoForward());
            Assert.IsFalse(navSvc.CanGoBack());

            nav(typeof(View1));
            Assert.IsFalse(navSvc.CanGoForward());
            Assert.IsFalse(navSvc.CanGoBack());

            nav(typeof(View2));
            Assert.IsTrue(navSvc.CanGoBack());
            Assert.IsFalse(navSvc.CanGoForward());

            nav(typeof(View3));
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
                Console.WriteLine(ex.Message);
                Assert.IsInstanceOfType(ex, typeof(ArgumentException));
            }
        }
        #endregion

        #region Forward/Back Tests
        [TestMethod]
        public void TestGoBack()
        {
            var uc1Model = "uc1 model";
            navSvc.NavigateTo(typeof(View1).Name, uc1Model);
            navSvc.NavigateTo(typeof(View2).Name);

            Assert.IsTrue(navSvc.CanGoBack());
            Assert.IsFalse(navSvc.CanGoForward());

            navSvc.GoBack();
            Assert.IsTrue(navSvc.CanGoForward());

            Assert.IsInstanceOfType(targetControl.Content, typeof(View1));
            Assert.AreEqual(typeof(View1).Name, navSvc.CurrentPageKey);
            Assert.AreEqual(uc1Model, navSvc.ViewParameter);
        }

        [TestMethod]
        public void TestGoForward()
        {
            var uc2Model = "uc2 model";
            navSvc.NavigateTo(typeof(View1).Name);
            navSvc.NavigateTo(typeof(View2).Name, uc2Model);

            Assert.IsTrue(navSvc.CanGoBack());
            Assert.IsFalse(navSvc.CanGoForward());

            navSvc.GoBack();
            Assert.IsTrue(navSvc.CanGoForward());

            navSvc.GoForward();
            Assert.IsTrue(navSvc.CanGoBack());
            Assert.IsFalse(navSvc.CanGoForward());

            Assert.IsInstanceOfType(targetControl.Content, typeof(View2));
            Assert.AreEqual(typeof(View2).Name, navSvc.CurrentPageKey);
            Assert.AreEqual(uc2Model, navSvc.ViewParameter);
        }

        [TestMethod]
        public void TestEnsureForwardStackCleared()
        {
            navSvc.NavigateTo(typeof(View1).Name);
            navSvc.NavigateTo(typeof(View2).Name);
            navSvc.GoBack();
            navSvc.NavigateTo(typeof(View2).Name);
            Assert.IsFalse(navSvc.CanGoForward());
        }
        #endregion

        #region Dummy views
        private class View1 : UserControl { }
        private class View2 : UserControl { }
        private class View3 : UserControl { }
        #endregion
    }
}
