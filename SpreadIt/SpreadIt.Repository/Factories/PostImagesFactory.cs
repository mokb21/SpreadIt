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
        string BaseHostingUrl = "http://eyadakkad-001-site4.btempurl.com/";
        public PostImagesFactory()
        {

        }

        public DTO.PostImage CreatePostImage(PostImage postImage)
        {
            return new DTO.PostImage()
            {
                Id = postImage.Id,
                Path = BaseHostingUrl + postImage.Path.Replace("\\", "/"),
                PostId = postImage.PostId
            };
        }
        //tps://media.mkiosk.net/categoriesCover/Luxury-bl.png


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
