using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Kingscup.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        #region events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region methods

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            field = value;

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}