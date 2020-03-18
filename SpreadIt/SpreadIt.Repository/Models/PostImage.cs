using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SpreadIt.Repository.Models
{
    public partial class PostImage
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string Path { get; set; }

    }
}
