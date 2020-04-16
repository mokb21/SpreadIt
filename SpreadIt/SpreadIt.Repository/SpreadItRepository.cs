using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SpreadIt.Constants;
using SpreadIt.Repository.Models;

namespace SpreadIt.Repository
{
    public class SpreadItRepository : ISpreadItRepository
    {

        SpreadItContext _ctx;

        public SpreadItRepository(SpreadItContext context)
        {
            _ctx = context;
        }

        #region MessageLog
        public void InsertMessageLog(MessageLog messageLog)
        {
            try
            {
                _ctx.MessageLogs.Add(messageLog);
                _ctx.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Location
        public RepositoryActionResult<Location> DeleteLocation(int id)
        {
            try
            {
                var location = _ctx.Locations.FirstOrDefault(e => e.Id == id);
                if (location != null)
                {
                    _ctx.Locations.Remove(location);
                    _ctx.SaveChanges();
                    return new RepositoryActionResult<Location>(null, RepositoryActionStatus.Deleted);
                }
                return new RepositoryActionResult<Location>(null, RepositoryActionStatus.NotFound);
            }
            catch (Exception ex)
            {
                InsertMessageLog(new MessageLog { Project = (byte)ProjectType.Reporsitory, Method = "DeleteLocation", Message = ex.Message });
                return new RepositoryActionResult<Location>(null, RepositoryActionStatus.Error, ex);
            }
        }

        public RepositoryActionResult<Location> InsertLocation(Location location)
        {
            try
            {
                _ctx.Locations.Add(location);
                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<Location>(location, RepositoryActionStatus.Created);
                }
                else
                {
                    return new RepositoryActionResult<Location>(location, RepositoryActionStatus.NothingModified, null);
                }

            }
            catch (Exception ex)
            {
                InsertMessageLog(new MessageLog { Project = (byte)ProjectType.Reporsitory, Method = "InsertLocation", Message = ex.Message });
                return new RepositoryActionResult<Location>(null, RepositoryActionStatus.Error, ex);
            }
        }

        public Location GetLocation(int id)
        {
            try
            {
                return _ctx.Locations.FirstOrDefault(e => e.Id == id);
            }
            catch (Exception ex)
            {
                InsertMessageLog(new MessageLog { Project = (byte)ProjectType.Reporsitory, Method = "GetLocation", Message = ex.Message });
                return null;
            }
        }

        public IQueryable<Location> GetLocations()
        {
            try
            {
                return _ctx.Locations;
            }
            catch (Exception ex)
            {
                InsertMessageLog(new MessageLog { Project = (byte)ProjectType.Reporsitory, Method = "GetLocations", Message = ex.Message });
                return null;
            }
        }
        #endregion

        #region Post
        public Post GetPost(int id)
        {
            try
            {
                return _ctx.Posts.Include(post => post.PostImages).Include(post => post.Category).FirstOrDefault(e => e.Id == id);
            }
            catch (Exception ex)
            {
                InsertMessageLog(new MessageLog { Project = (byte)ProjectType.Reporsitory, Method = "GetPost", Message = ex.Message });
                return null;
            }
        }

        public IQueryable<Post> GetPosts()
        {
            try
            {
                return _ctx.Posts.Include(a => a.PostImages).Include(a => a.Category)
                    .Where(a => !a.IsBlocked && !a.IsDeleted);
            }
            catch (Exception ex)
            {
                InsertMessageLog(new MessageLog { Project = (byte)ProjectType.Reporsitory, Method = "GetPosts", Message = ex.Message });
                return null;
            }
        }

        public RepositoryActionResult<Post> InsertPost(Post post)
        {
            try
            {
                _ctx.Posts.Add(post);
                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    var AddedPost = _ctx.Posts.Include(post => post.Category).FirstOrDefault(element => element.Id.Equals(post.Id));
                    return new RepositoryActionResult<Post>(AddedPost, RepositoryActionStatus.Created);
                }
                else
                {
                    return new RepositoryActionResult<Post>(post, RepositoryActionStatus.NothingModified, null);
                }

            }
            catch (Exception ex)
            {
                InsertMessageLog(new MessageLog { Project = (byte)ProjectType.Reporsitory, Method = "InsertPost", Message = ex.Message });
                return new RepositoryActionResult<Post>(null, RepositoryActionStatus.Error, ex);
            }
        }

