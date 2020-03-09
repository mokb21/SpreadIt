using System;
using System.Collections.Generic;
using System.Text;

namespace SpreadIt.DTO
{
    public class Post
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public string Description { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int? LocationId { get; set; }
    }
}
