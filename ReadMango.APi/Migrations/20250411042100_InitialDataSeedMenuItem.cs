using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ReadMango.APi.Migrations
{
    /// <inheritdoc />
    public partial class InitialDataSeedMenuItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "MenuItems",
                columns: new[] { "Id", "Category", "Description", "Image", "Name", "Price", "SepcialTag" },
                values: new object[,]
                {
                    { 1, "Appetizer", "Crispy golden rolls filled with a savory mix of vegetables and spices, served with tangy chili sauce.", "https://redmangoimg.blob.core.windows.net/redmangoimg/spring roll.jpg", "Spring Roll", 120.0, "" },
                    { 2, "Appetizer", "Soft, fluffy steamed rice cakes served with coconut chutney and spicy sambar.", "https://redmangoimg.blob.core.windows.net/redmangoimg/idli.jpg", "Idli", 100.0, "" },
                    { 3, "Appetizer", "Crunchy puris filled with spicy tangy water, mashed potatoes, and chutneys – a burst of flavors in every bite.", "https://redmangoimg.blob.core.windows.net/redmangoimg/pani puri.jpg", "Pani Puri", 90.0, "Best Seller" },
                    { 4, "Entrée", "Stir-fried noodles tossed with colorful veggies and Asian sauces for that perfect Indo-Chinese flavor.", "https://redmangoimg.blob.core.windows.net/redmangoimg/hakka noodles.jpg", "Hakka Noodles", 160.0, "" },
                    { 5, "Entrée", "Creamy tomato-based gravy with soft paneer and potato dumplings, rich in flavor and spices.", "https://redmangoimg.blob.core.windows.net/redmangoimg/malai kofta.jpg", "Malai Kofta", 200.0, "Top Rated" },
                    { 6, "Entrée", "Fusion delight with spicy paneer toppings, loaded cheese, and Indian herbs on a crispy base.", "https://redmangoimg.blob.core.windows.net/redmangoimg/paneer pizza.jpg", "Paneer Pizza", 180.0, "" },
                    { 7, "Entrée", "Chunks of paneer marinated in spices and grilled to perfection, served with mint chutney.", "https://redmangoimg.blob.core.windows.net/redmangoimg/paneer tikka.jpg", "Paneer Tikka", 220.0, "Chef's Special" },
                    { 8, "Dessert", "Classic Gajar ka Halwa made with grated carrots, milk, ghee, and dry fruits.", "https://redmangoimg.blob.core.windows.net/redmangoimg/carrot love.jpg", "Carrot Love", 100.0, "" },
                    { 9, "Dessert", "Soft and spongy paneer balls soaked in rich saffron and cardamom flavored milk.", "https://redmangoimg.blob.core.windows.net/redmangoimg/rasmalai.jpg", "Rasmalai", 120.0, "Chef's Special" },
                    { 10, "Dessert", "Warm and soft sweet rolls brushed with ghee and filled with jaggery-coconut mixture.", "https://redmangoimg.blob.core.windows.net/redmangoimg/sweet rolls.jpg", "Sweet Rolls", 90.0, "Top Rated" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "MenuItems",
                keyColumn: "Id",
                keyValue: 10);
        }
    }
}
