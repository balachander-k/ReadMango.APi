using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ReadMango.APi.Models;

namespace ReadMango.APi.Data
{

    public class ApplicationDbContext:IdentityDbContext<Applicationuser>
    {
        public ApplicationDbContext(DbContextOptions options):base(options) { }
        

        public DbSet<Applicationuser> Applicationusers { get; set; }

        public DbSet<MenuItem> MenuItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<MenuItem>().HasData(
                new MenuItem
                {
                    Id = 1,
                    Name = "Spring Roll",
                    Description = "Crispy golden rolls filled with a savory mix of vegetables and spices, served with tangy chili sauce.",
                    Image = "https://redmangoimg.blob.core.windows.net/redmangoimg/spring roll.jpg",
                    Price = 120,
                    Category = "Appetizer",
                    SepcialTag = ""

                },
new MenuItem
{
    Id = 2,
    Name = "Idli",
    Description = "Soft, fluffy steamed rice cakes served with coconut chutney and spicy sambar.",
    Image = "https://redmangoimg.blob.core.windows.net/redmangoimg/idli.jpg",
    Price = 100,
    Category = "Appetizer",
    SepcialTag = ""
},
new MenuItem
{
    Id = 3,
    Name = "Pani Puri",
    Description = "Crunchy puris filled with spicy tangy water, mashed potatoes, and chutneys – a burst of flavors in every bite.",
    Image = "https://redmangoimg.blob.core.windows.net/redmangoimg/pani puri.jpg",
    Price = 90,
    Category = "Appetizer",
    SepcialTag = "Best Seller"
},
new MenuItem
{
    Id = 4,
    Name = "Hakka Noodles",
    Description = "Stir-fried noodles tossed with colorful veggies and Asian sauces for that perfect Indo-Chinese flavor.",
    Image = "https://redmangoimg.blob.core.windows.net/redmangoimg/hakka noodles.jpg",
    Price = 160,
    Category = "Entrée",
    SepcialTag = ""
},
new MenuItem
{
    Id = 5,
    Name = "Malai Kofta",
    Description = "Creamy tomato-based gravy with soft paneer and potato dumplings, rich in flavor and spices.",
    Image = "https://redmangoimg.blob.core.windows.net/redmangoimg/malai kofta.jpg",
    Price = 200,
    Category = "Entrée",
    SepcialTag = "Top Rated"
},
new MenuItem
{
    Id = 6,
    Name = "Paneer Pizza",
    Description = "Fusion delight with spicy paneer toppings, loaded cheese, and Indian herbs on a crispy base.",
    Image = "https://redmangoimg.blob.core.windows.net/redmangoimg/paneer pizza.jpg",
    Price = 180,
    Category = "Entrée",
    SepcialTag = ""
},
new MenuItem
{
    Id = 7,
    Name = "Paneer Tikka",
    Description = "Chunks of paneer marinated in spices and grilled to perfection, served with mint chutney.",
    Image = "https://redmangoimg.blob.core.windows.net/redmangoimg/paneer tikka.jpg",
    Price = 220,
    Category = "Entrée",
    SepcialTag = "Chef's Special"
},
new MenuItem
{
    Id = 8,
    Name = "Carrot Love",
    Description = "Classic Gajar ka Halwa made with grated carrots, milk, ghee, and dry fruits.",
    Image = "https://redmangoimg.blob.core.windows.net/redmangoimg/carrot love.jpg",
    Price = 100,
    Category = "Dessert",
    SepcialTag = ""
},
new MenuItem
{
    Id = 9,
    Name = "Rasmalai",
    Description = "Soft and spongy paneer balls soaked in rich saffron and cardamom flavored milk.",
    Image = "https://redmangoimg.blob.core.windows.net/redmangoimg/rasmalai.jpg",
    Price = 120,
    Category = "Dessert",
    SepcialTag = "Chef's Special"
},
new MenuItem
{
    Id = 10,
    Name = "Sweet Rolls",
    Description = "Warm and soft sweet rolls brushed with ghee and filled with jaggery-coconut mixture.",
    Image = "https://redmangoimg.blob.core.windows.net/redmangoimg/sweet rolls.jpg",
    Price = 90,
    Category = "Dessert",
    SepcialTag = "Top Rated"
});
        }
    }
}
