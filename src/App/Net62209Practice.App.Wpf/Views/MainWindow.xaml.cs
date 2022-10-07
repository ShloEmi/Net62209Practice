using ControlzEx.Theming;
using NoNameCompany.IMS.App.Wpf.ViewModels;
using System.Windows;

namespace NoNameCompany.IMS.App.Wpf.Views;

public partial class MainWindow
{
    public MainWindow(ItemsDataViewModel itemsDataViewModel)
    {
        InitializeComponent();

        DataContext = itemsDataViewModel;

        ThemeManager.Current.ThemeSyncMode = ThemeSyncMode.SyncWithAppMode;
        ThemeManager.Current.SyncTheme();
    }


    private void LaunchGitHubSite(object sender, RoutedEventArgs e)
    {
        // Launch the GitHub site...
    }

    private void DeployCupCakes(object sender, RoutedEventArgs e)
    {
        // deploy some CupCakes...
    }

}