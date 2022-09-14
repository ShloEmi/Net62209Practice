using Bogus;
using CommunityToolkit.Mvvm.Input;
using ControlzEx.Theming;
using NoNameCompany.IMS.Data.ApplicationData;
using System;
using System.Collections.Generic;
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

    private readonly IDAL dataAccessLayer;

    private readonly Faker<ItemData>? itemDataFaker;


    /* TODO: Shlomi, register DAL! */
    public MainWindowViewModel(IDAL dataAccessLayer)
    {
        this.dataAccessLayer = dataAccessLayer;
        AddItemsCommand = new RelayCommand<int>(AddItemsExecute, AddItemsCanExecute);

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


    private void AddItemsExecute(int count) => 
        dataAccessLayer.AddItemsBulk(Enumerable.Range(1, count).Select(i => itemDataFaker!.Generate()).ToArray());

    private bool AddItemsCanExecute(int count) => 
        dataAccessLayer.CanAddItems();



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

public interface IDAL
{
    bool CanAddItems();
    void AddItemsBulk(IEnumerable<ItemData> items);
}

internal class Dal : IDAL
{
    public bool CanAddItems() => true;
    public void AddItemsBulk(IEnumerable<ItemData> items)
    {
        throw new NotImplementedException();
    }
}