﻿using SpreadIt.Repository.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;

namespace SpreadIt.Repository.Factories
{
    public class CommentReportFactory
    {
        public CommentReportFactory()
        {

        }

        public DTO.CommentReport CreateCommentReport(CommentReport commentReport)
        {
            return new DTO.CommentReport()
            {
                Id = commentReport.Id,
                Message = commentReport.Message,
                CreatedDate = commentReport.CreatedDate,
                CommentId = commentReport.CommentId,
                ReportCategoryId = commentReport.ReportCategoryId,
                IsActive = commentReport.IsActive
            };
        }


        public CommentReport CreateCommentReport(DTO.CommentReport commentReport)
        {
            return new CommentReport()
            {
                Id = commentReport.Id,
                Message = commentReport.Message,
                CreatedDate = commentReport.CreatedDate,
                CommentId = commentReport.CommentId,
                ReportCategoryId = commentReport.ReportCategoryId,
                IsActive = commentReport.IsActive
            };
        }

        public object CreateDataShapedObject(CommentReport commentReport, List<string> lstOfFields)
        {

            return CreateDataShapedObject(CreateCommentReport(commentReport), lstOfFields);
        }


        public object CreateDataShapedObject(DTO.CommentReport commentReport, List<string> lstOfFields)
        {

            if (!lstOfFields.Any())
            {
                return commentReport;
            }
            else
            {
                // create a new ExpandoObject & dynamically create the properties for this object

                ExpandoObject objectToReturn = new ExpandoObject();
                foreach (var field in lstOfFields)
                {
                    // need to include public and instance, b/c specifying a binding flag overwrites the
                    // already-existing binding flags.

                    var fieldValue = commentReport.GetType()
                        .GetProperty(field, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance)
                        .GetValue(commentReport, null);

                    // add the field to the ExpandoObject
                    ((IDictionary<String, Object>)objectToReturn).Add(field, fieldValue);
                }

                return objectToReturn;
            }
        }
    }
}
