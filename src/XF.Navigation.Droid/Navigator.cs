using System;
using System.Linq;
using Android.Content;
using Android.OS;
using XF.Navigation.Droid.Renderers;
using Xamarin.Forms;

namespace XF.Navigation.Droid
{
    public static class Navigator
    {
        internal static event EventHandler OnInitialized;

        internal static bool IsInitialized { get; private set; }

        internal static Context Context { get; private set; }

        public static void Init(Context context, Bundle bundle)
        {
            Context = context;

            IsInitialized = true;
            OnInitialized?.Invoke(null, EventArgs.Empty);
        }
    }
}
