using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GarduationDashbord.Models
{
    public class Hotel
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter Name Plz")]
        public string Name { get; set; }
       
        public string? Image { get; set; }
        public int? Rating { get; set; }
        public string? ScialMedia { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Longitude { get; set; }
        public string? Latitude { get; set; }
        [ForeignKey("city")]
        public int? CityId { get; set; }
        public City? city { get; set; }
    }
}
