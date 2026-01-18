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

    public record OrderResult(bool Success, Order? Order, string? ErrorMessage);
}
