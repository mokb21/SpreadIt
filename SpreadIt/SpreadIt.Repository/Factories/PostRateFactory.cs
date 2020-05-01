using SpreadIt.Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpreadIt.Repository.Factories
{
    public class PostRateFactory
    {
        public DTO.PostRate CreatePostRate(PostRate postRate)
        {
            return new DTO.PostRate()
            {
                Id = postRate.Id,
                PostId = postRate.PostId,
                UserId = postRate.UserId,
                Status = postRate.Status
            };
        }


        public PostRate CreatePostRate(DTO.PostRate postRate)
        {
            return new PostRate()
            {
                Id = postRate.Id,
                PostId = postRate.PostId,
                UserId = postRate.UserId,
                Status = postRate.Status
            };
        }
    }
}
