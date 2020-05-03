using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Demo.Services;
using Demo.Views;

namespace Demo
{
    public partial class App : Application
    {
        public static NavigationService _Navigation;

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();

            XF.Navigation.Forms.Navigator.Init(this, "NavBarConfig.Style");

            var navService = new NavigationService();

            navService.SetRootView(new ItemsPage());
            _Navigation = navService;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
