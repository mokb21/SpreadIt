using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SpreadIt.Repository.Models
{
    public partial class PostRate
    {
        public int Id { get; set; }
        [Required]
        public byte Status { get; set; }
    }
}
