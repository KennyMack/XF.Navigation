using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace XF.Navigation.NavBar
{
    public class XFNavigationPage : NavigationPage
    {
        private TitleLabel _customTitleView;

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

            ChangeFont(rootPage);
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
            /*if (page.BackgroundColor.IsDefault)
            {
                page.SetDynamicResource(BackgroundColorProperty, MaterialConstants.Color.BACKGROUND);
            }*/

            ChangeFont(page);
            ChangeBarTextColor(page);
            ChangeBarBackgroundColor(page);
        }

        private void ChangeBarBackgroundColor(Page page)
        {
            /*var barColor = (Color)page.GetValue(AppBarColorProperty);

            if (barColor.IsDefault)
            {
                SetDynamicResource(BarBackgroundColorProperty, MaterialConstants.Color.PRIMARY);
            }
            else
            {
                BarBackgroundColor = barColor;
            }*/
        }

        private void ChangeBarTextColor(Page page)
        {
            /*var barTextColor = (Color)page.GetValue(AppBarTitleTextColorProperty);

            if (page.GetValue(TitleViewProperty) is TitleLabel customTitleView)
            {
                if (barTextColor.IsDefault)
                {
                    BarTextColor = customTitleView.TextColor = Material.Color.OnPrimary;
                }
                else
                {
                    BarTextColor = customTitleView.TextColor = barTextColor;
                }
            }
            else
            {
                BarTextColor = barTextColor.IsDefault ? Material.Color.OnPrimary : barTextColor;
            }*/
        }

        private void ChangeFont(Page page)
        {
            /*var currentTitleView = page.GetValue(TitleViewProperty);

            var textAlignment = (TextAlignment)page.GetValue(AppBarTitleTextAlignmentProperty);

            if (currentTitleView != null)
            {
                if (currentTitleView is TitleLabel titleLabelView)
                {
                    titleLabelView.HorizontalTextAlignment = textAlignment;
                }
                return;
            }

            _customTitleView = new TitleLabel();

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    _customTitleView.Margin = Navigation.NavigationStack.Count == 1 ? new Thickness(8, 0, 8, 0) : new Thickness(8, 0, 32, 0);
                    break;
                case Device.Android when Navigation.NavigationStack.Count > 1 && page.ToolbarItems.Count == 0:
                    _customTitleView.Margin = new Thickness(0, 0, 72, 0);
                    break;
            }

            page.SetValue(TitleViewProperty, _customTitleView);

            _customTitleView.VerticalTextAlignment = TextAlignment.Center;
            _customTitleView.VerticalOptions = LayoutOptions.FillAndExpand;
            _customTitleView.HorizontalOptions = LayoutOptions.FillAndExpand;
            _customTitleView.HorizontalTextAlignment = textAlignment;
            _customTitleView.Text = page.Title;*/
        }
    }
    internal class TitleLabel : Label { }
}
