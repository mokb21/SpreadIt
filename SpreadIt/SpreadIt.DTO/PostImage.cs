using System;
using System.Collections.Generic;
using System.Text;

namespace SpreadIt.DTO
{
    public class PostImage
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public int PostId { get; set; }
    }
}
