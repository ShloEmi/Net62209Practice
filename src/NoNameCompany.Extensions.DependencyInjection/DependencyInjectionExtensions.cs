using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel;
using System.Windows;

namespace NoNameCompany.Extensions.DependencyInjection;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddSingletonView<TViewModel, TView>(this IServiceCollection serviceCollection) 
        where TViewModel : class 
        where TView : class
    {
        serviceCollection.AddSingleton<TViewModel>();
        serviceCollection.AddSingleton<TView>(); /* TODO: Shlomi, make this work: provider => (provider.GetRequiredService<TViewModel>() as TView)! */

        return serviceCollection;
    }

    public static TView GetView<TViewModel, TView>(this IServiceProvider serviceCollection) 
        where TViewModel : INotifyPropertyChanged 
        where TView : FrameworkElement
    {
        FrameworkElement view = serviceCollection.GetRequiredService<TView>();
        view.DataContext = serviceCollection.GetRequiredService<TViewModel>();

        return (view as TView)!;
    }

}