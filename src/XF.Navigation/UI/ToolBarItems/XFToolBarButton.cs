using Xamarin.Forms;

namespace XF.Navigation.UI.ToolBarItems
{
    public class XFToolBarButton: Button
    {
        public XFToolBarButton()
        {
            ApplyLayout();
        }

        public void ApplyLayout()
        {
            this.Margin = 0;
            this.Padding = 0;
            this.BorderWidth = 0;
            this.MinimumWidthRequest = 0;
            this.HorizontalOptions = LayoutOptions.End;

            this.WidthRequest = 56;
            this.HeightRequest = 56;
            this.CornerRadius = 50;
        }
    }
}
