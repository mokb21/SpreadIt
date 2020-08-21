using SpreadIt.Repository.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using SpreadIt.Repository;

namespace SpreadIt.Repository.Factories
{
    public class PostFactory
    {
        PostImagesFactory _imageFactory;
        CategoryFactory _categoryFactory;
        AccountFactory _accountFactory;
        CommentFactory _commentFactory;

        public PostFactory()
        {
            _imageFactory = new PostImagesFactory();
            _categoryFactory = new CategoryFactory();
            _accountFactory = new AccountFactory();
            _categoryFactory = new CategoryFactory();
        }

        public DTO.Post CreatePost(Post post, string userId)
        {
            return new DTO.Post()
            {
                Id = post.Id,
                CreatedDate = post.CreatedDate,
                LastUpdatedDate = post.LastUpdatedDate,
                Description = post.Description,
                Latitude = post.Latitude,
                Longitude = post.Longitude,
                UserId = post.UserId,
                IsBlocked = post.IsBlocked,
                IsDeleted = post.IsDeleted,
                CategoryId = post.CategoryId,
                PostImages = post.PostImages?.Select(element => _imageFactory.CreatePostImage(element)).ToList(),
                Category = post.Category != null ? _categoryFactory.CreateCategory(post.Category) : null,
                User = post.User != null ? _accountFactory.CreateAccount(post.User) : null,

                TotalLikes = post.PostRates == null ? 0 :
                    post.PostRates.Where(a => a.Status == (byte)Constants.RatingStatus.Like).Count(),

                TotalDisLikes = post.PostRates == null ? 0 :
                    post.PostRates.Where(a => a.Status == (byte)Constants.RatingStatus.Dislike).Count(),

                IsLiked = !string.IsNullOrEmpty(userId) ? post.PostRates.FirstOrDefault(a => a.UserId == userId)?
                    .Status == (byte)Constants.RatingStatus.Like : false,

                IsDisLiked = !string.IsNullOrEmpty(userId) ? post.PostRates.FirstOrDefault(a => a.UserId == userId)?
                    .Status == (byte)Constants.RatingStatus.Dislike : false,

                Comments = post.Comments != null && post.Comments.Count > 0 ? post.Comments
                    .Select(a => _commentFactory.CreateComment(a, userId)).ToList() : new List<DTO.Comment>(),

                DateFormated = post.LastUpdatedDate.HasValue ? post.LastUpdatedDate.Value.ToString("yyyy-MM-dd | HH:mm")
                    :post.CreatedDate.ToString("yyyy-MM-dd | HH:mm")
            };
        }

        public Post CreatePost(DTO.Post post)
        {
            return new Post()
            {
                Id = post.Id,
                Description = post.Description,
                Latitude = post.Latitude,
                Longitude = post.Longitude,
                CategoryId = post.CategoryId,
                UserId = post.UserId,
                IsBlocked = post.IsBlocked,
                IsDeleted = post.IsDeleted,
                CreatedDate = post.CreatedDate == DateTime.MinValue ? DateTime.Now : post.CreatedDate,
            };
        }

        public object CreateDataShapedObject(Post post, string userId, List<string> lstOfFields)
        {
            return CreateDataShapedObject(CreatePost(post, userId), lstOfFields);
        }


        public object CreateDataShapedObject(DTO.Post post, List<string> lstOfFields)
        {

            if (!lstOfFields.Any())
            {
                return post;
            }
            else
            {
                // create a new ExpandoObject & dynamically create the properties for this object

                ExpandoObject objectToReturn = new ExpandoObject();
                foreach (var field in lstOfFields)
                {
                    // need to include public and instance, b/c specifying a binding flag overwrites the
                    // already-existing binding flags.

                    var fieldValue = post.GetType()
                        .GetProperty(field, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance)
                        .GetValue(post, null);

                    // add the field to the ExpandoObject
                    ((IDictionary<String, Object>)objectToReturn).Add(field, fieldValue);
                }

                return objectToReturn;
            }
        }

    }
}
