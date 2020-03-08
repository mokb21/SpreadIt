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
    }
}
