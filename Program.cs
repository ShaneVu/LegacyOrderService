using System;
using LegacyOrderService.Models;
using LegacyOrderService.Data;

namespace LegacyOrderService
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Welcome to Order Processor!");
                Console.WriteLine("Enter customer name:");
                string name = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(name))
                {
                    Console.WriteLine("Error: Customer name cannot be empty.");
                    return;
                }

                Console.WriteLine("Enter product name:");
                string product = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(product))
                {
                    Console.WriteLine("Error: Product name cannot be empty.");
                    return;
                }

                var productRepo = new ProductRepository();
                double price = productRepo.GetPrice(product);

                Console.WriteLine("Enter quantity:");
                string qtyInput = Console.ReadLine();
                if (!int.TryParse(qtyInput, out int qty) || qty <= 0)
                {
                    Console.WriteLine("Error: Quantity must be a positive number.");
                    return;
                }

                Console.WriteLine("Processing order...");

                Order order = new Order();
                order.CustomerName = name;
                order.ProductName = product;
                order.Quantity = qty;
                order.Price = price;

                double total = order.Quantity * order.Price;

                Console.WriteLine("Order complete!");
                Console.WriteLine("Customer: " + order.CustomerName);
                Console.WriteLine("Product: " + order.ProductName);
                Console.WriteLine("Quantity: " + order.Quantity);
                Console.WriteLine("Total: $" + total);

                Console.WriteLine("Saving order to database...");
                var repo = new OrderRepository();
                repo.Save(order);
                Console.WriteLine("Done.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
