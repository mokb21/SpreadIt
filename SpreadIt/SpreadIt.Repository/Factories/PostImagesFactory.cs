using SpreadIt.Repository.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;

namespace SpreadIt.Repository.Factories
{
    public class PostImagesFactory
    {
        public PostImagesFactory()
        {

        }

        public DTO.PostImage CreatePostImage(PostImage postImage)
        {
            return new DTO.PostImage()
            {
                Id = postImage.Id,
                Path = Constants.SpreadItConstants.APIURI + postImage.Path.Replace("\\", "/"),
                PostId = postImage.PostId
            };
        }


        public PostImage CreatePostImage(DTO.PostImage postImage)
        {
            return new PostImage()
            {
                Id = postImage.Id,
                Path = postImage.Path,
                PostId = postImage.PostId
            };
        }
    }
}
