using Demo.ViewModels;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XF.Navigation.NavBar;

namespace Demo
{
    public class CustomNavigation : XFNavigationPage
    {
        private object _currentNavigationParameter;
        public CustomNavigation(Page rootPage) : base(rootPage)
        {
        }

        public CustomNavigation(Page rootPage, object parameter) : base(rootPage)
        {
            _currentNavigationParameter = parameter;
        }

        public async Task PopViewAsync()
        {
            await Navigation.PopAsync(true);
        }

        public async Task PushViewAsync(Page view, object parameter = null)
        {
            _currentNavigationParameter = parameter;
            await Navigation.PushAsync(view, true);
        }

        public async Task PushModalAsync(Page view, object parameter = null)
        {
            _currentNavigationParameter = parameter;
            await Navigation.PushModalAsync(view, true);
        }

        public async Task PopModalAsync()
        {
            await Navigation.PopModalAsync(true);
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == nameof(RootPage) && RootPage != null)
            {
                RootPage.Appearing += AppearingHandler;
            }
        }

        protected override void OnPagePush(Page page)
        {
            base.OnPagePush(page);

            if (!(page.BindingContext is BaseViewModel viewModel))
            {
                return;
            }

            viewModel?.OnViewPushed(_currentNavigationParameter);
            _currentNavigationParameter = null;
        }

        protected override void OnPagePop(Page previousPage, Page poppedPage)
        {
            base.OnPagePop(previousPage, poppedPage);

            if (previousPage.BindingContext is BaseViewModel viewModel)
            {
                viewModel.OnViewPopped();
            }
        }

        private void AppearingHandler(object sender, EventArgs e)
        {
            var viewModel = RootPage.BindingContext as BaseViewModel;
            viewModel?.OnViewPushed(_currentNavigationParameter);
            _currentNavigationParameter = null;
            RootPage.Appearing -= AppearingHandler;
        }
    }
}
