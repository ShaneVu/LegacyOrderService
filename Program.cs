using LegacyOrderService.Data;
using LegacyOrderService.Models;
using LegacyOrderService.Services;
using System;

namespace LegacyOrderService
{
    class Program
    {
        static async Task Main(string[] args)
        {
            IProductRepository productRepo = new ProductRepository();
            IOrderRepository orderRepo = new OrderRepository();
            var orderService = new OrderService(productRepo, orderRepo);
            var consoleUI = new ConsoleOrderUI(orderService);
            await consoleUI.ProcessOrderAsync();
        }
    }
}
