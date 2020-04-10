﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SpreadIt.DTO
{
    public class Post
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public string Description { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int CategoryId { get; set; }
        public bool IsBlocked { get; set; }
        public bool IsDeleted { get; set; }
        public List<PostImage> PostImages { get; set; }
        public Category Category { get; set; }
    }   
}
