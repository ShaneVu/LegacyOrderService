using System;
using System.Threading.Tasks;
using LegacyOrderService.Models;
using LegacyOrderService.Data;

namespace LegacyOrderService.Services
{
    public class OrderService : IOrderService
    {
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;

        public OrderService(IProductRepository productRepository, IOrderRepository orderRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        }

        public async Task<OrderResult> CreateOrderAsync(string customerName, string productName, int quantity)
        {
            try
            {
                var price = _productRepository.GetPrice(productName);
                if (price <= 0)
                    return new OrderResult(false, null, $"Product '{productName}' not found or invalid price.");

                var order = new Order
                {
                    CustomerName = customerName,
                    ProductName = productName,
                    Quantity = quantity,
                    Price = price
                };

                _orderRepository.Save(order);

                return new OrderResult(true, order, null);
            }
            catch (Exception ex)
            {
                return new OrderResult(false, null, $"Failed to create order: {ex.Message}");
            }
        }
    }
}