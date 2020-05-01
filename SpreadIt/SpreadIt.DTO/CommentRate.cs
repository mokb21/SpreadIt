using System;
using System.Collections.Generic;
using System.Text;

namespace SpreadIt.DTO
{
    public class CommentRate
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int CommentId { get; set; }
        public byte Status { get; set; }
    }
}