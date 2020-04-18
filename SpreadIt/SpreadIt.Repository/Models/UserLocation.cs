using System;
using System.Collections.Generic;
using System.Text;

namespace SpreadIt.Repository.Models
{
    public class UserLocation
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int LocationId { get; set; }
        public Location Locations { get; set; }
    }
}