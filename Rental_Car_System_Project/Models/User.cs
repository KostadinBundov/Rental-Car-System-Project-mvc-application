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
        public string PIN { get; set; }
    }
}
