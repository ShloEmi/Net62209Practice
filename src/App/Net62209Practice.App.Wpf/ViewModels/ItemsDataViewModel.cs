﻿using Autofac;
using Bogus;
using CommunityToolkit.Mvvm.Input;
using NoNameCompany.IMS.BL.DAL.Interfaces;
using NoNameCompany.IMS.Data.ApplicationData;
using Serilog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Windows.Input;

namespace NoNameCompany.IMS.App.Wpf.ViewModels;

public class ItemsDataViewModel : ViewModelBase, IStartable, IDisposable
{
    // ReSharper disable once CollectionNeverQueried.Local
    private readonly CompositeDisposable disposables = new();

    private readonly IDAL dataAccessLayer;
    private readonly ILogger logger;


    public ItemsDataViewModel(IDAL dataAccessLayer, ILogger logger, Faker<ItemData> itemDataProvider)
    {
        logger.Information("called");

        this.dataAccessLayer = dataAccessLayer;
        this.logger = logger;


        AddItemsCommand = new RelayCommand<object>(
            count => { ExecuteAddItemsCommand(itemDataProvider, count); },
            _ => dataAccessLayer.CanAddItems());
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
        bool executed = dataAccessLayer.AddItemsBulk(
            Enumerable
                .Range(1, howMuch)
                .Select(_ => itemDataProvider.Generate()).ToArray()
        );

        if (!executed) 
            logger.Warning("Couldn't dataAccessLayer.AddItemsBulk for ...");
    }

    private void OnItemsChanged(IEnumerable<ItemChanged> changedItems)
    {
        foreach (ItemChanged itemChanged in changedItems)
        {
            try
            {
                switch (itemChanged.ChangeDescription)
                {
                    case ChangeDescriptions.added:
                        ItemsDataSource.Add(itemChanged.ChangedItem);
                        break;
                    case ChangeDescriptions.removed:
                        ItemsDataSource.Remove(itemChanged.ChangedItem);
                        break;
                    case ChangeDescriptions.updated:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            catch (Exception exception)
            {
                logger.Warning(exception, "Couldn't change ItemsDataSource");
            }
            finally
            {
                base.OnPropertyChanged(
                    nameof(ItemsDataSource)); /* TODO: Shlomi, this can be more easily done using Dynamic-Data: https://github.com/reactivemarbles/DynamicData#dynamic-data */
            }
        }
    }


    public ObservableCollection<ItemData> ItemsDataSource { get; } = new();

    public ICommand AddItemsCommand { get; }
}
