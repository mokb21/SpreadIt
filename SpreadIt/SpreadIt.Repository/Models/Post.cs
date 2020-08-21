using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpreadIt.Repository.Models
{
    public partial class Post
    {
        public int Id { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        [MaxLength(3000)]
        public string Description { get; set; }
        public bool IsBlocked { get; set; }
        public bool IsDeleted { get; set; }
        [Required]
        public double Longitude { get; set; }
        [Required]
        public double Latitude { get; set; }
        [Required]
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        #nullable enable
        public List<PostImage> PostImages { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<PostRate> PostRates { get; set; }
        public ICollection<PostReport> PostReports { get; set; }

        public Post()
        {
            CreatedDate = DateTime.Now;
            LastUpdatedDate = DateTime.Now;
            PostImages = new List<PostImage>();
        }
    }
}
