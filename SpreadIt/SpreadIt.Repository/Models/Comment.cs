using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public ICollection<CommentRate> CommentRates { get; set; }
        public ICollection<CommentReport> CommentReports { get; set; }
    }
}
