using SpreadIt.Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpreadIt.Repository.Factories
{
    public class CommentRateFactory
    {
        public DTO.CommentRate CreateCommentRate(CommentRate commentRate)
        {
            return new DTO.CommentRate()
            {
                Id = commentRate.Id,
                CommentId = commentRate.CommentId,
                UserId = commentRate.UserId,
                Status = commentRate.Status
            };
        }


        public CommentRate CreateCommentRate(DTO.CommentRate commentRate)
        {
            return new CommentRate()
            {
                Id = commentRate.Id,
                CommentId = commentRate.CommentId,
                UserId = commentRate.UserId,
                Status = commentRate.Status
            };
        }
    }
}
