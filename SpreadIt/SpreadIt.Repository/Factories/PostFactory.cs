using SpreadIt.Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpreadIt.Repository.Factories
{
    public class PostFactory
    {
        public PostFactory()
        {

        }

        public DTO.Post CreatePost(Post post)
        {
            return new DTO.Post()
            {
                Id = post.Id,
                CreatedDate = post.CreatedDate,
                LastUpdatedDate = post.LastUpdatedDate,
                Description = post.Description
            };
        }



        public Post CreatePost(DTO.Post post)
        {
            return new Post()
            {
                Id = post.Id,
                Description = post.Description
            };
        }
    }
}
