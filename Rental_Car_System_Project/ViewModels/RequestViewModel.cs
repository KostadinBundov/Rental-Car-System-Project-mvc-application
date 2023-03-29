using Rental_Car_System_Project.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Rental_Car_System_Project.ViewModels
{
    public class RequestViewModel
    {
        public int CarId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime PickUpDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DropOffDate { get; set; }
    }
}
