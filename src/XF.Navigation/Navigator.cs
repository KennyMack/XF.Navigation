using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using XF.Navigation.Extras;
using XF.Navigation.FormsResources;

namespace XF.Navigation.Forms
{
    public class Navigator
    {
        private static readonly Lazy<INavigationUtility> NavigationUtilityInstance = new Lazy<INavigationUtility>(() => DependencyService.Get<INavigationUtility>());
        private readonly NavigationConfiguration _config;
        private readonly ResourceDictionary _res;

        internal Navigator(Application app, NavigationConfiguration materialResource) : this(app)
        {
            _config = materialResource;
        }

        internal Navigator(Application app, string key) : this(app)
        {
            _config = GetResource<NavigationConfiguration>(key);
        }

        internal Navigator(Application app)
        {
            _res = app.Resources;
            _config = new NavigationConfiguration
            {
                ColorConfiguration = new NavigationColorConfiguration()
            };
        }

        /// <summary>
        /// A dependency service for configuring cross-platform UI customizations.
        /// </summary>
        public static INavigationUtility PlatformConfiguration => NavigationUtilityInstance.Value;

        /// <summary>
        /// Gets a resource of the specified type from the current
        /// </summary>
        /// <typeparam name="T">The type of the resource object to be retrieved.</typeparam>
        /// <param name="key">The key of the resource object. For a list of XF-Navigation resource keys.</param>
        /// <exception cref="InvalidCastException" />
        /// <exception cref="ArgumentNullException" />
        public static T GetResource<T>(string key)
        {
            Application.Current.Resources.TryGetValue(key ?? throw new ArgumentNullException(nameof(key)), out var value);

            if (value is T resource)
            {
                return resource;
            }

            if (value != null)
            {
                throw new InvalidCastException($"The resource retrieved was not of the type {typeof(T)}. Use {value.GetType()} instead.");
            }

            return default;
        }

        /// <summary>
        /// Configure's the current app's resources by merging pre-defined Navigation resources and creating new resources
        /// </summary>
        /// <param name="app">The cross-platform mobile application that is running.</param>
        /// <param name="NavigationConfiguration">The object containing the <see cref="NavigationColorConfiguration"/>.</param>
        /// <exception cref="ArgumentNullException" />
        public static void Init(Application app, NavigationConfiguration navigationResource)
        {
            var navigation = new Navigator(app ?? throw new ArgumentNullException(nameof(app)), navigationResource ?? throw new ArgumentNullException(nameof(navigationResource)));
            navigation.MergeNavigationDictionaries();
        }

        /// <summary>
        /// Configure's the current app's resources by merging pre-defined Navigation resources and creating new resources based on the <see cref="NavigationConfiguration"/>'s properties.
        /// </summary>
        /// <param name="app">The cross-platform mobile application that is running.</param>
        /// <param name="key">The key of the <see cref="NavigationConfiguration"/> object in the current app's resource dictionary.</param>
        /// <exception cref="ArgumentNullException" />
        public static void Init(Application app, string key)
        {
            var navigation = new Navigator(app ?? throw new ArgumentNullException(nameof(app)), key ?? throw new ArgumentNullException(nameof(key)));
            navigation.MergeNavigationDictionaries();
        }

        /// <summary>
        /// Configure's the current app's resources by merging pre-defined Material resources.
        /// </summary>
        /// <param name="app">The cross-platform mobile application that is running.</param>
        /// <exception cref="ArgumentNullException" />
        public static void Init(Application app)
        {
            var navigation = new Navigator(app ?? throw new ArgumentNullException(nameof(app)));
            navigation.MergeNavigationDictionaries();
        }

        private void MergeNavigationDictionaries()
        {
            _res.MergedDictionaries.Add(new NavigationColors(_config.ColorConfiguration ?? new NavigationColorConfiguration()));
           
        }


        /// <summary>
        /// Static class that contains the current navigation color values.
        /// </summary>
        public static class Color
        {
            /// <summary>
            /// Navigation bar background color
            /// </summary>
            public static Xamarin.Forms.Color NavBarColor => GetResource<Xamarin.Forms.Color>(NavigationConstants.Color.NAV_BAR_COLOR);

            /// <summary>
            /// Navigation text color
            /// </summary>
            public static Xamarin.Forms.Color NavBarTextColor => GetResource<Xamarin.Forms.Color>(NavigationConstants.Color.NAV_BAR_TEXT_COLOR);

            /// <summary>
            /// Status bar color
            /// </summary>
            public static Xamarin.Forms.Color StatusBarColor => GetResource<Xamarin.Forms.Color>(NavigationConstants.Color.STATUS_BAR_COLOR);

            /// <summary>
            /// Background page color
            /// </summary>
            public static Xamarin.Forms.Color BackgroundColor => GetResource<Xamarin.Forms.Color>(NavigationConstants.Color.BACKGROUND_COLOR);
        }
    }
}
