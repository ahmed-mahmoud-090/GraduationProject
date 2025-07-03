﻿using System.ComponentModel.DataAnnotations;

namespace GarduationDashbord.Models
{
    public class Type_place
    {
        [Key]
        public int Id { get; set; } 
        [Required(ErrorMessage ="Enter Name of type of place plz")]
        public string Name { get; set; }
        public ICollection<Tourismt_Place>? tourismt_Places { get; set; }
    }
}
