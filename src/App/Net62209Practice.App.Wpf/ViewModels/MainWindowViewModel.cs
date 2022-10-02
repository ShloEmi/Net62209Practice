﻿using Autofac;
using Bogus;
using CommunityToolkit.Mvvm.Input;
using ControlzEx.Theming;
using NoNameCompany.IMS.BL.DAL.Interfaces;
using NoNameCompany.IMS.Data.ApplicationData;
using Serilog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace NoNameCompany.IMS.App.Wpf.ViewModels;

public class MainWindowViewModel : ViewModelBase, IStartable, IDisposable
{
    private readonly CompositeDisposable disposables = new();

    private readonly IDAL dataAccessLayer;
    private readonly ILogger logger;
    private string themeSelectedItem;
    private ObservableCollection<string> availableThemes = new();
    private readonly ObservableCollection<ItemContainerData> itemContainerData = new();
    

    public MainWindowViewModel(IDAL dataAccessLayer, ILogger logger, Faker<ItemData> itemDataProvider)
    {
        logger.Information("called");

        this.dataAccessLayer = dataAccessLayer;
        this.logger = logger;


        AddItemsCommand = new RelayCommand<object>(
            count => { ExecuteAddItemsCommand(itemDataProvider, count); },
            _ => dataAccessLayer.CanAddItems());


        /* TODO: Shlomi, should be in configuration */
        availableThemes.Add("light.blue");
        availableThemes.Add("dark.blue");

        themeSelectedItem = availableThemes.First();
    }

    public void Start()
    {
        disposables.Add(
            dataAccessLayer.ItemsChanged
                .ObserveOnDispatcher()
                .Subscribe(OnItemsChanged)
        );
    }

    public void Dispose() => 
        disposables.Clear();


    
    /// <remarks>If all is ok, will continue with <see cref="OnItemsChanged"/></remarks>
    private void ExecuteAddItemsCommand(Faker<ItemData> itemDataProvider, object? count)
    {
        int howMuch = int.Parse(count?.ToString() ?? "0");
        bool result = dataAccessLayer.AddItemsBulk(
            Enumerable
                .Range(1, howMuch)
                .Select(_ => itemDataProvider.Generate()).ToArray()
        );
    }

    private void OnItemsChanged(IEnumerable<ItemChanged> datum)
    {
#if false

        foreach (ItemChanged itemChanged in datum)
        {
            Dispatcher.CurrentDispatcher.InvokeAsync(
                /*async*/ () =>
                {
                    switch (itemChanged.ChangeDescription)
                    {
                        case ChangeDescriptions.added:
                            OnItemAdded(itemChanged);
                            break;
                        case ChangeDescriptions.removed:
                            OnItemRemoved(itemChanged);
                            break;
                        case ChangeDescriptions.updated:
                            OnItemUpdated(itemChanged);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                });
        }

#else

        Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Inactive, 
            () =>
            {
                //if(lockedObject.Contains("Items");
                // lockedObject.Add("Items");
                // {...}
                // lockedObject.Remove("Items");

                foreach (ItemChanged itemChanged in datum)
                {
                    switch (itemChanged.ChangeDescription)
                    {
                        case ChangeDescriptions.added:
                            OnItemAdded(itemChanged);
                            break;
                        case ChangeDescriptions.removed:
                            OnItemRemoved(itemChanged);
                            break;
                        case ChangeDescriptions.updated:
                            OnItemUpdated(itemChanged);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }

            });
#endif
    }

    private void OnItemUpdated(ItemChanged itemChanged)
    {
        Task.Delay(30000).Wait(); /* TODO: Shlomi, changing this will take ~1 RL month */
    }

    private void OnItemRemoved(ItemChanged itemChanged)
    {
        Task.Delay(1000).Wait();
    }

    private void OnItemAdded(ItemChanged itemChanged)
    {
        Task.Delay(1000).Wait();
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
