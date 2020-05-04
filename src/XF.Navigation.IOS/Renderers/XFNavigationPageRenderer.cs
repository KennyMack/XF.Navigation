﻿using ObjCRuntime;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XF.Navigation.IOS.Renderers;
using XF.Navigation.NavBar;

[assembly: ExportRenderer(typeof(XFNavigationPage), typeof(XFNavigationPageRenderer))]
namespace XF.Navigation.IOS.Renderers
{
    public class XFNavigationPageRenderer : NavigationRenderer
    {
        private XFNavigationPage _navigationPage;
        private Page _childPage;

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            if (e?.NewElement != null)
            {
                _navigationPage = Element as XFNavigationPage;

                _navigationPage.PropertyChanged += MaterialNavigationPage_PropertyChanged;

                _navigationPage.Appearing += MaterialNavigationPage_Appearing;

                Delegate = new NavigationControllerDelegate(this, _navigationPage);

                HandleChildPage(_navigationPage.CurrentPage);
            }

            if (e?.OldElement != null)
            {
                _navigationPage.PropertyChanged -= MaterialNavigationPage_PropertyChanged;

                _navigationPage.Appearing -= MaterialNavigationPage_Appearing;

                if (_childPage != null)
                {
                    _childPage.PropertyChanged -= ChildPage_PropertyChanged;
                }
            }
        }

        private void MaterialNavigationPage_Appearing(object sender, EventArgs e)
        {
            ChangeStatusBarColor(_navigationPage.CurrentPage);
        }

        private void MaterialNavigationPage_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == NavigationPage.CurrentPageProperty.PropertyName)
            {
                HandleChildPage(_navigationPage.CurrentPage);
            }
        }

        private void HandleChildPage(Page page)
        {
            if (_childPage != null)
            {
                _childPage.PropertyChanged -= ChildPage_PropertyChanged;
            }

            _childPage = page;

            if (_childPage != null)
            {
                _childPage.PropertyChanged += ChildPage_PropertyChanged;
            }
        }

        private void ChildPage_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var page = sender as Page;

            if (page == null)
            {
                return;
            }

            /*if (e.PropertyName == XFNavigationPage.AppBarElevationProperty.PropertyName)
            {
                ChangeElevation(page);
            }
            else */if (e.PropertyName == XFNavigationPage.StatusBarColorProperty.PropertyName)
            {
                ChangeStatusBarColor(page);
            }
        }

        public override UIViewController[] PopToRootViewController(bool animated)
        {
            _navigationPage.InternalPopToRoot(_navigationPage.RootPage);

            ChangeElevation(_navigationPage.RootPage);

            ChangeStatusBarColor(_navigationPage.RootPage);

            return base.PopToRootViewController(animated);
        }

        public override UIViewController PopViewController(bool animated)
        {
            var navStack = _navigationPage.Navigation.NavigationStack.ToList();

            if (navStack.Count - 1 - navStack.IndexOf(_navigationPage.CurrentPage) < 0)
            {
                return base.PopViewController(animated);
            }

            var currentPage = _navigationPage.CurrentPage;

            var previousPage = navStack[navStack.IndexOf(_navigationPage.CurrentPage) - 1];

            _navigationPage.InternalPagePop(previousPage, currentPage);

            ChangeElevation(previousPage);

            ChangeStatusBarColor(previousPage);

            return base.PopViewController(animated);
        }

        protected override Task<bool> OnPushAsync(Page page, bool animated)
        {
            _navigationPage.InternalPagePush(page);

            ChangeElevation(page);

            ChangeStatusBarColor(page);

            return base.OnPushAsync(page, animated);
        }

        private void ChangeElevation(Page page)
        {
            /*var elevation = (double)page.GetValue(XFNavigationPage.AppBarElevationProperty);

            ChangeElevation(elevation);*/
        }

        private void ChangeElevation(double elevation)
        {
            /*if (NavigationBar == null || NavigationBar.Layer == null)
            {
                return;
            }

            NavigationBar.Elevate(elevation);*/
        }

        private static void ChangeStatusBarColor(Page page)
        {
            var statusBarColor = (Color)page.GetValue(XFNavigationPage.StatusBarColorProperty);

            Forms.Navigator.PlatformConfiguration.ChangeStatusBarColor(statusBarColor.IsDefault ? Forms.Navigator.Color.StatusBarColor : statusBarColor);
        }

        private class NavigationControllerDelegate : UINavigationControllerDelegate
        {
            private UINavigationController _navigationController;
            private XFNavigationPage _navigationPage;

            public NavigationControllerDelegate(UINavigationController uINavigationController, XFNavigationPage navigationPage)
            {
                _navigationController = uINavigationController;
                _navigationPage = navigationPage;
            }

            public override void WillShowViewController(UINavigationController navigationController, [Transient] UIViewController viewController, bool animated)
            {
                var coordinator = _navigationController?.TopViewController?.GetTransitionCoordinator();

                if (coordinator == null)
                {
                    return;
                }

                coordinator.NotifyWhenInteractionChanges((context) =>
                {
                    if (context.IsCancelled)
                    {
                        _navigationPage.ForceUpdateCurrentPage();
                        ChangeStatusBarColor(_navigationPage.CurrentPage);
                    }
                });
            }
        }
    }
}