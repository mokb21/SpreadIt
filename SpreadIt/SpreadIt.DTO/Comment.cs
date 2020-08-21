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
        public int TotalLikes { get; set; }
        public int TotalDisLikes { get; set; }
        public bool IsLiked { get; set; }
        public bool IsDisLiked { get; set; }
    }
}
