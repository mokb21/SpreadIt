using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SpreadIt.Repository.Models
{
    public partial class CommentReport
    {
        public int Id { get; set; }
        [MaxLength(200)]
        public string Title { get; set; }
        [MaxLength(3000)]
        public string Message { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
