using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SpreadIt.Repository.Models
{
    public partial class MessageLog
    {
        public int Id { get; set; }
        [Required]
        public byte Project { get; set; }
        [Required]
        [MaxLength(300)]
        public string Method { get; set; }
        [Required]
        [MaxLength(5000)]
        public string Message { get; set; }
    }
}
