using Demo.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XF.Navigation.Extras;
using XF.Navigation.UI.Pages;

namespace Demo
{
    public class NavigationService : IXFNavigationService
    {
        private CustomNavigation _currentNavigationPage;

        public async Task PopAsync()
        {
            await _currentNavigationPage?.PopViewAsync();
        }

        public async Task PushAsync(Page view, object parameter = null)
        {
            await _currentNavigationPage?.PushViewAsync(view, parameter);
        }

        public async Task PushModalAsync(ContentModalPage view, object parameter = null)
        {
            await _currentNavigationPage?.PushModalAsync(view, parameter);
        }

        public async Task PopModalAsync()
        {
            await _currentNavigationPage?.PopModalAsync();
        }

        public void SetRootView(Page view, object parameter = null)
        {
            _currentNavigationPage = new CustomNavigation(view, parameter);



            var pagePrincipal = new MainPage();

            pagePrincipal.Detail = _currentNavigationPage;

            Application.Current.MainPage = pagePrincipal;
        }
    }
}
