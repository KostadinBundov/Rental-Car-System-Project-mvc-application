using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

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
        [DataType(DataType.Date)]
        public DateTime PickUpDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DropOffDate { get; set; }

        [Required]
        public string UserId { get; set; }
        public virtual User User { get; set; }

        [DefaultValue(false)]
        public bool IsRequestApproved { get; set; }
    }
}
