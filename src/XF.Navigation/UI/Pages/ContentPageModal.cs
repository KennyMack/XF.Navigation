using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using XF.Navigation.UI.Animations;

namespace XF.Navigation.UI.Pages
{
    public class ContentPageModal : PopupPage
    {
        public ContentPageModal()
        {
            this.CloseWhenBackgroundIsClicked = false;
            this.Animation = new Default();
            this.BackgroundColor = Color.Yellow;
            var titleView = new Slider { HeightRequest = 44, WidthRequest = 300 };
            NavigationPage.SetTitleView(this, titleView);
            this.Content = new ScrollView
            {
                VerticalOptions = LayoutOptions.StartAndExpand,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                Content = new StackLayout
                {
                    Children =
                    {

                    new Label { Text = "Welcome to Xamarin.Forms!" }
                    }
                }
            };
        }
    }
}