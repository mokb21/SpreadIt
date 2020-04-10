using SpreadIt.Repository.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;

namespace SpreadIt.Repository.Factories
{
    public class CategoryFactory
    {
        public CategoryFactory()
        {

        }

        public DTO.Category CreateCategory(Category category)
        {
            return new DTO.Category()
            {
                Id = category.Id,
                Name = category.Name,
            };
        }



        public Category CreateCategory(DTO.Category category)
        {
            return new Category()
            {
                Id = category.Id,
                Name = category.Name
            };
        }

        public object CreateDataShapedObject(Category category, List<string> lstOfFields)
        {
            return CreateDataShapedObject(CreateCategory(category), lstOfFields);
        }


        public object CreateDataShapedObject(DTO.Category category, List<string> lstOfFields)
        {

            if (!lstOfFields.Any())
            {
                return category;
            }
            else
            {
                // create a new ExpandoObject & dynamically create the properties for this object

                ExpandoObject objectToReturn = new ExpandoObject();
                foreach (var field in lstOfFields)
                {
                    // need to include public and instance, b/c specifying a binding flag overwrites the
                    // already-existing binding flags.

                    var fieldValue = category.GetType()
                        .GetProperty(field, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance)
                        .GetValue(category, null);

                    // add the field to the ExpandoObject
                    ((IDictionary<String, Object>)objectToReturn).Add(field, fieldValue);
                }

                return objectToReturn;
            }
        }

    }
}
