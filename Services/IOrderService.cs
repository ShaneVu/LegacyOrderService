using LegacyOrderService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyOrderService.Services
{
    public interface IOrderService
    {
        Task<OrderResult> CreateOrderAsync(string customerName, string productName, int quantity);
    }

    public class OrderResult
    {
        public OrderResult(bool Success, Order? Order, string? ErrorMessage)
        {
            this.Success = Success;
            this.Order = Order;
            this.ErrorMessage = ErrorMessage;
        }

        public bool Success { get; set; }
        public Order? Order { get; set; }
        public string? ErrorMessage { get; set; }

    }
}
