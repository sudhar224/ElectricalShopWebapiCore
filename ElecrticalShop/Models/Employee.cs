using System.ComponentModel.DataAnnotations;

namespace ElecrticalShop.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string EmployeeName { get; set; }
        [Required]
        public DateTime JoinDate { get; set; }
        [Required]
        [MaxLength(50)]
        public string Gender { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        [MaxLength(150)]
        public string Address { get; set; }
    }
}
