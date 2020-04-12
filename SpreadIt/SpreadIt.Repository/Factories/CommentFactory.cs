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

        public CommentFactory()
        {

        }

        public DTO.Comment CreateComment(Comment comment)
        {
            return new DTO.Comment()
            {
                Id = comment.Id,
                Text = comment.Text,
                IsBlocked = comment.IsBlocked,
                IsDeleted = comment.IsDeleted,
                PostId = comment.PostId
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
                PostId = comment.PostId
            };
        }

    }
}
