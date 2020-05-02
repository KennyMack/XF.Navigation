using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XF.Navigation.Extras
{
    public interface IXFNavigationService
    {
        void SetRootView(Page view, object parameter = null);

        Task PushAsync(Page view, object parameter = null);

        Task PopAsync();

        Task PushModalAsync(Page view, object parameter = null);

        Task PopModalAsync();
    }
}
