namespace BasketApi.Models
{
    public class BasketItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; }
        public int Quantity { get; private set; }
        public BasketItem(int id, string name, string description, double price, int quantity)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            Quantity = quantity;
        }
        public void IncreaseQuantity(int quantity)
        {
            Quantity += quantity;
        }
        public double Total => Price * Quantity;
    }
}
