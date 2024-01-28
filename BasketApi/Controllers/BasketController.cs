using BasketApi.Infrastructure.Repositories;
using BasketApi.Models;
using BasketApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BasketApi.Controllers
{
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _repository;
        private readonly IProductService _productService;

        public BasketController(
            IBasketRepository repository,
            IProductService productService)
        {
            _repository = repository;
            _productService = productService;
        }

        [HttpGet("api/v1/customers/{id:long}/baskets")]
        public async Task<IActionResult> GetBasketByIdAsync(long id)
        {
            CustomerBasket? basket = await _repository.GetBasketAsync(id);

            return Ok(basket ?? new CustomerBasket(id, new List<BasketItem>()));
        }

        [HttpPost("api/v1/customers/{id:long}/baskets")]
        public async Task<IActionResult> AddBasketItem(long id, BasketItemData basketItem)
        {
            if (basketItem.Quantity <= 0)
            {
                return BadRequest("Invalid quantity");
            }

            CustomerBasket? basket = await _repository.GetBasketAsync(id) ??
                new CustomerBasket(id, new List<BasketItem>());

            bool productAlreadyInBasket = basket.Items.Any(p => p.Id == basketItem.ProductId);

            if (productAlreadyInBasket)
            {
                basket.Items.First(p => p.Id == basketItem.ProductId).IncreaseQuantity(basketItem.Quantity);
            }
            else
            {
                ProductItem? product = await _productService.GetProductAsync(basketItem.ProductId);

                if (product is null)
                {
                    return BadRequest("Product doest not exist");
                }

                basket.Items.Add(new BasketItem(
                    product.Id,
                    product.Name,
                    product.Description,
                    product.Price,
                    basketItem.Quantity));
            }

            await _repository.UpdateBasketAsync(basket);

            return Ok();
        }

        [HttpDelete("api/v1/customers/{id:long}/baskets/{productId:int}")]
        public async Task<IActionResult> RemoveBasketItem(long id, int productId)
        {
            CustomerBasket? basket = await _repository.GetBasketAsync(id);

            if (basket is null)
            {
                return Ok();
            }

            basket.Items.RemoveAll(a => a.Id == productId);

            await _repository.UpdateBasketAsync(basket);

            return Ok();
        }
    }
}
