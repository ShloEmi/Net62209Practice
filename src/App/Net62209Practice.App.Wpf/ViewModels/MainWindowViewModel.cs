using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using ControlzEx.Theming;

namespace NoNameCompany.IMS.App.Wpf.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private string themeSelectedItem;
    private ObservableCollection<string> availableThemes = new();


    public MainWindowViewModel()
    {
        availableThemes.Add("light.blue");
        availableThemes.Add("dark.blue");

        themeSelectedItem = availableThemes.First();
    }


    public string ThemeSelectedItem
    {
        get => themeSelectedItem;
        set
        {
            if (SetField(ref themeSelectedItem, value))
                ThemeManager.Current.ChangeTheme(Application.Current, themeSelectedItem);
        }
    }
    
    public ObservableCollection<string> AvailableThemes
    {
        get => availableThemes;
        set => SetField(ref availableThemes, value);
    }
}