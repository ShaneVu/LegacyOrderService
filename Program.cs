using LegacyOrderService.Data;
using LegacyOrderService.Models;
using LegacyOrderService.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace LegacyOrderService
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            var serviceProvider = ConfigureServices(configuration);

            var consoleUI = serviceProvider.GetRequiredService<IConsoleOrderUI>();
            await consoleUI.ProcessOrderAsync();
        }

        private static ServiceProvider ConfigureServices(IConfiguration configuration)
        {
            var services = new ServiceCollection();

            services.AddSingleton(configuration);

            var connectionString = configuration.GetConnectionString("OrdersDatabase")
                ?? $"Data Source={Path.Combine(AppContext.BaseDirectory, "orders.db")}";

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IOrderRepository>(provider => new OrderRepository(connectionString));
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IConsoleOrderUI, ConsoleOrderUI>();

            return services.BuildServiceProvider();
        }
    }
}
