using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SpreadIt.Repository.Models
{
    public partial class CommentReport
    {
        public int Id { get; set; }
        [Required]
        [ForeignKey("Comment")]
        public int CommentId { get; set; }
        [MaxLength(3000)]
        public string Message { get; set; }
        public DateTime CreatedDate { get; set; }
        [Required]
        [ForeignKey("ReportCategory")]
        public int ReportCategoryId { get; set; }
        public ReportCategory ReportCategory { get; set; }
        public bool IsActive { get; set; }
        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public CommentReport()
        {
            CreatedDate = DateTime.Now;
        }
    }
}
