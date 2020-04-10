using SpreadIt.Repository.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using SpreadIt.Repository.Factories;

namespace SpreadIt.Repository.Factories
{
    public class PostReportFactory
    {
        private Factories.ReportCategoryFactory ReportCategoryFactory;
        public PostReportFactory()
        {

        }

        public DTO.PostReport CreatePostReport(PostReport postReport)
        {
            return new DTO.PostReport()
            {
                Id = postReport.Id,
                Message = postReport.Message,
                CreatedDate = postReport.CreatedDate,
                PostId = postReport.PostId,
                ReportCategoryId = postReport.ReportCategoryId,
                IsActive = postReport.IsActive
                //ReportCategory = ReportCategoryFactory.CreateReportCategory(new ReportCategory())
            };
        }


        public PostReport CreatePostReport(DTO.PostReport postReport)
        {
            return new PostReport()
            {
                Id = postReport.Id,
                Message = postReport.Message,
                //CreatedDate = postReport.CreatedDate,
                PostId = postReport.PostId,
                ReportCategoryId = postReport.ReportCategoryId,
                IsActive = postReport.IsActive
            };
        }


        public object CreateDataShapedObject(PostReport postReport, List<string> lstOfFields)
        {

            return CreateDataShapedObject(CreatePostReport(postReport), lstOfFields);
        }


        public object CreateDataShapedObject(DTO.PostReport postReport, List<string> lstOfFields)
        {

            if (!lstOfFields.Any())
            {
                return postReport;
            }
            else
            {
                // create a new ExpandoObject & dynamically create the properties for this object

                ExpandoObject objectToReturn = new ExpandoObject();
                foreach (var field in lstOfFields)
                {
                    // need to include public and instance, b/c specifying a binding flag overwrites the
                    // already-existing binding flags.

                    var fieldValue = postReport.GetType()
                        .GetProperty(field, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance)
                        .GetValue(postReport, null);

                    // add the field to the ExpandoObject
                    ((IDictionary<String, Object>)objectToReturn).Add(field, fieldValue);
                }

                return objectToReturn;
            }
        }

    }
}
