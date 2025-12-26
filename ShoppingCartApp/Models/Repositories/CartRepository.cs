using Microsoft.EntityFrameworkCore;
using ShoppingCartApp.Models;
using ShoppingCartApp.Models.Data;
using System.Security.Cryptography.Xml;

namespace ShoppingCartApp.Models.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly ShoppingCartDBContext _context;

    public CartRepository(ShoppingCartDBContext context)
        {
            _context = context;
        }

        public void CreateCart(ShoppingCart cart)
        {
            _context.ShoppingCarts.Add(cart);
            _context.SaveChanges();
        }

        public void CreateCartItem(ShoppingCartItem cartItem)
        {
            _context.ShoppingCartItems.Add(cartItem);
            _context.SaveChanges();
        }

        public void DeleteCartItem(int id)
        {
            _context.ShoppingCartItems.Where(ci => ci.Id == id).ExecuteDelete();
            _context.SaveChanges();
        }

        public List<ShoppingCart> GetAllCartsForUser(int id)
        {
            var carts = _context.ShoppingCarts.Where(c => c.AppUserId == id && c.FinalisedDate != null)
                                              .OrderByDescending(c => c.FinalisedDate)
                                              .ToList();
            return carts;
        }

        public ShoppingCart GetCartById(int id)
        {
            var cart = _context.ShoppingCarts.Where(c => c.Id == id && c.FinalisedDate == null)
                                             .Include(c => c.CartItems)
                                             .ThenInclude(ci => ci.Product)
                                             .FirstOrDefault();
            return cart;
        }

        public ShoppingCart GetFinalisedCartById(int id)
        {
            var cart = _context.ShoppingCarts.Where(c => c.Id == id && c.FinalisedDate != null)
                                             .Include(c => c.CartItems)
                                             .ThenInclude(ci => ci.Product)
                                             .FirstOrDefault();
            return cart;
        }

        public ShoppingCart GetUserShoppingCart(int id)
        {
            var cart = _context.ShoppingCarts.Where(c => c.AppUserId == id && c.FinalisedDate == null)
                                             .Include(c => c.CartItems)
                                             .ThenInclude(ci => ci.Product)
                                             .FirstOrDefault();

            return cart;
        }

        public void RemoveItemsFromCart(int id)
        {
            _context.ShoppingCartItems.Where(ci => ci.ShoppingCartId == id).ExecuteDelete();
            _context.SaveChanges();
        }

        public void UpdateCart(ShoppingCart cart)
        {
            _context.ShoppingCarts.Update(cart);
            _context.SaveChanges();
        }

        public void UpdateCartItemQUanitity(ShoppingCartItem cartItem)
        {
            // Attach the cart item tot he DB set.
            _context.ShoppingCartItems.Attach(cartItem);
            _context.Entry(cartItem).Property(ci => ci.Quantity).IsModified = true;
            _context.SaveChanges();
        }
    }
}
