using Microsoft.EntityFrameworkCore;
using ShoppingCartApp.Models;
using ShoppingCartApp.Models.Data;

namespace ShoppingCartApp.Models.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ShoppingCartDBContext _context;
        public ProductRepository(ShoppingCartDBContext context)
        {
            _context = context;
        }

        public void CreateProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void DeleteProduct(int id)
        {
            _context.Products.Where(p => p.Id == id).ExecuteDelete();
        }
        
        public List<Product> GetAllProducts()
        {
            var products = _context.Products.ToList();
            return products;
        }

        public Product GetProductById(int id)
        {
            var product = _context.Products.SingleOrDefault(p => p.Id == id);
            return product;
        }

        public void UpdateProduct(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();
        }
    }
}
