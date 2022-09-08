using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Windows;

namespace Net62209Practice.App.Wpf;

public partial class App
{
    private void Bootstrapper(object sender, StartupEventArgs args)
    {
        Register(args);

        ShowMainUi();
    }


    private static void ShowMainUi() => 
        new MainWindow().Show();

    private static void Register(StartupEventArgs args)
    {
        using IHost host = Host
            .CreateDefaultBuilder(args.Args)
            .ConfigureServices((_, services) =>
                services.AddTransient<TransientDisposable>()
                    .AddScoped<ScopedDisposable>()
                    .AddSingleton<SingletonDisposable>())
            .Build();
    }
}



public sealed class TransientDisposable : IDisposable
{
    public void Dispose() => Console.WriteLine($"{nameof(TransientDisposable)}.Dispose()");
}

public sealed class ScopedDisposable : IDisposable
{
    public void Dispose() => Console.WriteLine($"{nameof(ScopedDisposable)}.Dispose()");
}

public sealed class SingletonDisposable : IDisposable
{
    public void Dispose() => Console.WriteLine($"{nameof(SingletonDisposable)}.Dispose()");
}