using Microsoft.Extensions.Hosting;
using System.Windows;

namespace Net62209Practice.App.Wpf
{
    public partial class App : Application
    {
        private void Bootstraper(object sender, StartupEventArgs args)
        {
            using IHost host = CreateHostBuilder(args).Build();

            new MainWindow().Show();
        }


        static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureServices((_, services) =>
            services.AddTransient<TransientDisposable>()
                    .AddScoped<ScopedDisposable>()
                    .AddSingleton<SingletonDisposable>());

    }
}
