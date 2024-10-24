using System.ComponentModel.DataAnnotations;

namespace ElecrticalShop.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string ProductName { get; set; }
        [Required]
        [MaxLength(200)]
        public string ProductDescription { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int Stock { get; set; }


    }
}
