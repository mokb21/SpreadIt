using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SpreadIt.Repository.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(3000)]
        public string Name { get; set; }
    }
}
