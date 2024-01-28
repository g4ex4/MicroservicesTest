using BasketApi.Models;

namespace BasketApi.Services
{
    public interface IProductService
    {
        Task<ProductItem?> GetProductAsync(int productId);
    }
}
