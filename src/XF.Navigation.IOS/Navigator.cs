
using Rg.Plugins.Popup;
using System;

namespace XF.Navigation.IOS
{
    public static class Navigator
    {
        internal static event EventHandler OnInitialized;

        internal static bool IsInitialized { get; private set; }

        public static void Init()
        {
            Popup.Init();
            IsInitialized = true;
            OnInitialized?.Invoke(null, EventArgs.Empty);
            
        }

    }
}
