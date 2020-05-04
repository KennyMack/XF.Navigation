using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Navigation.UI.Animations;
using XF.Navigation.UI.ToolBarItems;

namespace XF.Navigation.UI.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContentModalPage : PopupPage
    {
        public IList<XFToolBarButton> XFToolbarItems => TitleBarRightContent.Children;
        public IList<View> BasePageContent => BaseContentGrid.Children;

        public ContentModalPage()
        {
            InitializeComponent();
            this.CloseWhenBackgroundIsClicked = false;
            this.Animation = new Default();
            this.HasSystemPadding = true;
            this.SystemPaddingSides = Rg.Plugins.Popup.Enums.PaddingSide.All;
            this.scViewPage.BackgroundColor = XF.Navigation.Forms.Navigator.Color.BackgroundColor;
            this.gTitleView.BackgroundColor = XF.Navigation.Forms.Navigator.Color.NavBarColor;
            this.TitleLabel.TextColor = XF.Navigation.Forms.Navigator.Color.NavBarTextColor;
            TapGestureRecognizer tap = new TapGestureRecognizer();
            skvCanvas.GestureRecognizers.Add(tap);
            tap.Tapped += Tap_Tapped;
        }
        private async void Tap_Tapped(object sender, EventArgs e)
        {
            await skvCanvas.FadeTo(.3, 50, Easing.Linear);
            await skvCanvas.FadeTo(1, 5, Easing.Linear);

            await skvCanvas.RotateTo(-180, 200, Easing.Linear);

            await PopupNavigation.Instance.PopAsync();
        }

        private async void OnPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            var canvas = e.Surface.Canvas;
            var paint = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                Color = XF.Navigation.Forms.Navigator.Color.NavBarTextColor.ToSKColor(),
                IsAntialias = true
            };

            skvCanvas.BackgroundColor = Color.Transparent;


            using (var outerPath = SKPath.ParseSvgPathData("M242.72 256l100.07-100.07c12.28-12.28 12.28-32.19 0-44.48l-22.24-22.24c-12.28-12.28-32.19-12.28-44.48 0L176 189.28 75.93 89.21c-12.28-12.28-32.19-12.28-44.48 0L9.21 111.45c-12.28 12.28-12.28 32.19 0 44.48L109.28 256 9.21 356.07c-12.28 12.28-12.28 32.19 0 44.48l22.24 22.24c12.28 12.28 32.2 12.28 44.48 0L176 322.72l100.07 100.07c12.28 12.28 32.2 12.28 44.48 0l22.24-22.24c12.28-12.28 12.28-32.19 0-44.48L242.72 256z"))
            {
                var xamagonBounds = outerPath.Bounds;
                var smallerCanvasSide = Math.Min(skvCanvas.CanvasSize.Width, skvCanvas.CanvasSize.Height);
                smallerCanvasSide *= 0.30f; // Add a bit padding
                var largerXamagonSide = Math.Max(xamagonBounds.Height, xamagonBounds.Width);
                var scale = smallerCanvasSide / largerXamagonSide;
                outerPath.Offset(-xamagonBounds.Left, -xamagonBounds.Top);
                canvas.Translate((skvCanvas.CanvasSize.Width - smallerCanvasSide) / 2, (skvCanvas.CanvasSize.Height - smallerCanvasSide) / 2);
                canvas.Scale(scale);
                canvas.DrawPath(outerPath, paint);

            }
            await skvCanvas.RotateTo(-135, 200, Easing.Linear);
        }

        protected async override void OnAppearingAnimationEnd()
        {
            base.OnAppearingAnimationEnd();
            await skvCanvas.RotateTo(0, 200, Easing.Linear);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            foreach (var item in this.XFToolbarItems)
            {
                item.BackgroundColor = XF.Navigation.Forms.Navigator.Color.NavBarColor;
                ((XFToolBarButton)item).TextColor = XF.Navigation.Forms.Navigator.Color.NavBarTextColor;
                ((XFToolBarButton)item).ApplyLayout();
            }
        }

    }
}