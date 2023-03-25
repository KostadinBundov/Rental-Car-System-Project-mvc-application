using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Rental_Car_System_Project.Models
{
    public class Car
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string CarBrand { get; set; }

        [Required]
        public string CarModel { get; set; }

        [Required]
        public string ManufactureYear { get; set; }

        [Required]
        public int PassengerSeats { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}
