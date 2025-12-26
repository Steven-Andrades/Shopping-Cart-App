using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ShoppingCartApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialBuildShoppingCartApp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingCarts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppUserId = table.Column<int>(type: "int", nullable: false),
                    FinalisedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Total = table.Column<double>(type: "float", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCarts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShoppingCarts_AppUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ShoppingCartItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShoppingCartId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCartItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShoppingCartItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShoppingCartItems_ShoppingCarts_ShoppingCartId",
                        column: x => x.ShoppingCartId,
                        principalTable: "ShoppingCarts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "Email", "PasswordHash" },
                values: new object[] { 1, "Admin", "$2a$11$5ThwsSS2DE2o3Wo6EjcpHu2vBE/w6CEwRBfQq2ALVR1nq53jk6o1O" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "Price", "ProductName", "Unit" },
                values: new object[,]
                {
                    { 1, "A light green apple with a crisp, juicy texture and a distinctively tart flavor.", 5.5, "Granny Smith Apples", "1kg" },
                    { 2, "A common, red, juicy fruit used in salads, sauces, and cooking.", 5.9000000000000004, "Fresh tomatoes", "500g" },
                    { 3, "A large, edible fruit with a hard rind and sweet, juicy deep red to pink flesh, known for its high water content.", 6.5999999999999996, "Watermelon", "Whole" },
                    { 4, "A long, green-skinned fruit with crisp, cool, and mildly flavored flesh, often used in salads.", 1.8999999999999999, "Cucumber", "1 whole" },
                    { 5, "Potatoes with a pink, smooth skin and creamy white, low-starch flesh, ideal for roasting or boiling.", 4.0, "Red potato washed", "1kg" },
                    { 6, "Sweet and creamy bananas grown using the Ecoganic® farming system, dipped in a distinctive red food-grade wax.", 4.9000000000000004, "Red tipped bananas", "1kg" },
                    { 7, "An onion with purplish-red skin and a sweeter, milder flavor, often used raw in salads.", 3.5, "Red onion", "1kg" },
                    { 8, "A root vegetable, typically orange, with a crisp texture and sweet, earthy flavor, high in beta-carotene.", 2.0, "Carrots", "1kg" },
                    { 9, "Characterized by a tight head of crisp, light green leaves and a neutral taste, popular for salads.", 2.5, "Iceburg Lettuce", "1" },
                    { 10, "Traditional sliced bread made with 60% wholemeal wheat flour, a source of wholegrains, fibre, and protein.", 3.7000000000000002, "Helga's Wholemeal", "1" },
                    { 11, "Chicken meat from birds allowed to roam freely outdoors for at least part of the day.", 7.5, "Free range chicken", "1kg" },
                    { 12, "Six large, free-range eggs produced by hens that roam with access to the outdoors.", 3.6000000000000001, "Manning Valley 6-pk", "6 eggs" },
                    { 13, "Milk containing only the A2 type of beta-casein protein, with a reduced fat content (light).", 2.8999999999999999, "A2 light milk", "1 litre" },
                    { 14, "A single-serve Greek yogurt, typically high in protein, with a sweet strawberry flavor.", 1.5, "Chobani Strawberry Yoghurt", "1" },
                    { 15, "A spreadable blend of butter and vegetable oil (like rapeseed oil) with a slightly salty taste.", 5.0, "Lurpak Salted Blend", "250g" },
                    { 16, "A block of Bega brand cheese, typically a mature or vintage cheddar, known for its strong, full flavor.", 4.0, "Bega Farmers Tasty", "250g" },
                    { 17, "Small, round, semisoft cheese portions encased in a distinctive red wax shell.", 4.2000000000000002, "Babybel Mini", "100g" },
                    { 18, "Extra Virgin Olive Oil from the Cobram Estate brand, known for its high quality.", 8.0, "Cobram EVOO", "375ml" },
                    { 19, "A classic, smooth, condensed soup made primarily from sun-ripened tomatoes.", 2.5, "Heinz Tomato Soup", "535g" },
                    { 20, "Tuna chunks preserved in sunflower oil or brine, rich in protein and a source of omega-3 fatty acids.", 1.5, "John West Tuna can", "95g" },
                    { 21, "A brand of milk chocolate known for its creamy texture.", 5.0, "Cadbury Dairy Milk", "200g" },
                    { 22, "A globally recognized carbonated soft drink with a characteristic sweet, caramel-colored, bubbly flavor.", 2.8500000000000001, "Coca Cola", "2 litre" },
                    { 23, "Crinkle cut potato chips with a classic original (salted) flavor.", 3.29, "Smith's Original Share Pack Crisps", "170g" },
                    { 24, "Rectangular portions of white fish (often cod or pollock) coated in breadcrumbs, popular for quick meals.", 4.5, "Birds Eye Fish Fingers", "375g" },
                    { 25, "A brand of orange juice, likely a blend or juice from concentrate.", 6.0, "Berri Orange Juice", "2 litre" },
                    { 26, "A thick, dark-brown Australian spread made from brewer's yeast extract with a strong, salty, umami flavor.", 6.0, "Vegemite", "380g" },
                    { 27, "A type of baked savory biscuit known for their crunchy texture and baked-in cheddar cheese flavor.", 3.0, "Cheddar Shapes", "175g" },
                    { 28, "A toothpaste offering complete oral health protection, including against plaque, tartar, and gingivitis.", 4.5, "Colgate Total Toothpaste Original", "110g" },
                    { 29, "A low GI malt powder drink with a delicious choc malt flavor, made from milk powder, malt barley, sugar, and cocoa.", 4.9000000000000004, "Milo Choclate Malt", "460g" },
                    { 30, "Breakfast cereal made from 97% certified organic whole wheat, low in fat and sugar, and a source of fibre.", 4.0, "Weet Bix Saniatarium Organic", "750g" },
                    { 31, "Dark chocolate expertly crafted with 70% cocoa, boasting deep roasted cocoa flavors and a smooth, refined texture.", 3.0, "Lindt Excellence 70% Cocoa Block", "100g" },
                    { 32, "An iconic Australian chocolate biscuit: two malted biscuits separated by cream and coated in chocolate.", 3.7999999999999998, "Original Tim Tams Choclate", "200g" },
                    { 33, "A deliciously creamy, full-flavored, block-style cream cheese made with fresh milk and real cream.", 4.0, "Philadeplhia Original Cream Cheese", "250g" },
                    { 34, "A full-bodied, richly aromatic instant coffee with a rounded, balanced medium roast flavor.", 8.5, "Moccana Classic Instant Medium Roast", "100g" },
                    { 35, "Pure Australian honey packaged in a convenient squeeze-bottle dispenser.", 6.5, "Capilano Squeezeable Honey", "375g" },
                    { 36, "An iconic smooth and creamy hazelnut chocolate spread, crafted with roasted hazelnuts and rich cocoa.", 4.0, "Nutella jar", "220g" },
                    { 37, "A popular rectangular sweet biscuit from Arnott's, known for its rich, buttery, and melt-in-your-mouth shortbread texture.", 3.0, "Arnott's Scotch Finger", "250g" },
                    { 38, "A creamy white cheese with a porous texture, a sharp, salty, and milky flavor, often sold in brine.", 4.5, "South Cape Greek Feta", "150g" },
                    { 39, "A pasta sauce featuring whole Italian cherry tomatoes and fresh basil in a soffritto base of carrots, onions, and celery.", 6.0, "Sacla Pasta Tomato Basil Sauce", "680g" },
                    { 40, "English style leg ham, cured and seasoned, offering a savory deli meat.", 3.5, "Primo English Ham", "100g" },
                    { 41, "Bacon made from the back loin, cured and smoked, with a rich sweet and salty taste, and naturally rindless.", 7.0, "Primo Short cut rindless Bacon", "250g" },
                    { 42, "Canned pineapple pieces preserved in natural pineapple juice.", 1.5, "Golden Circle Pineapple Pieces in natural juice", "425g" },
                    { 43, "A thin, flat ribbon style pasta (No. 1), made from Durum Wheat Semolina, great with pesto or cream-based sauces.", 3.0, "San Remo Linguine Pasta No 1", "500g" }
                });

            migrationBuilder.InsertData(
                table: "ShoppingCarts",
                columns: new[] { "Id", "AppUserId", "FinalisedDate", "Total", "UserId" },
                values: new object[] { 1, 1, null, 0.0, null });

            migrationBuilder.InsertData(
                table: "ShoppingCartItems",
                columns: new[] { "Id", "ProductId", "Quantity", "ShoppingCartId" },
                values: new object[,]
                {
                    { 1, 1, 1, 1 },
                    { 2, 3, 2, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartItems_ProductId",
                table: "ShoppingCartItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartItems_ShoppingCartId",
                table: "ShoppingCartItems",
                column: "ShoppingCartId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCarts_UserId",
                table: "ShoppingCarts",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShoppingCartItems");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "ShoppingCarts");

            migrationBuilder.DropTable(
                name: "AppUsers");
        }
    }
}
