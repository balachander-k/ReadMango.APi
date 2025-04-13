using System.ComponentModel.DataAnnotations;

namespace ReadMango.APi.Models.DTO
{
    public class MenuItemUpdateDTO
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public string SepcialTag { get; set; }

        public string Category { get; set; }

        public double Price { get; set; }

        public IFormFile File { get; set; }


    }
}
