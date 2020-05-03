using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XF.Navigation.FormsResources;
using XF.Navigation.UI.Pages;

namespace XF.Navigation.NavBar
{
    public class XFNavigationPage : NavigationPage
    {

        /// <summary>
        /// Attached property that is used by <see cref="Page"/>s to determine the status bar color.
        /// </summary>
        public static readonly BindableProperty NavBarColorProperty = BindableProperty.Create("NavBarColor", typeof(Color), typeof(XFNavigationPage), Color.Default);


        /// <summary>
        /// Attached property that is used by <see cref="Page"/>s to determine the status bar color.
        /// </summary>
        public static readonly BindableProperty NavBarTextColorProperty = BindableProperty.Create("NavBarTextColor", typeof(Color), typeof(XFNavigationPage), Color.Default);

        /// <summary>
        /// Attached property that is used by <see cref="Page"/>s to determine the status bar color.
        /// </summary>
        public static readonly BindableProperty StatusBarColorProperty = BindableProperty.Create("StatusBarColor", typeof(Color), typeof(XFNavigationPage), Color.Default);


        /// <summary>
        /// For binding use only.
        /// </summary>
        public static Color GetNavBarColor(BindableObject view)
        {
            return (Color)view.GetValue(NavBarColorProperty);
        }

        /// <summary>
        /// For binding use only.
        /// </summary>
        public static Color GetNavBarTextColor(BindableObject view)
        {
            return (Color)view.GetValue(NavBarTextColorProperty);
        }

        /// <summary>
        /// For binding use only.
        /// </summary>
        public static Color GetStatusBarColor(BindableObject view)
        {
            return (Color)view.GetValue(StatusBarColorProperty);
        }


        /// <summary>
        /// For binding use only.
        /// </summary>
        public static void SetNavBarColor(BindableObject view, Color color)
        {
            view.SetValue(NavBarColorProperty, color);
        }

        /// <summary>
        /// For binding use only.
        /// </summary>
        public static void SetNavBarTextColor(BindableObject view, Color color)
        {
            view.SetValue(NavBarTextColorProperty, color);
        }

        /// <summary>
        /// For binding use only.
        /// </summary>
        public static void SetStatusBarColor(BindableObject view, Color color)
        {
            view.SetValue(StatusBarColorProperty, color);
        }


        // private TitleLabel _customTitleView;
        /// <summary>
        /// Control current navigation stack
        /// </summary>
        public INavigation PageNavigation;

        /// <summary>
        /// Initializes a new instance of <see cref="XFNavigationPage"/>.
        /// </summary>
        /// <param name="rootPage">The root page.</param>
        public XFNavigationPage(Page rootPage) : base(rootPage)
        {
            /*if (rootPage.BackgroundColor.IsDefault)
            {
                rootPage.SetDynamicResource(BackgroundColorProperty, MaterialConstants.Color.BACKGROUND);
            }*/
            PageNavigation = this.Navigation;
            
            ChangeBarTextColor(rootPage);
            ChangeBarBackgroundColor(rootPage);
        }

        /// <summary>
        /// For internal use only.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void InternalPopToRoot(Page rootPage)
        {
            OnPopToRoot(rootPage);
        }

        /// <summary>
        /// For internal use only.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void InternalPagePop(Page previousPage, Page poppedPage)
        {
            OnPagePop(previousPage, poppedPage);
        }

        /// <summary>
        /// For internal use only.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void InternalPagePush(Page page)
        {
            OnPagePush(page);
        }

        /// <summary>
        /// For internal use only.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void ForceUpdateCurrentPage()
        {
            UpdatePage(CurrentPage);
        }

        public async Task PushModalAsync(ContentModalPage page, bool animated)
        {
            var ab = PageNavigation.ModalStack;
            await PopupNavigation.Instance.PushAsync(page, animated);

            var ac = PageNavigation.ModalStack;
        }

        public async Task PopModalAsync(bool animated)
        {
            var ab = PageNavigation.ModalStack;
            await PopupNavigation.Instance.PopAsync(animated);
            var ac = PageNavigation.ModalStack;
        }

        public async Task PushViewAsync(Page view, bool animated)
        {
            await PageNavigation.PushAsync(view, animated);
        }

        public async Task PopViewAsync(bool animated)
        {
            await PageNavigation.PopAsync(animated);
        }

        protected override void OnPropertyChanging([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanging(propertyName);

            if (propertyName == nameof(CurrentPage) && CurrentPage != null)
            {
                CurrentPage.PropertyChanged -= Page_PropertyChanged;
            }
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == nameof(CurrentPage) && CurrentPage != null)
            {
                CurrentPage.PropertyChanged += Page_PropertyChanged;
            }
        }

        private void Page_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var page = sender as Page;

            if (page == null)
            {
                return;
            }

            if (e.PropertyName == nameof(Title) && page.GetValue(TitleViewProperty) is TitleLabel label)
            {
                label.Text = page.Title;
            }
            else if (e.PropertyName == NavBarColorProperty.PropertyName)
            {
                ChangeBarBackgroundColor(page);
            }
            else if (e.PropertyName == NavBarTextColorProperty.PropertyName)
            {
                ChangeBarTextColor(page);
            }
        }

        /// <summary>
        /// Called when all pages are being popped.
        /// </summary>
        /// <param name="rootPage">The root page.</param>
        protected virtual void OnPopToRoot(Page rootPage)
        {
            UpdatePage(rootPage);

            rootPage.SetValue(BackButtonTitleProperty, string.Empty);
        }

        /// <summary>
        /// Called when a page is being popped.
        /// </summary>
        /// <param name="previousPage">The page that will re-appear.</param>
        /// <param name="poppedPage">The page that will be popped.</param>
        protected virtual void OnPagePop(Page previousPage, Page poppedPage)
        {
            UpdatePage(previousPage);

            previousPage.SetValue(BackButtonTitleProperty, string.Empty);
        }

        /// <summary>
        /// Called when a page is being pushed.
        /// </summary>
        /// <param name="page">The page that is being pushed.</param>
        protected virtual void OnPagePush(Page page)
        {
            UpdatePage(page);

            page.SetValue(BackButtonTitleProperty, string.Empty);
        }

        private void UpdatePage(Page page)
        {
            ChangeBarTextColor(page);
            ChangeBarBackgroundColor(page);
        }

        private void ChangeBarBackgroundColor(Page page)
        {
            var barColor = (Color)page.GetValue(NavBarColorProperty);

            if (barColor.IsDefault)
            {
                SetDynamicResource(BarBackgroundColorProperty, NavigationConstants.Color.NAV_BAR_COLOR);
            }
            else
            {
                BarBackgroundColor = barColor;
            }
        }

        private void ChangeBarTextColor(Page page)
        {
            var barTextColor = (Color)page.GetValue(NavBarTextColorProperty);

            if (page.GetValue(TitleViewProperty) is TitleLabel customTitleView)
            {
                if (barTextColor.IsDefault)
                    BarTextColor = customTitleView.TextColor = Forms.Navigator.Color.NavBarTextColor;
                else
                    BarTextColor = customTitleView.TextColor = barTextColor;
            }
            else
                BarTextColor = barTextColor.IsDefault ? Forms.Navigator.Color.NavBarTextColor : barTextColor;
        }
    }
    internal class TitleLabel : Label { }
}
