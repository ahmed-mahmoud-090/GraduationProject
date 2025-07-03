using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
namespace Grad.Controllers
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Enter First Name Plz")]
        public string FirstName { get; set; }
        [Required(ErrorMessage ="Enter Sacond Name Plz")]
        public string SacondName { get; set; }
        [Required(ErrorMessage ="Enter Birth Date Name Plz")]
        public DateOnly BDate { get; set; }
        [Required(ErrorMessage = "Enter Password, please.")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 100 characters.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
         ErrorMessage = "Password must include at least one uppercase letter, one lowercase letter, one number, and one special character.")]
        public string Password { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Enter a valid email address.")]
        public string Email { get; set; }
        public string ?BackgroundPhoto {  get; set; }
        public string? City {  get; set; }    
       
    }
}
