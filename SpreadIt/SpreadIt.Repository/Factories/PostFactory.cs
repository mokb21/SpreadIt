using SpreadIt.Repository.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
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
                Description = post.Description,
                Latitude = post.Latitude,
                Longitude = post.Longitude
            };
        }



        public Post CreatePost(DTO.Post post)
        {
            return new Post()
            {
                Id = post.Id,
                Description = post.Description,
                Latitude = post.Latitude,
                Longitude = post.Longitude
            };
        }

        public object CreateDataShapedObject(Post post, List<string> lstOfFields)
        {

            return CreateDataShapedObject(CreatePost(post), lstOfFields);
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
