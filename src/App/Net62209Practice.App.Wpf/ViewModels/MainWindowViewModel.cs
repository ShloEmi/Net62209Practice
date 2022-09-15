using Bogus;
using CommunityToolkit.Mvvm.Input;
using ControlzEx.Theming;
using Microsoft.Extensions.Configuration;
using NoNameCompany.IMS.BL.DAL.Interfaces;
using NoNameCompany.IMS.Data.ApplicationData;
using Serilog;
using System;
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
    private readonly Faker<ItemData>? itemDataFaker;

    private readonly IDAL dataAccessLayer;
    private readonly ILogger logger;
    private readonly IConfiguration configuration;


    public MainWindowViewModel(IDAL dataAccessLayer, ILogger logger, IConfiguration configuration)
    {
        this.dataAccessLayer = dataAccessLayer;
        this.logger = logger;
        this.configuration = configuration;

        AddItemsCommand = new RelayCommand<object>(
            count =>
            {
                var howMuch = int.Parse(count.ToString() ?? "0");
                this.dataAccessLayer.AddItemsBulk(
                    Enumerable.Range(1, howMuch)
                        .Select(_ => itemDataFaker!.Generate()).ToArray()
                );
            }, 
            _ => this.dataAccessLayer.CanAddItems());


        /* TODO: Shlomi, should be in configuration */
        availableThemes.Add("light.blue");
        availableThemes.Add("dark.blue");

        themeSelectedItem = availableThemes.First();


        var itemCategorizationDataFaker = new Faker<ItemCategorizationData>()
            .RuleFor(o => o.Id, f => Guid.NewGuid())
            .RuleFor(o => o.Name, f => f.Name.JobTitle())
            .RuleFor(o => o.Description, f => f.Lorem.Sentence());

        itemDataFaker = new Faker<ItemData>()
            //Ensure all properties have rules. By default, StrictMode is false
            //Set a global policy by using Faker.DefaultStrictMode
            .StrictMode(true)
            //OrderId is deterministic
            .RuleFor(o => o.Id, f => Guid.NewGuid())
            .RuleFor(o => o.Name, f => f.Name.JobTitle())
            .RuleFor(o => o.Description, f => f.Lorem.Sentence())
            .RuleFor(o => o.ItemCategorization, f =>
                {
                    ItemCategorizationData itemCategorizationData = itemCategorizationDataFaker.Generate();
                    return itemCategorizationData;
                }
            );
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
