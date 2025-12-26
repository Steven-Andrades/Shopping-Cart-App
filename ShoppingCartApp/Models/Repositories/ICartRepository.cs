namespace ShoppingCartApp.Models.Repositories
{
    public interface ICartRepository
    {
        ShoppingCart GetUserShoppingCart(int id);
        List<ShoppingCart> GetAllCartsForUser(int id);
        ShoppingCart GetFinalisedCartById(int id);
        ShoppingCart GetCartById(int id);
        void CreateCart(ShoppingCart cart);
        void UpdateCart(ShoppingCart cart);
        void CreateCartItem(ShoppingCartItem cartItem);
        void UpdateCartItemQUanitity(ShoppingCartItem cartItem);
        void DeleteCartItem(int id);
        void RemoveItemsFromCart(int id);
    }
}
