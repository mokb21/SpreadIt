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
        IQueryable<Post> GetPosts();
        RepositoryActionResult<Post> InsertPost(Post post);
        RepositoryActionResult<Post> UpdatePost(Post post);
        #endregion

        #region
        List<Comment> GetCommentByPost(int id);
        RepositoryActionResult<Comment> InsertComment(Comment comment);
        RepositoryActionResult<Comment> UpdateComment(Comment comment);
        RepositoryActionResult<Comment> DeleteComment(int id);

        #endregion
    }
}
