namespace BasketApi.Models
{
    public record CustomerBasket(
    long BuyerId,
    List<BasketItem> Items)
    {
        public double Total => Items.Sum(p => p.Total);
    }
}
