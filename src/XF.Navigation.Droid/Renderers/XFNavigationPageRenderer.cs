using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XF.Navigation.Droid.Renderers;
using XF.Navigation.NavBar;

[assembly: ExportRenderer(typeof(XFNavigationPage), typeof(XFNavigationPageRenderer))]

namespace XF.Navigation.Droid.Renderers
{
    public class XFNavigationPageRenderer : Xamarin.Forms.Platform.Android.AppCompat.NavigationPageRenderer
    {
        private XFNavigationPage _navigationPage;
        private MultiPage<Page> _multiPageParent;
        private Toolbar _toolbar;
        private Page _childPage;

        public XFNavigationPageRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<NavigationPage> e)
        {
            base.OnElementChanged(e);

            if (e?.NewElement != null)
            {
                _navigationPage = Element as XFNavigationPage;

                _toolbar = ViewGroup.GetChildAt(0) as Toolbar;

                HandleParent(_navigationPage.Parent);

                HandleChildPage(_navigationPage.CurrentPage);
            }

            if (e?.OldElement != null)
            {
                if (_childPage != null)
                {
                    _childPage.PropertyChanged -= ChildPage_PropertyChanged;
                }

                if (_multiPageParent != null)
                {
                    _multiPageParent.CurrentPageChanged -= MultiPageParent_CurrentPageChanged;
                }
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == NavigationPage.CurrentPageProperty.PropertyName)
            {
                HandleChildPage(_navigationPage.CurrentPage);
            }
            else if (e.PropertyName == nameof(_navigationPage.Parent))
            {
                HandleParent(_navigationPage.Parent);
            }
        }

        private void HandleParent(Element parent)
        {
            if (parent is MultiPage<Page>)
            {
                if (_multiPageParent != null)
                {
                    _multiPageParent.CurrentPageChanged -= MultiPageParent_CurrentPageChanged;
                }

                _multiPageParent = parent as Xamarin.Forms.MultiPage<Xamarin.Forms.Page>;

                if (_multiPageParent != null)
                {
                    _multiPageParent.CurrentPageChanged += MultiPageParent_CurrentPageChanged;
                }
            }
            else if (_multiPageParent != null)
            {
                _multiPageParent.CurrentPageChanged -= MultiPageParent_CurrentPageChanged;

                _multiPageParent = null;
            }
        }

        private void MultiPageParent_CurrentPageChanged(object sender, EventArgs e)
        {
            if (!(sender is MultiPage<Page> multiPage))
            {
                return;
            }

            if (multiPage.CurrentPage is NavigationPage navPage)
            {
                ChangeStatusBarColor(navPage.CurrentPage);
            }
            else
            {
                ChangeStatusBarColor(multiPage.CurrentPage);
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

            /*if (e.PropertyName == MaterialNavigationPage.AppBarElevationProperty.PropertyName)
            {
                ChangeElevation(page);
            }
            else*/ if (e.PropertyName == XFNavigationPage.StatusBarColorProperty.PropertyName)
            {
                ChangeStatusBarColor(page);
            }
        }

        protected override Task<bool> OnPopToRootAsync(Page page, bool animated)
        {
            _navigationPage.InternalPopToRoot(page);

            ChangeElevation(page);

            ChangeStatusBarColor(page);

            return base.OnPopToRootAsync(page, animated);
        }

        protected override Task<bool> OnPopViewAsync(Page page, bool animated)
        {
            var navStack = _navigationPage.Navigation.NavigationStack.ToList();

            if (navStack.Count - 1 - navStack.IndexOf(page) < 0)
            {
                return base.OnPopViewAsync(page, animated);
            }

            var previousPage = navStack[navStack.IndexOf(page) - 1];

            _navigationPage.InternalPagePop(previousPage, page);

            ChangeElevation(previousPage);

            ChangeStatusBarColor(previousPage);

            return base.OnPopViewAsync(page, animated);
        }

        protected override Task<bool> OnPushAsync(Page page, bool animated)
        {
            _navigationPage.InternalPagePush(page);

            ChangeElevation(page);

            if (_navigationPage.Parent is MultiPage<Page> parent)
            {
                if (parent.CurrentPage == _navigationPage)
                {
                    ChangeStatusBarColor(page);
                }
            }
            else
            {
                ChangeStatusBarColor(page);
            }

            return base.OnPushAsync(page, animated);
        }

        private void ChangeElevation(Page page)
        {
            /*var elevation = (double)page.GetValue(XFNavigationPage.AppBarElevationProperty);

            ChangeElevation(elevation);*/
        }

        public void ChangeElevation(double elevation)
        {
            /* if (elevation > 0)
             {
                 _toolbar.Elevate(elevation);
             }
             else
             {
                 _toolbar.Elevate(0);
             }*/
        }

        private void ChangeStatusBarColor(Page page)
        {
            var statusBarColor = (Color)page.GetValue(XFNavigationPage.StatusBarColorProperty);

            Forms.Navigator.PlatformConfiguration.ChangeStatusBarColor(statusBarColor.IsDefault ? Forms.Navigator.Color.StatusBarColor : statusBarColor);
        }

        public override void AddView(Android.Views.View child)
        {
            base.AddView(child);
        }

        public override void AddView(Android.Views.View child, int index, LayoutParams @params)
        {
            base.AddView(child, index, @params);
        }

        public override void AddView(Android.Views.View child, LayoutParams @params)
        {
            base.AddView(child, @params);
        }

        public override void AddView(Android.Views.View child, int width, int height)
        {
            base.AddView(child, width, height);
        }

        public override void AddView(Android.Views.View child, int index)
        {
            base.AddView(child, index);
        }
    }
}