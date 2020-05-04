using Android.Content;
using Android.OS;
using Rg.Plugins.Popup;
using System;

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
            Popup.Init(context, bundle);
        }

        /// <summary>
        /// Handles the physical back button event to dismiss specific dialogs shown
        /// </summary>
        /// <param name="backAction">The base <see cref="Activity.OnBackPressed"/> method.</param>
        public static bool HandleBackButton(Action backAction) =>
            Popup.SendBackPressed(backAction);
    }
}