        public RepositoryActionResult<Post> UpdatePost(Post post)
        {
            try
            {
                _ctx.Posts.Update(post);
                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<Post>(post, RepositoryActionStatus.Updated);
                }
                else
                {
                    return new RepositoryActionResult<Post>(post, RepositoryActionStatus.NothingModified, null);
                }

            }
            catch (Exception ex)
            {
                InsertMessageLog(new MessageLog { Project = (byte)ProjectType.Reporsitory, Method = "UpdatePost", Message = ex.Message });
                return new RepositoryActionResult<Post>(null, RepositoryActionStatus.Error, ex);
            }
        }

        public RepositoryActionResult<Post> DeletePost(int id)
        {
            try
            {
                var comment = _ctx.Posts.FirstOrDefault(a => a.Id == id && !a.IsDeleted);
                if (comment != null)
                {
                    comment.IsDeleted = true;
                    _ctx.SaveChanges();
                    return new RepositoryActionResult<Post>(null, RepositoryActionStatus.Deleted);
                }
                return new RepositoryActionResult<Post>(null, RepositoryActionStatus.NotFound);
            }
            catch (Exception ex)
            {
                InsertMessageLog(new MessageLog { Project = (byte)ProjectType.Reporsitory, Method = "DeletePost", Message = ex.Message });
                return new RepositoryActionResult<Post>(null, RepositoryActionStatus.Error, ex);
            }
        }
        #endregion

        #region Comment
        public List<Comment> GetCommentByPost(int PostId)
        {
            try
            {
                return _ctx.Comments.Where(a => a.PostId == PostId && !a.IsBlocked && !a.IsDeleted).ToList();
            }
            catch (Exception ex)
            {
                InsertMessageLog(new MessageLog { Project = (byte)ProjectType.Reporsitory, Method = "GetCommentByPost", Message = ex.Message });
                return null;
            }
        }

