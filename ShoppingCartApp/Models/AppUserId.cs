namespace ShoppingCartApp.Models
{
    public class AppUserId
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        //public string Role { get; set; }
        // Linking reference to the shopping carts for the FK.
        public virtual List<ShoppingCart> ShoppingCarts { get; set; }
    }
}
