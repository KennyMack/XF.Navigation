using Xamarin.Forms;

namespace XF.Navigation
{
    public class NavigationColorConfiguration : BindableObject
    {
        /// <summary>
        /// Navigation background color
        /// </summary>
        public static readonly BindableProperty NavBarColorProperty = BindableProperty.Create(nameof(NavBarColor), typeof(Color), typeof(Color), Color.FromHex("#2196F3"));

        /// <summary>
        /// Navigation text color
        /// </summary>
        public static readonly BindableProperty NavBarTextColorProperty = BindableProperty.Create(nameof(NavBarTextColor), typeof(Color), typeof(Color), Color.FromHex("#FFFFFF"));

        /// <summary>
        /// Navigation status bar color
        /// </summary>
        public static readonly BindableProperty StatusBarColorProperty = BindableProperty.Create(nameof(StatusBarColor), typeof(Color), typeof(Color), Color.FromHex("#0466b5"));

        /// <summary>
        /// Navigation status bar color
        /// </summary>
        public static readonly BindableProperty BackgroundColorProperty = BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(Color), Color.FromHex("#FFFFFF"));


        /// <summary>
        /// Navigation background color
        /// </summary>
        public Color NavBarColor
        {
            get => (Color)GetValue(NavBarColorProperty);
            set => SetValue(NavBarColorProperty, value);
        }

        /// <summary>
        /// Navigation text color
        /// </summary>
        public Color NavBarTextColor
        {
            get => (Color)GetValue(NavBarTextColorProperty);
            set => SetValue(NavBarTextColorProperty, value);
        }

        /// <summary>
        /// Navigation background color
        /// </summary>
        public Color StatusBarColor
        {
            get => (Color)GetValue(StatusBarColorProperty);
            set => SetValue(StatusBarColorProperty, value);
        }

        /// <summary>
        /// Background page color
        /// </summary>
        public Color BackgroundColor
        {
            get => (Color)GetValue(BackgroundColorProperty);
            set => SetValue(BackgroundColorProperty, value);
        }
    }
}
