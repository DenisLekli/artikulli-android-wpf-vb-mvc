using ArtikulliWpf.StartupHelpers;
using ArtikulliWpf.Views;
using ArtikullServices.Data;
using ArtikullServices.Dependencys;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;

namespace ArtikulliWpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IHost? AppHost { get; set; }
        public App()
        {
            AppHost = Host.CreateDefaultBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddFormFactory<MainWindow>();
                    services.AddFormFactory<AddProductView>();
                    services.AddFormFactory<EditProducts>();
                    services.AddRepositoryServices();
                    services.AddServicesColection();
                    services.AddDbContext<ArtikullServicesDbContext>(optionsBuilder =>
                    {
                        optionsBuilder.UseSqlServer(ArtikulliWpfConfig.GetConnectionString());
                    });
                })
                .Build();
        }
        protected override async void OnStartup(StartupEventArgs e)
        {
            await AppHost!.StartAsync();

            var startupForm = AppHost.Services.GetRequiredService<MainWindow>();
            startupForm.Show();



            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await AppHost!.StopAsync();
            base.OnExit(e);
        }
    }
}
