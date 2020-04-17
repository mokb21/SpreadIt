using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SpreadIt.Repository.Models
{
    public partial class PostImage
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(500)]
        public string Path { get; set; }
        [Required]
        [ForeignKey("Post")]
        public int PostId { get; set; }
    }
}
