using ControlzEx.Theming;
using System.Windows;

namespace Net62209Practice.App.Wpf.Views;

public partial class MainWindow
{
    public MainWindow()
    {
        InitializeComponent();

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