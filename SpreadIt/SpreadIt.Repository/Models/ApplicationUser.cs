using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SpreadIt.Repository.Models
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(500)]
        public string Name { get; set; }
        [MaxLength(500)]
        public string Image { get; set; }
        public ICollection<UserLocation> UserLocations { get; set; }
    }
}