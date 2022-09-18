using Bogus;
using CommunityToolkit.Mvvm.Input;
using ControlzEx.Theming;
using Microsoft.Extensions.Configuration;
using NoNameCompany.IMS.BL.DAL.Interfaces;
using NoNameCompany.IMS.Data.ApplicationData;
using Serilog;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace NoNameCompany.IMS.App.Wpf.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private string themeSelectedItem;
    private ObservableCollection<string> availableThemes = new();
    private readonly ObservableCollection<ItemContainerData> itemContainerData = new();
    private readonly Faker<ItemData> itemDataFaker;

    private readonly IDAL dataAccessLayer;
    private readonly ILogger logger;
    private readonly IConfiguration configuration;


    public MainWindowViewModel(IDAL dataAccessLayer, ILogger logger, IConfiguration configuration, Faker<ItemData> itemDataProvider)
    {
        logger.Information("called");

        this.dataAccessLayer = dataAccessLayer;
        this.logger = logger;
        this.configuration = configuration;


        this.itemDataFaker = this.itemDataFaker = new Faker<ItemData>();

        AddItemsCommand = new RelayCommand<object>(
            count =>
            {
                var howMuch = int.Parse(count.ToString() ?? "0");
                this.dataAccessLayer.AddItemsBulk(
                    Enumerable
                        .Range(1, howMuch)
                        .Select(_ => itemDataProvider.Generate()).ToArray()
                );
            }, 
            _ => this.dataAccessLayer.CanAddItems());


        /* TODO: Shlomi, should be in configuration */
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

    public ICommand AddItemsCommand { get; }
}
