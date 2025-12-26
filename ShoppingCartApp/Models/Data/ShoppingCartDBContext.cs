using Microsoft.EntityFrameworkCore;

namespace ShoppingCartApp.Models.Data
{
    public class ShoppingCartDBContext: DbContext
    {
        public ShoppingCartDBContext(DbContextOptions options):base(options)
        {
            
        }
        
        public DbSet<Product> Products { get; set; }
        public DbSet<AppUserId> AppUsers { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<AppUserId>().HasData(
                new AppUserId
                {
                    Id = 1,
                    Email = "Admin",
                    PasswordHash = BCrypt.Net.BCrypt.EnhancedHashPassword("Password_1")
                });

            builder.Entity<ShoppingCart>().HasData(
                new ShoppingCart
                {
                    Id = 1,
                    AppUserId = 1,
                    FinalisedDate = null,
                    Total = 0.0
                }
                );

            builder.Entity<ShoppingCartItem>().HasData(
                new ShoppingCartItem
                {
                    Id = 1,
                    ShoppingCartId = 1,
                    ProductId = 1,
                    Quantity = 1
                },
                new ShoppingCartItem
                {
                    Id = 2,
                    ShoppingCartId = 1,
                    ProductId = 3,
                    Quantity = 2
                }
                );

            builder.Entity<Product>().HasData(
                new Product { Id = 1, ProductName = "Granny Smith Apples", Description = "A light green apple with a crisp, juicy texture and a distinctively tart flavor.", Unit = "1kg", Price = 5.50 },
                new Product { Id = 2, ProductName = "Fresh tomatoes", Description = "A common, red, juicy fruit used in salads, sauces, and cooking.", Unit = "500g", Price = 5.90 },
                new Product { Id = 3, ProductName = "Watermelon", Description = "A large, edible fruit with a hard rind and sweet, juicy deep red to pink flesh, known for its high water content.", Unit = "Whole", Price = 6.60 },
                new Product { Id = 4, ProductName = "Cucumber", Description = "A long, green-skinned fruit with crisp, cool, and mildly flavored flesh, often used in salads.", Unit = "1 whole", Price = 1.90 },
                new Product { Id = 5, ProductName = "Red potato washed", Description = "Potatoes with a pink, smooth skin and creamy white, low-starch flesh, ideal for roasting or boiling.", Unit = "1kg", Price = 4.00 },
                new Product { Id = 6, ProductName = "Red tipped bananas", Description = "Sweet and creamy bananas grown using the Ecoganic® farming system, dipped in a distinctive red food-grade wax.", Unit = "1kg", Price = 4.90 },
                new Product { Id = 7, ProductName = "Red onion", Description = "An onion with purplish-red skin and a sweeter, milder flavor, often used raw in salads.", Unit = "1kg", Price = 3.50 },
                new Product { Id = 8, ProductName = "Carrots", Description = "A root vegetable, typically orange, with a crisp texture and sweet, earthy flavor, high in beta-carotene.", Unit = "1kg", Price = 2.00 },
                new Product { Id = 9, ProductName = "Iceburg Lettuce", Description = "Characterized by a tight head of crisp, light green leaves and a neutral taste, popular for salads.", Unit = "1", Price = 2.50 },
                new Product { Id = 10, ProductName = "Helga's Wholemeal", Description = "Traditional sliced bread made with 60% wholemeal wheat flour, a source of wholegrains, fibre, and protein.", Unit = "1", Price = 3.70 },
                new Product { Id = 11, ProductName = "Free range chicken", Description = "Chicken meat from birds allowed to roam freely outdoors for at least part of the day.", Unit = "1kg", Price = 7.50 },
                new Product { Id = 12, ProductName = "Manning Valley 6-pk", Description = "Six large, free-range eggs produced by hens that roam with access to the outdoors.", Unit = "6 eggs", Price = 3.60 },
                new Product { Id = 13, ProductName = "A2 light milk", Description = "Milk containing only the A2 type of beta-casein protein, with a reduced fat content (light).", Unit = "1 litre", Price = 2.90 },
                new Product { Id = 14, ProductName = "Chobani Strawberry Yoghurt", Description = "A single-serve Greek yogurt, typically high in protein, with a sweet strawberry flavor.", Unit = "1", Price = 1.50 },
                new Product { Id = 15, ProductName = "Lurpak Salted Blend", Description = "A spreadable blend of butter and vegetable oil (like rapeseed oil) with a slightly salty taste.", Unit = "250g", Price = 5.00 },
                new Product { Id = 16, ProductName = "Bega Farmers Tasty", Description = "A block of Bega brand cheese, typically a mature or vintage cheddar, known for its strong, full flavor.", Unit = "250g", Price = 4.00 },
                new Product { Id = 17, ProductName = "Babybel Mini", Description = "Small, round, semisoft cheese portions encased in a distinctive red wax shell.", Unit = "100g", Price = 4.20 },
                new Product { Id = 18, ProductName = "Cobram EVOO", Description = "Extra Virgin Olive Oil from the Cobram Estate brand, known for its high quality.", Unit = "375ml", Price = 8.00 },
                new Product { Id = 19, ProductName = "Heinz Tomato Soup", Description = "A classic, smooth, condensed soup made primarily from sun-ripened tomatoes.", Unit = "535g", Price = 2.50 },
                new Product { Id = 20, ProductName = "John West Tuna can", Description = "Tuna chunks preserved in sunflower oil or brine, rich in protein and a source of omega-3 fatty acids.", Unit = "95g", Price = 1.50 },
                new Product { Id = 21, ProductName = "Cadbury Dairy Milk", Description = "A brand of milk chocolate known for its creamy texture.", Unit = "200g", Price = 5.00 },
                new Product { Id = 22, ProductName = "Coca Cola", Description = "A globally recognized carbonated soft drink with a characteristic sweet, caramel-colored, bubbly flavor.", Unit = "2 litre", Price = 2.85 },
                new Product { Id = 23, ProductName = "Smith's Original Share Pack Crisps", Description = "Crinkle cut potato chips with a classic original (salted) flavor.", Unit = "170g", Price = 3.29 },
                new Product { Id = 24, ProductName = "Birds Eye Fish Fingers", Description = "Rectangular portions of white fish (often cod or pollock) coated in breadcrumbs, popular for quick meals.", Unit = "375g", Price = 4.50 },
                new Product { Id = 25, ProductName = "Berri Orange Juice", Description = "A brand of orange juice, likely a blend or juice from concentrate.", Unit = "2 litre", Price = 6.00 },
                new Product { Id = 26, ProductName = "Vegemite", Description = "A thick, dark-brown Australian spread made from brewer's yeast extract with a strong, salty, umami flavor.", Unit = "380g", Price = 6.00 },
                new Product { Id = 27, ProductName = "Cheddar Shapes", Description = "A type of baked savory biscuit known for their crunchy texture and baked-in cheddar cheese flavor.", Unit = "175g", Price = 3.00 },
                new Product { Id = 28, ProductName = "Colgate Total Toothpaste Original", Description = "A toothpaste offering complete oral health protection, including against plaque, tartar, and gingivitis.", Unit = "110g", Price = 4.50 },
                new Product { Id = 29, ProductName = "Milo Choclate Malt", Description = "A low GI malt powder drink with a delicious choc malt flavor, made from milk powder, malt barley, sugar, and cocoa.", Unit = "460g", Price = 4.90 },
                new Product { Id = 30, ProductName = "Weet Bix Saniatarium Organic", Description = "Breakfast cereal made from 97% certified organic whole wheat, low in fat and sugar, and a source of fibre.", Unit = "750g", Price = 4.00 },
                new Product { Id = 31, ProductName = "Lindt Excellence 70% Cocoa Block", Description = "Dark chocolate expertly crafted with 70% cocoa, boasting deep roasted cocoa flavors and a smooth, refined texture.", Unit = "100g", Price = 3.00 },
                new Product { Id = 32, ProductName = "Original Tim Tams Choclate", Description = "An iconic Australian chocolate biscuit: two malted biscuits separated by cream and coated in chocolate.", Unit = "200g", Price = 3.80 },
                new Product { Id = 33, ProductName = "Philadeplhia Original Cream Cheese", Description = "A deliciously creamy, full-flavored, block-style cream cheese made with fresh milk and real cream.", Unit = "250g", Price = 4.00 },
                new Product { Id = 34, ProductName = "Moccana Classic Instant Medium Roast", Description = "A full-bodied, richly aromatic instant coffee with a rounded, balanced medium roast flavor.", Unit = "100g", Price = 8.50 },
                new Product { Id = 35, ProductName = "Capilano Squeezeable Honey", Description = "Pure Australian honey packaged in a convenient squeeze-bottle dispenser.", Unit = "375g", Price = 6.50 },
                new Product { Id = 36, ProductName = "Nutella jar", Description = "An iconic smooth and creamy hazelnut chocolate spread, crafted with roasted hazelnuts and rich cocoa.", Unit = "220g", Price = 4.00 },
                new Product { Id = 37, ProductName = "Arnott's Scotch Finger", Description = "A popular rectangular sweet biscuit from Arnott's, known for its rich, buttery, and melt-in-your-mouth shortbread texture.", Unit = "250g", Price = 3.00 },
                new Product { Id = 38, ProductName = "South Cape Greek Feta", Description = "A creamy white cheese with a porous texture, a sharp, salty, and milky flavor, often sold in brine.", Unit = "150g", Price = 4.50 },
                new Product { Id = 39, ProductName = "Sacla Pasta Tomato Basil Sauce", Description = "A pasta sauce featuring whole Italian cherry tomatoes and fresh basil in a soffritto base of carrots, onions, and celery.", Unit = "680g", Price = 6.00 },
                new Product { Id = 40, ProductName = "Primo English Ham", Description = "English style leg ham, cured and seasoned, offering a savory deli meat.", Unit = "100g", Price = 3.50 },
                new Product { Id = 41, ProductName = "Primo Short cut rindless Bacon", Description = "Bacon made from the back loin, cured and smoked, with a rich sweet and salty taste, and naturally rindless.", Unit = "250g", Price = 7.00 },
                new Product { Id = 42, ProductName = "Golden Circle Pineapple Pieces in natural juice", Description = "Canned pineapple pieces preserved in natural pineapple juice.", Unit = "425g", Price = 1.50 },
                new Product { Id = 43, ProductName = "San Remo Linguine Pasta No 1", Description = "A thin, flat ribbon style pasta (No. 1), made from Durum Wheat Semolina, great with pesto or cream-based sauces.", Unit = "500g", Price = 3.00 }
                );
        }
    }
}
