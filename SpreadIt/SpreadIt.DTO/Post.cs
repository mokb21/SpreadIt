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
        public int CategoryId { get; set; }
        public string UserId { get; set; }
        public bool IsBlocked { get; set; }
        public bool IsDeleted { get; set; }
        public List<PostImage> PostImages { get; set; }
        public Category Category { get; set; }
        public Account User { get; set; }
        public int TotalLikes { get; set; }
        public int TotalDisLikes { get; set; }
        public bool IsLiked { get; set; }
        public bool IsDisLiked { get; set; }
        public List<Comment> Comments { get; set; } = new List<Comment>();
        public string DateFormated { get; set; }

    }
}
