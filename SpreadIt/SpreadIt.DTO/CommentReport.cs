using System;
using System.Collections.Generic;
using System.Text;

namespace SpreadIt.DTO
{
    public class CommentReport
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public int CommentId { get; set; }
        public int ReportCategoryId { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }
    }
}
