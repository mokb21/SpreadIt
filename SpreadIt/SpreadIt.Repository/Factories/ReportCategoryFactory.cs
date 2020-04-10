using SpreadIt.Repository.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;

namespace SpreadIt.Repository.Factories
{
    public class ReportCategoryFactory
    {
        public ReportCategoryFactory()
        {

        }

        public DTO.ReportCategory CreateReportCategory(ReportCategory reportCategory)
        {
            return new DTO.ReportCategory()
            {
                Id = reportCategory.Id,
                Name = reportCategory.Name
            };
        }



        public ReportCategory CreateReportCategory(DTO.ReportCategory reportCategory)
        {
            return new ReportCategory()
            {
                Id = reportCategory.Id,
                Name = reportCategory.Name
            };

        }
    }
}
