using Microsoft.AspNetCore.Mvc;
using ShoppingCartApp.Models;
using ShoppingCartApp.Models.Repositories;

namespace ShoppingCartApp.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly ICartRepository _repository;

        public ShoppingCartController(ICartRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            // Checks if the user's login by trying to retreive their ID from the 
            // session data.
            var userId = HttpContext.Session.GetInt32("UserId") ?? -1;
            // If no user is logged in, show unauthorised.
            if (userId <= 0)
            {
                return Unauthorized();
            }

            var cart = _repository.GetUserShoppingCart(userId);

            return PartialView("_ShoppingCartPartial", cart);
        }

        [HttpPost]
        public IActionResult AddToCart(int productId)
        {
            // Checks if the user's login by trying to retreive their ID from the 
            // session data.
            var userId = HttpContext.Session.GetInt32("UserId") ?? -1;
            // If no user is logged in, show unauthorised.
            if (userId <= 0)
            {
                return Unauthorized();
            }
            // Get the user's shopping cart.
            var cart = _repository.GetUserShoppingCart(userId);
            // If no cart exists, create one.
            var cartItem = new ShoppingCartItem
            {
                ProductId = productId,
                Quantity = 1
            };
            // If a cart exists, check if the item is already in the cart.
            if (cart == null)
            {
                // No cart exists, create a new cart with the item. 
                cart = new ShoppingCart
                {
                    AppUserId = userId,
                    CartItems = new List<ShoppingCartItem> { cartItem }
                };
                // Create the cart with the new item.
                _repository.CreateCart(cart);
            }
            // If a cart exists, check if the item is already in the cart.
            else
            {
                // Check if the Item is already in the cart.
                var item = cart.CartItems.Where(ci => ci.ProductId == productId)
                                         .FirstOrDefault();
                // If the item is not in the cart, add it.
                if (item != null)
                {
                    item.Quantity++;
                    _repository.UpdateCartItemQUanitity(item);
                }
                // If the item is not in the cart, add it.
                else
                {
                    cartItem.ShoppingCartId = cart.Id;
                    _repository.CreateCartItem(cartItem);
                }
            }
            // Return OK status.
            return Ok();
        }

        [HttpDelete]
        public IActionResult RemoveFromItem(int id)
        {
            _repository.DeleteCartItem(id);
            return Ok();
        }

        [HttpPut]
        public IActionResult UpdateItemQuantity([FromBody] ShoppingCartItem cartItem)
        {
            _repository.UpdateCartItemQUanitity(cartItem);
            return Ok();
        }
        
        public IActionResult CartHistory()
        {
            var userId = HttpContext.Session.GetInt32("UserId") ?? -1;

            if (userId <= 0)
            {
                return RedirectToAction("Login", "Authentication");
            }
            
            var carts = _repository.GetAllCartsForUser(userId);

            return View(carts);
        }
        
        public IActionResult PurchaseDetails(int id)
        {
            var cart = _repository.GetFinalisedCartById(id);
            var userId = HttpContext.Session.GetInt32("UserId") ?? -1;

            if (userId <= 0 || cart.AppUserId != userId)
            {
                return RedirectToAction("AccessDenied", "Authentication");
            }
            return View(cart);
        }

        // Finalise the cart.
        [HttpPut]
        public IActionResult FinaliseCart(int id)
        {
            var cart = _repository.GetCartById(id);
            if (cart == null)
            {
                return BadRequest();
            }
            // Calculate the total cost of the cart.
            cart.Total = CalculateCartTotal(cart);
            cart.FinalisedDate = DateTime.Now;
            // Update the cart in the database.
            _repository.UpdateCart(cart);

            return Ok();
        }

        [HttpDelete]
        public IActionResult CancelCart(int id)
        {
            _repository.RemoveItemsFromCart(id);
            return Ok();
        }

        // Helper method to calculate the total cost of the cart.
        private double CalculateCartTotal(ShoppingCart cart)
        {
            var total = 0.0;

            foreach (var item in cart.CartItems)
            {
                total += item.Quantity * item.Product.Price;
            }
            return total;
        }

    } 
}
