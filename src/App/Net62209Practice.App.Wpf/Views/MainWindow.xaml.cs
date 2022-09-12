using System.Windows;
using ControlzEx.Theming;
using NoNameCompany.IMS.App.Wpf.ViewModels;

namespace NoNameCompany.IMS.App.Wpf.Views;

public partial class MainWindow
{
    public MainWindow(MainWindowViewModel mainWindowViewModel)
    {
        InitializeComponent();

        DataContext = mainWindowViewModel;

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