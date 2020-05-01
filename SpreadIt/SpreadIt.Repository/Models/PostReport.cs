using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SpreadIt.Repository.Models
{
    public partial class PostReport
    {
        public int Id { get; set; }
        [MaxLength(3000)]
        public string Message { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        [Required]
        [ForeignKey("ReportCategory")]
        public int ReportCategoryId { get; set; }
        public ReportCategory ReportCategory { get; set; }
        [Required]
        [ForeignKey("Post")]
        public int PostId { get; set; }
        public bool IsActive { get; set; }
        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public PostReport()
        {
            CreatedDate = DateTime.Now;
        }
    }
}