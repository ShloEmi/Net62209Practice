using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Net62209Practice.App.Wpf.ViewModels;

public abstract class ViewModelBase : INotifyPropertyChanged
{
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null) => 
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) 
            return false;

        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }

    public virtual event PropertyChangedEventHandler? PropertyChanged;
}
