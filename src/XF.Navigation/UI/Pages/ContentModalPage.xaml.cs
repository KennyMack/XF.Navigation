using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Navigation.NavBar;
using XF.Navigation.UI.Animations;
using XF.Navigation.UI.ToolBarItems;

namespace XF.Navigation.UI.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContentModalPage : PopupPage
    {
        public IList<View> BasePageTitleBarLeftContent => TitleBarLeftContent.Children;
        public IList<View> BasePageTitleBarRightContent => TitleBarRightContent.Children;
        public IList<View> BasePageContent => BaseContentGrid.Children;

        public ContentModalPage()
        {
            InitializeComponent();
            this.ToolbarItems.Add(new ToolbarItem
            {
                Text = "ab"
            });
            this.CloseWhenBackgroundIsClicked = false;
            this.Animation = new Default();
            this.HasSystemPadding = true;
            this.SystemPaddingSides = Rg.Plugins.Popup.Enums.PaddingSide.All;
            this.scViewPage.BackgroundColor = XF.Navigation.Forms.Navigator.Color.BackgroundColor;
            this.gTitleView.BackgroundColor = XF.Navigation.Forms.Navigator.Color.NavBarColor;
            this.TitleLabel.TextColor = XF.Navigation.Forms.Navigator.Color.NavBarTextColor;
            this.CloseButton.TextColor = XF.Navigation.Forms.Navigator.Color.NavBarTextColor;
            this.CloseButton.BackgroundColor = XF.Navigation.Forms.Navigator.Color.NavBarColor;

            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            foreach (var item in this.BasePageTitleBarRightContent)
            {
                item.BackgroundColor = XF.Navigation.Forms.Navigator.Color.NavBarColor;
                if (item is Label)
                    (item as Label).TextColor = XF.Navigation.Forms.Navigator.Color.NavBarTextColor;
                if (item is Button)
                    (item as Button).TextColor = XF.Navigation.Forms.Navigator.Color.NavBarTextColor;

                if (item is XFToolBarButton)
                    (item as XFToolBarButton).ApplyLayout();
            }
        }

        private async void OnClose(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }
    }
}