
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XF.Navigation.FormsResources
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NavigationColors : ResourceDictionary
    {
        public NavigationColors(NavigationColorConfiguration NavigationColor)
        {
            InitializeComponent();
            SetColors(NavigationColor);
        }


        private void SetColors(NavigationColorConfiguration NavigationColor)
        {
            TryAddColorResource(NavigationConstants.Color.NAV_BAR_COLOR, NavigationColor.NavBarColor);
            TryAddColorResource(NavigationConstants.Color.NAV_BAR_TEXT_COLOR, NavigationColor.NavBarTextColor);
            TryAddColorResource(NavigationConstants.Color.STATUS_BAR_COLOR, NavigationColor.StatusBarColor);
            TryAddColorResource(NavigationConstants.Color.BACKGROUND_COLOR, NavigationColor.BackgroundColor);
        }

        private void TryAddColorResource(string key, Color color)
        {
            if (key == null || color.IsDefault)
            {
                return;
            }

            Add(key, color);
        }
    }
}