        public RepositoryActionResult<Comment> InsertComment(Comment comment)
        {
            try
            {
                _ctx.Comments.Add(comment);
                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<Comment>(comment, RepositoryActionStatus.Created);
                }
                else
                {
                    return new RepositoryActionResult<Comment>(comment, RepositoryActionStatus.NothingModified, null);
                }

            }
            catch (Exception ex)
            {
                InsertMessageLog(new MessageLog { Project = (byte)ProjectType.Reporsitory, Method = "InsertComment", Message = ex.Message });
                return new RepositoryActionResult<Comment>(null, RepositoryActionStatus.Error, ex);
            }
        }

        public RepositoryActionResult<Comment> UpdateComment(Comment comment)
        {
            try
            {
                _ctx.Comments.Update(comment);
                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<Comment>(comment, RepositoryActionStatus.Updated);
                }
                else
                {
                    return new RepositoryActionResult<Comment>(comment, RepositoryActionStatus.NothingModified, null);
                }

            }
            catch (Exception ex)
            {
                InsertMessageLog(new MessageLog { Project = (byte)ProjectType.Reporsitory, Method = "InsertComment", Message = ex.Message });
                return new RepositoryActionResult<Comment>(null, RepositoryActionStatus.Error, ex);
            }
        }

        public RepositoryActionResult<Comment> DeleteComment(int id)
        {
            try
            {
                var comment = _ctx.Comments.FirstOrDefault(a => a.Id == id && !a.IsDeleted);
                if (comment != null)
                {
                    comment.IsDeleted = true;
                    _ctx.SaveChanges();
                    return new RepositoryActionResult<Comment>(null, RepositoryActionStatus.Deleted);
                }
                return new RepositoryActionResult<Comment>(null, RepositoryActionStatus.NotFound);
            }
            catch (Exception ex)
            {
                InsertMessageLog(new MessageLog { Project = (byte)ProjectType.Reporsitory, Method = "DeleteComment", Message = ex.Message });
                return new RepositoryActionResult<Comment>(null, RepositoryActionStatus.Error, ex);
            }
        }
        #endregion

        #region Categories
        public IQueryable<Category> GetCategories()
        {
            try
            {
                return _ctx.Categories;
            }
            catch (Exception ex)
            {
                InsertMessageLog(new MessageLog { Project = (byte)ProjectType.Reporsitory, Method = "GetCategories", Message = ex.Message });
                return null;
            }
        }

        public RepositoryActionResult<Category> InsertCategory(Category category)
        {
            try
            {
                _ctx.Categories.Add(category);
                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<Category>(category, RepositoryActionStatus.Created);
                }
                else
                {
                    return new RepositoryActionResult<Category>(category, RepositoryActionStatus.NothingModified, null);
                }

            }
            catch (Exception ex)
            {
                InsertMessageLog(new MessageLog { Project = (byte)ProjectType.Reporsitory, Method = "InsertCategory", Message = ex.Message });
                return new RepositoryActionResult<Category>(null, RepositoryActionStatus.Error, ex);
            }
        }

        public RepositoryActionResult<Category> UpdateCategory(Category category)
        {
            try
            {
                _ctx.Categories.Update(category);
                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<Category>(category, RepositoryActionStatus.Updated);
                }
                else
                {
                    return new RepositoryActionResult<Category>(category, RepositoryActionStatus.NothingModified, null);
                }

            }
            catch (Exception ex)
            {
                InsertMessageLog(new MessageLog { Project = (byte)ProjectType.Reporsitory, Method = "UpdateCategory", Message = ex.Message });
                return new RepositoryActionResult<Category>(null, RepositoryActionStatus.Error, ex);
            }
        }

        public RepositoryActionResult<Category> DeleteCategory(int id)
        {
            try
            {
                var category = _ctx.Categories.Find(id);
                if (category != null)
                {
                    _ctx.Categories.Remove(category);
                    _ctx.SaveChanges();
                    return new RepositoryActionResult<Category>(null, RepositoryActionStatus.Deleted);
                }
                return new RepositoryActionResult<Category>(null, RepositoryActionStatus.NotFound);
            }
            catch (Exception ex)
            {
                InsertMessageLog(new MessageLog { Project = (byte)ProjectType.Reporsitory, Method = "DeleteCategory", Message = ex.Message });
                return new RepositoryActionResult<Category>(null, RepositoryActionStatus.Error, ex);
            }
        }
        #endregion Categories

        #region Post Reprots
        public IQueryable<PostReport> GetPostReports()
        {
            try
            {
                return _ctx.PostReports.Where(element => element.IsActive.Equals(true));
            }
            catch (Exception ex)
            {
                InsertMessageLog(new MessageLog { Project = (byte)ProjectType.Reporsitory, Method = "GetPostReports", Message = ex.Message });
                return null;
            }
        }

        public IQueryable<PostReport> GetPostReportsByPostId(int PostId)
        {
            try
            {
                return _ctx.PostReports.Where(element => element.PostId.Equals(PostId));
            }
            catch (Exception ex)
            {
                InsertMessageLog(new MessageLog { Project = (byte)ProjectType.Reporsitory, Method = "GetPostReportsByPostId", Message = ex.Message });
                return null;
            }
        }
        public RepositoryActionResult<PostReport> InsertPostReport(PostReport postReport)
        {
            try
            {
                _ctx.PostReports.Add(postReport);
                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<PostReport>(postReport, RepositoryActionStatus.Created);
                }
                else
                {
                    return new RepositoryActionResult<PostReport>(postReport, RepositoryActionStatus.NothingModified, null);
                }

            }
            catch (Exception ex)
            {
                InsertMessageLog(new MessageLog { Project = (byte)ProjectType.Reporsitory, Method = "InsertPostReport", Message = ex.Message });
                return new RepositoryActionResult<PostReport>(null, RepositoryActionStatus.Error, ex);
            }
        }
        public RepositoryActionResult<PostReport> ChangePostReportStatus(int id)
        {
            try
            {
                var postReport = _ctx.PostReports.FirstOrDefault(element => element.Id.Equals(id));
                if (postReport != null)
                {
                    postReport.IsActive = !postReport.IsActive;
                    _ctx.SaveChanges();
                    return new RepositoryActionResult<PostReport>(postReport, RepositoryActionStatus.Updated);
                }
                return new RepositoryActionResult<PostReport>(postReport, RepositoryActionStatus.NotFound);
            }
            catch (Exception ex)
            {
                InsertMessageLog(new MessageLog { Project = (byte)ProjectType.Reporsitory, Method = "ChangePostReportStatus", Message = ex.Message });
                return new RepositoryActionResult<PostReport>(null, RepositoryActionStatus.Error, ex);
            }
        }
        #endregion Post Reprots

        #region Comment Reports
        public IQueryable<CommentReport> GetCommentReports()
        {
            try
            {
                return _ctx.CommentReports;
            }
            catch (Exception ex)
            {
                InsertMessageLog(new MessageLog { Project = (byte)ProjectType.Reporsitory, Method = "GetCommentReports", Message = ex.Message });
                return null;
            }
        }
        public IQueryable<CommentReport> GetCommentReportsByCommentId(int CommentId)
        {
            try
            {
                return _ctx.CommentReports.Where(e => e.CommentId.Equals(CommentId)
                    && e.IsActive);
            }
            catch (Exception ex)
            {
                InsertMessageLog(new MessageLog { Project = (byte)ProjectType.Reporsitory, Method = "GetCommentReportsByCommentId", Message = ex.Message });
                return null;
            }
        }
        public RepositoryActionResult<CommentReport> InsertCommentReport(CommentReport commentReport)
        {
            try
            {
                _ctx.CommentReports.Add(commentReport);
                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<CommentReport>(commentReport, RepositoryActionStatus.Created);
                }
                else
                {
                    return new RepositoryActionResult<CommentReport>(commentReport, RepositoryActionStatus.NothingModified, null);
                }

            }
            catch (Exception ex)
            {
                InsertMessageLog(new MessageLog { Project = (byte)ProjectType.Reporsitory, Method = "InsertCommentReport", Message = ex.Message });
                return new RepositoryActionResult<CommentReport>(null, RepositoryActionStatus.Error, ex);
            }
        }
        public RepositoryActionResult<CommentReport> ChangeCommentReportStatus(int id)
        {
            try
            {
                _ctx.CommentReports.Where(element => element.Id.Equals(id))
                    .Select(element => element.IsActive == !element.IsActive);
                var result = _ctx.SaveChanges();
                var updatedCommentReprot = _ctx.CommentReports.FirstOrDefault(element => element.Id.Equals(id));
                if (result > 0)
                {
                    return new RepositoryActionResult<CommentReport>(updatedCommentReprot, RepositoryActionStatus.Updated);
                }
                else
                {
                    return new RepositoryActionResult<CommentReport>(updatedCommentReprot, RepositoryActionStatus.NothingModified, null);
                }
            }
            catch (Exception ex)
            {
                InsertMessageLog(new MessageLog { Project = (byte)ProjectType.Reporsitory, Method = "ChangeCommentReportStatus", Message = ex.Message });
                return new RepositoryActionResult<CommentReport>(null, RepositoryActionStatus.Error, ex);
            }
        }
        #endregion

        #region Reports Categories
        public IQueryable<ReportCategory> GetReportCategories()
        {
            try
            {
                return _ctx.ReportCategories;
            }
            catch (Exception ex)
            {
                InsertMessageLog(new MessageLog { Project = (byte)ProjectType.Reporsitory, Method = "GetReportCategories", Message = ex.Message });
                return null;
            }
        }
        public ReportCategory GetReportCategory(int id)
        {
            try
            {
                return _ctx.ReportCategories.FirstOrDefault(element => element.Id.Equals(id));
            }
            catch (Exception ex)
            {
                InsertMessageLog(new MessageLog { Project = (byte)ProjectType.Reporsitory, Method = "GetReportCategoryById", Message = ex.Message });
                return null;
            }
        }
        #endregion Reports Categories

        #region Posts Images
        public RepositoryActionResult<PostImage> InsertImage(PostImage postImage)
        {
            try
            {
                _ctx.PostImages.Add(postImage);
                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new RepositoryActionResult<PostImage>(postImage, RepositoryActionStatus.Created);
                }
                else
                {
                    return new RepositoryActionResult<PostImage>(postImage, RepositoryActionStatus.NothingModified, null);
                }

            }
            catch (Exception ex)
            {
                InsertMessageLog(new MessageLog { Project = (byte)ProjectType.Reporsitory, Method = "InsertImage", Message = ex.Message });
                return new RepositoryActionResult<PostImage>(null, RepositoryActionStatus.Error, ex);
            }
        }

        public List<PostImage> GetImagesByPost(int? PostId)
        {
            try
            {
                return _ctx.PostImages.Where(a => a.PostId.Equals(PostId)).ToList();
            }
            catch (Exception ex)
            {
                InsertMessageLog(new MessageLog { Project = (byte)ProjectType.Reporsitory, Method = "GetImagesByPost", Message = ex.Message });
                return null;
            }
        }
        #endregion
    }
}
