using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EasyRooms.ViewModel

{
    public class BindableBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged = delegate { };

        protected virtual void SetProperty<T>(ref T member, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(member, value))
            {
                return;
            }

            member = value;
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OnPropertyChanged(string propertyName)
            => PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
    }
}