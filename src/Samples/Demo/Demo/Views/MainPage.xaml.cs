using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Demo.Models;

namespace Demo.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : MasterDetailPage
    {
        Dictionary<int, Page> MenuPages = new Dictionary<int, Page>();
        public MainPage()
        {
            InitializeComponent();

            MasterBehavior = MasterBehavior.SplitOnPortrait;

           // MenuPages.Add((int)MenuItemType.Browse, (NavigationPage)Detail);
        }

        public async Task NavigateFromMenu(int id)
        {
            if (!MenuPages.ContainsKey(id))
            {
                switch (id)
                {
                    case (int)MenuItemType.Second:
                        MenuPages.Add(id, new SecondPage());
                        break;
                    case (int)MenuItemType.Third:
                        MenuPages.Add(id, new ThirdPage());
                        break;
                    case (int)MenuItemType.Browse:
                        MenuPages.Add(id, new ItemsPage());
                        break;
                    case (int)MenuItemType.About:
                        MenuPages.Add(id, new AboutPage());
                        break;
                }
            }

            var newPage = MenuPages[id];

            if (newPage != null && Detail != newPage)
            {
                await App._Navigation.PushAsync(newPage);

                if (Device.RuntimePlatform == Device.Android)
                    await Task.Delay(100);

                IsPresented = false;
            }
        }
    }
}