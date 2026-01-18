using System;
using Microsoft.Data.Sqlite;
using LegacyOrderService.Models;

namespace LegacyOrderService.Data
{
    public class OrderRepository : IOrderRepository
    {
        private string _connectionString = $"Data Source={Path.Combine(AppContext.BaseDirectory, "orders.db")}";

        public Task SaveAsync(Order order)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = @"
                INSERT INTO Orders (CustomerName, ProductName, Quantity, Price)
                VALUES (@customerName, @productName, @quantity, @price)";

            command.Parameters.AddWithValue("@customerName", order.CustomerName);
            command.Parameters.AddWithValue("@productName", order.ProductName);
            command.Parameters.AddWithValue("@quantity", order.Quantity);
            command.Parameters.AddWithValue("@price", order.Price);

            return command.ExecuteNonQueryAsync();
        }

        public void SeedBadData()
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            using var cmd = connection.CreateCommand();
            cmd.CommandText = "INSERT INTO Orders (CustomerName, ProductName, Quantity, Price) VALUES (@name, @product, @qty, @price)";
            cmd.Parameters.AddWithValue("@name", "John");
            cmd.Parameters.AddWithValue("@product", "Widget");
            cmd.Parameters.AddWithValue("@qty", 9999);
            cmd.Parameters.AddWithValue("@price", 9.99);
            cmd.ExecuteNonQuery();
        }
    }
}
