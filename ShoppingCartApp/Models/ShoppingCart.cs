namespace ShoppingCartApp.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public int AppUserId { get; set; } 
        public DateTime? FinalisedDate { get; set; }
        public double Total { get; set; }

        // Linking reference to the User and Cart items models/tables.
        public virtual AppUserId? User { get; set; }
        public virtual List<ShoppingCartItem>? CartItems { get; set; }
    }
}
