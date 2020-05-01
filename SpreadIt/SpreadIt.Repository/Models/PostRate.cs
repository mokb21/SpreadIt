using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SpreadIt.Repository.Models
{
    public partial class PostRate
    {
        public int Id { get; set; }
        [Required]
        public byte Status { get; set; }
        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
