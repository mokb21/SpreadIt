using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SpreadIt.Repository.Models
{
    public partial class Comment
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(3000)]
        public string Text { get; set; }
        public bool IsBlocked { get; set; }
        public bool IsDeleted { get; set; }
        [Required]
        [ForeignKey("Post")]
        public int PostId { get; set; }
        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public ICollection<CommentRate> CommentRates { get; set; }
        public ICollection<CommentReport> CommentReports { get; set; }
    }
}
