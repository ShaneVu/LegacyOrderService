using LegacyOrderService.Data;
using LegacyOrderService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyOrderService.Services
{
    public class ConsoleOrderUI : IConsoleOrderUI
    {
        private readonly IOrderService _orderService;

        public ConsoleOrderUI(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task ProcessOrderAsync()
        {
            Console.WriteLine("Welcome to Order Processor!");

            var customerName = GetInput("Customer name", "Enter customer name:");
            var productName = GetInput("Product name", "Enter product name:");
            var quantity = GetQuantityInput("Enter quantity:");

            if (!quantity.HasValue)
                return;

            Console.WriteLine("Processing order...");

            var result = await _orderService.CreateOrderAsync(customerName, productName, quantity.Value);

            if (result.Success && result.Order != null)
            {
                var order = result.Order;
                Console.WriteLine("Order complete!");
                Console.WriteLine($"Customer: {order.CustomerName}");
                Console.WriteLine($"Product: {order.ProductName}");
                Console.WriteLine($"Unit Price: ${order.Price:F2}");
                Console.WriteLine($"Quantity: {order.Quantity}");
                Console.WriteLine($"Total: ${order.TotalPrice:F2}");
                Console.WriteLine("Order saved to database.");
            }
            else
            {
                Console.WriteLine($"Error: {result.ErrorMessage}");
            }
        }

        private static string GetInput(string fieldName, string prompt)
        {
            Console.WriteLine(prompt);
            var input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine($"{fieldName} cannot be empty. Please try again.");
                return GetInput(fieldName, prompt);
            }
            return input;
        }

        private static int? GetQuantityInput(string prompt)
        {
            Console.WriteLine(prompt);
            var input = Console.ReadLine();

            if (int.TryParse(input, out var quantity) && quantity > 0)
                return quantity;

            Console.WriteLine("Invalid quantity. Please enter a positive number.");
            return null;
        }
    }
}
