using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace OpenTools.MVVM;
public class ViewModelBase : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    public bool SetProperty<T>(ref T source, T value,
        [CallerMemberName] string propertyName = "")
    {
        if (Equals(source, value))
        {
            return false;
        }
        source = value;
        OnPropertyChanged(propertyName);
        return true;
    }

    public void OnPropertyChanged([CallerMemberName] string proerptyName = "")
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(proerptyName));
}
