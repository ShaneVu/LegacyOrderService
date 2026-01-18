// Data/ProductRepository.cs
using System;
using System.Collections.Generic;
using System.Threading;

namespace LegacyOrderService.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly Dictionary<string, double> _productPrices = new()
        {
            ["Widget"] = 12.99,
            ["Gadget"] = 15.49,
            ["Doohickey"] = 8.75
        };

        public Task<double> GetPriceAsync(string productName)
        {
            if (_productPrices.TryGetValue(productName, out var price))
                return Task.FromResult(price);

            throw new KeyNotFoundException($"Product '{productName}' not found");
        }
    }
}
