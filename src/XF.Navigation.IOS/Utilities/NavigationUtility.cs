using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XF.Navigation.Extras;
using XF.Navigation.IOS.Utilities;

[assembly: Dependency(typeof(NavigationUtility))]
namespace XF.Navigation.IOS.Utilities
{
    public class NavigationUtility : INavigationUtility
    {
        public void ChangeStatusBarColor(Color color)
        {
            var isColorDark = color.ToCGColor().IsColorDark();
            UIApplication.SharedApplication.StatusBarStyle = isColorDark ? UIStatusBarStyle.LightContent : UIStatusBarStyle.Default;

            UIView statusBar = null;
            if (UIDevice.CurrentDevice.CheckSystemVersion(13, 0))
            {
                var tag = (System.nint)38482458385;
                statusBar = UIApplication.SharedApplication.KeyWindow?.ViewWithTag(tag);
                if (statusBar == null)
                {
                    statusBar = new UIView(UIApplication.SharedApplication.StatusBarFrame);
                    statusBar.Tag = tag;
                    UIApplication.SharedApplication.KeyWindow?.AddSubview(statusBar);
                }
            }
            else
            {
                statusBar = UIApplication.SharedApplication.ValueForKey(new NSString("statusBar")) as UIView;
            }

            if (statusBar != null)
            {
                statusBar.BackgroundColor = color.ToUIColor();
            }
        }
    }
}