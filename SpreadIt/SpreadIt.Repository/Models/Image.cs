using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SpreadIt.Repository.Models
{
    public partial class Image
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string Path { get; set; }

    }
}
