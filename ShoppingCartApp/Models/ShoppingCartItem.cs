namespace ShoppingCartApp.Models
{
    public class ShoppingCartItem
    {
        public int Id { get; set; }
        public int ShoppingCartId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        // Linking references to the Carts and Products Tables. Each cart item will
        // have 1 cart and 1 product it is associated with.
        public virtual ShoppingCart? Cart { get; set; }
        public virtual Product? Product { get; set; }
    }
}
