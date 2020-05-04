using System.Threading.Tasks;
using Xamarin.Forms;
using XF.Navigation.UI.Pages;

namespace XF.Navigation.Extras
{
    public interface IXFNavigationService
    {
        void SetRootView(Page view, object parameter = null);

        Task PushAsync(Page view, object parameter = null);

        Task PopAsync();

        Task PushModalAsync(ContentModalPage view, object parameter = null);

        Task PopModalAsync();
    }
}
