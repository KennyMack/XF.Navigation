using Android.OS;
using Android.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XF.Navigation.Droid.Utilities;
using XF.Navigation.Extras;

[assembly: Dependency(typeof(NavigationUtility))]
namespace XF.Navigation.Droid.Utilities
{
    public class NavigationUtility : INavigationUtility
    {
        public void ChangeStatusBarColor(Color color)
        {
            var activity = (FormsAppCompatActivity)Navigator.Context;

            var isColorDark = color.ToAndroid().IsColorDark();
            activity.SetStatusBarColor(color.ToAndroid());

            if (Build.VERSION.SdkInt < BuildVersionCodes.M)
            {
                return;
            }

            if (!isColorDark)
            {
                activity.Window.DecorView.SystemUiVisibility = (StatusBarVisibility)SystemUiFlags.LightStatusBar;
            }
            else
            {
                activity.Window.DecorView.SystemUiVisibility = StatusBarVisibility.Visible;
            }
        }
    }
}