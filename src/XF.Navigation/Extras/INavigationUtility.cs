using Xamarin.Forms;

namespace XF.Navigation.Extras
{
    public interface INavigationUtility
    {
        /// <summary>
        /// Changes the color of the status bar.
        /// </summary>
        /// <param name="color">The new color of the status bar.</param>
        void ChangeStatusBarColor(Color color);
    }
}
