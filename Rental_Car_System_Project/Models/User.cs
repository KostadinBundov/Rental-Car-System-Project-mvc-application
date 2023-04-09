using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Rental_Car_System_Project.Models
{
    public class User : IdentityUser
    {
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        //Personal Identificational Number
        [Required]
        [StringLength(10, ErrorMessage = "The PIN must be 10 digits long. ")]
        public string PIN { get; set; }
    }
}
