using SpreadIt.Repository.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;


namespace SpreadIt.Repository.Factories
{
    public class CommentFactory
    {
        AccountFactory _accountFactory;
        public CommentFactory()
        {
            _accountFactory = new AccountFactory();
        }

        public DTO.Comment CreateComment(Comment comment)
        {

            return new DTO.Comment()
            {
                Id = comment.Id,
                Text = comment.Text,
                IsBlocked = comment.IsBlocked,
                IsDeleted = comment.IsDeleted,
                PostId = comment.PostId,
                UserId = comment.UserId,
                User = comment.User != null ? _accountFactory.CreateAccount(comment.User) : null,

                TotalLikes = comment.CommentRates == null ? 0 :
                    comment.CommentRates.Where(a => a.Status == (byte)Constants.RatingStatus.Like).Count(),

                TotalDisLikes = comment.CommentRates == null ? 0 :
                    comment.CommentRates.Where(a => a.Status == (byte)Constants.RatingStatus.Dislike).Count(),
            };
        }


        public Comment CreateComment(DTO.Comment comment)
        {
            return new Comment()
            {
                Id = comment.Id,
                Text = comment.Text,
                IsBlocked = comment.IsBlocked,
                IsDeleted = comment.IsDeleted,
                PostId = comment.PostId,
                UserId = comment.UserId,
                CreatedDate = comment.CreatedDate == DateTime.MinValue ? DateTime.Now : comment.CreatedDate
            };
        }

    }
}
