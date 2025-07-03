﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GarduationDashbord.Models
{
    public class TransportProvider
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter Name Plz")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Enter Type Of Provide Plz")]
        public string ProviderType { get; set; }
        public string? ContactInfo { get; set; }
        public string? Longitude { get; set; }
        public string? Latitude { get; set; }
        public float ? PriceModel { get; set; }
        [ForeignKey("city")]
        public int? CityId { get; set; }
        public City? city { get; set; }
        [ForeignKey("Type_Place")]
        public int TypePlaceID { get; set; }
        public Type_place? Type_Place { get; set; }
    }
}
