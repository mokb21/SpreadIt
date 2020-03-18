using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpreadIt.Repository.Models
{
    public partial class SpreadItContext : DbContext
    {
        public SpreadItContext()
        {

        }

        public DbSet<MessageLog> MessageLogs { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<PostImage> PostImages { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostRate> PostRates { get; set; }
        public DbSet<PostReport> PostReports { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<CommentRate> CommentRates { get; set; }
        public DbSet<CommentReport> CommentReports { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(SpreadIt.Constants.SpreadItConstants.ConnectionString);
        }

    }
}
