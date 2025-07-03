﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GarduationDashbord.Models
{
    public class Bank
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter Bank Name Plz")]
        public string Name { get; set; }
        public string? Image { get; set; }
        public int? Rating { get; set; }
        public string? ScialMedia { get; set; }
        public string? Longitude { get; set; }
        public string? Latitude { get; set; }
        [ForeignKey("city")]
        public int CityId { get; set; }
        public City? city { get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }  


    }
}
