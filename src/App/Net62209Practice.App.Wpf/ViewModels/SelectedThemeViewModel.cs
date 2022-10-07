using ControlzEx.Theming;
using Serilog;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace NoNameCompany.IMS.App.Wpf.ViewModels;

public class SelectedThemeViewModel : ViewModelBase
{
    private string themeSelectedItem;


    public SelectedThemeViewModel(ILogger logger)
    {
        logger.Information("called");


        /* TODO: Shlomi, should be in configuration */
        AvailableThemes.Add("light.blue");
        AvailableThemes.Add("dark.blue");

        themeSelectedItem = AvailableThemes.First();
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
    
    public ObservableCollection<string> AvailableThemes { get; } = new();
}
