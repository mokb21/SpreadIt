using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SpreadIt.Repository.Models
{
    public partial class Location
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        [Required]
        public double Longitude { get; set; }
        [Required]
        public double Latitude { get; set; }
        [Required]
        public double Range { get; set; }
        public ICollection<UserLocation> UserLocations { get; set; }
    }
}
