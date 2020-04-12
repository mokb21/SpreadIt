using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SpreadIt.Repository.Models
{
    public partial class CommentReport
    {
        public CommentReport()
        {
            CreatedDate = DateTime.Now;
        }
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
        public bool IsActive { get; set; }
    }
}
