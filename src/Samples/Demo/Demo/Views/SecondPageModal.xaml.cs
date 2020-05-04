using System;
using Xamarin.Forms.Xaml;
using XF.Navigation.UI.Pages;

namespace Demo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SecondPageModal : ContentModalPage
    {
        public SecondPageModal()
        {
            InitializeComponent();
        }
        private async void OnClose(object sender, EventArgs e) =>
            await App._Navigation.PopModalAsync();
    }
}