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
        //public ICollection<Comment> Comments { get; set; }
        //public ICollection<CommentRate> CommentRates { get; set; }
        //public ICollection<CommentReport> CommentReports { get; set; }
        //public ICollection<Post> Posts { get; set; }
        //public ICollection<PostRate> PostRates { get; set; }
        //public ICollection<PostReport> PostReports { get; set; }
    }
}