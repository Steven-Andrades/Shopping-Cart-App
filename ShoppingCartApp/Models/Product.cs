using ShoppingCartApp.Models;

namespace ShoppingCartApp.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Unit { get; set; } = string.Empty;
        public double Price { get; set; }
        public virtual List<ShoppingCartItem>? CartItems { get; set; }
    }
}
