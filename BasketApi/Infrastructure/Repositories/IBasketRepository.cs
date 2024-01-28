using BasketApi.Models;

namespace BasketApi.Infrastructure.Repositories
{
    public interface IBasketRepository
    {
        Task<CustomerBasket?> GetBasketAsync(long customerId);
        Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket);
    }
}
