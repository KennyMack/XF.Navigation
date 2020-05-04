using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace XF.Navigation.UI.ToolBarItems
{
    public class XFToolBarContainer : StackLayout
    {
        public new ObservableCollection<XFToolBarButton> Children { get; set; }

        public XFToolBarContainer()
        {
            Children = new ObservableCollection<XFToolBarButton>();
            Children.CollectionChanged += _Children_CollectionChanged;
        }

        private void _Children_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            foreach (View item in e.NewItems)
                base.Children.Add(item);
        }

        protected override void OnAdded(View view)
        {
            base.OnAdded(view);
        }

    }
}
