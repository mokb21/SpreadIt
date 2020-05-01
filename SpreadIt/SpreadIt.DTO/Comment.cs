using System;
using System.Collections.Generic;
using System.Text;

namespace SpreadIt.DTO
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public bool IsBlocked { get; set; }
        public int PostId { get; set; }
        public bool IsDeleted { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public Account User { get; set; }
    }
}
