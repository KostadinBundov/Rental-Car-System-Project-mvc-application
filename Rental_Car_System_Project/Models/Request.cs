using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Rental_Car_System_Project.Models
{
    public class Request
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int CarId { get; set; }
        public virtual Car Car { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime PickUpDate { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DropOffDate { get; set; }

        [Required]
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
