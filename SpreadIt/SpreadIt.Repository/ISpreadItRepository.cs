using SpreadIt.Constants;
using SpreadIt.Repository.Models;
using System.Collections.Generic;
using System.Linq;

namespace SpreadIt.Repository
{
    public interface ISpreadItRepository
    {
        #region MessageLog
        void InsertMessageLog(MessageLog messageLog);
        #endregion

        #region Location
        RepositoryActionResult<Location> DeleteLocation(int id);
        RepositoryActionResult<Location> InsertLocation(Location location);
        Location GetLocation(int id);
        IQueryable<Location> GetLocations();
        #endregion

        #region Posts
        Post GetPost(int id);
        IQueryable<Post> GetPosts(string userId);
        RepositoryActionResult<Post> InsertPost(Post post);
        RepositoryActionResult<Post> UpdatePost(Post post);
        RepositoryActionResult<Post> DeletePost(int id);
        #endregion

        #region Comments
        List<Comment> GetCommentByPost(int id);
        RepositoryActionResult<Comment> InsertComment(Comment comment);
        RepositoryActionResult<Comment> UpdateComment(Comment comment);
        RepositoryActionResult<Comment> DeleteComment(int id);
        #endregion Comments

        #region Categories
        IQueryable<Category> GetCategories();
        RepositoryActionResult<Category> InsertCategory(Category category);
        RepositoryActionResult<Category> UpdateCategory(Category category);
        RepositoryActionResult<Category> DeleteCategory(int id);
        #endregion Categories

        #region Post Reports
        IQueryable<PostReport> GetPostReports();
        PostReport GetPostReport(int id);
        RepositoryActionResult<PostReport> InsertPostReport(PostReport postReport);
        RepositoryActionResult<PostReport> ChangePostReportStatus(int id);
        #endregion Post Reports

        #region Comment Reports
        public IQueryable<CommentReport> GetCommentReports();
        public CommentReport GetCommentReport(int id);
        public RepositoryActionResult<CommentReport> InsertCommentReport(CommentReport commentReport);
        public RepositoryActionResult<CommentReport> ChangeCommentReportStatus(int id);
        #endregion

        #region Reports Categories
        IQueryable<ReportCategory> GetReportCategories();
        ReportCategory GetReportCategory(int id);
        #endregion Reports Categories

        #region Posts Images
        RepositoryActionResult<PostImage> InsertImage(PostImage postImage);
        List<PostImage> GetImagesByPost(int? PostId);
        #endregion

        #region UserLocation
        RepositoryActionStatus InsertUserLocation(List<UserLocation> userLocations);
        RepositoryActionStatus UpdateUserLocation(List<UserLocation> userLocations, string userId);
        List<UserLocation> GetUserLocations(string userId);

        #endregion

        #region Post Rating
        RepositoryActionResult<PostRate> InsertPostRate(PostRate postRate);
        #endregion

        #region Comment Rating
        RepositoryActionResult<CommentRate> InsertCommentRate(CommentRate commentRate);
        #endregion
    }
}
