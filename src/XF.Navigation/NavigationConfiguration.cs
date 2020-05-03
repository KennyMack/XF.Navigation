using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XF.Navigation
{
     /// <summary>
     /// Class that provides navigation configuration
     /// </summary>
    public class NavigationConfiguration : BindableObject
    {
        /// <summary>
        /// Color bind Configuration
        /// </summary>
        public static readonly BindableProperty ColorConfigurationProperty = BindableProperty.Create(nameof(ColorConfiguration), typeof(NavigationColorConfiguration), typeof(NavigationColorConfiguration));

        /// <summary>
        /// Gets or sets the color configuration of the theme.
        /// </summary>
        public NavigationColorConfiguration ColorConfiguration
        {
            get => (NavigationColorConfiguration)GetValue(ColorConfigurationProperty);
            set => SetValue(ColorConfigurationProperty, value);
        }
    }
}
