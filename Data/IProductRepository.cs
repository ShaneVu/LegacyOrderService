// Data/ProductRepository.cs
namespace LegacyOrderService.Data
{
    public interface IProductRepository
    {
        Task<double> GetPriceAsync(string productName);
    }
}