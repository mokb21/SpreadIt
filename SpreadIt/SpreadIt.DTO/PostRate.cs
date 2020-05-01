using System;
using System.Collections.Generic;
using System.Text;

namespace SpreadIt.DTO
{
    public class PostRate
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int PostId { get; set; }
        public byte Status { get; set; }
    }
}