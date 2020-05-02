using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Xamarin.Forms;

using Demo.Models;
using Demo.Services;

namespace Demo.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public IDataStore<Item> DataStore => DependencyService.Get<IDataStore<Item>>();

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName]string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        /// <summary>
        /// When overriden, allow to add additional logic to this view model when the view where it was attached was pushed using <see cref="INavigationService.PushAsync(string, object)"/>.
        /// </summary>
        /// <param name="navigationParameter">The navigation parameter to pass after the view was pushed.</param>
        public virtual void OnViewPushed(object navigationParameter = null) { }

        /// <summary>
        /// When overriden, allow to add additional logic to this view model when the view where it was attached was popped using <see cref="INavigationService.PopAsync"/>.
        /// </summary>
        public virtual void OnViewPopped()
        {
            CleanUp();
        }

        public virtual void CleanUp() { }
    }
}
