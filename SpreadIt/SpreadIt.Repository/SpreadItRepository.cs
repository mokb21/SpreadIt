using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                return _ctx.Posts.FirstOrDefault(e => e.Id == id);
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
                return _ctx.Posts;
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
                    return new RepositoryActionResult<Post>(post, RepositoryActionStatus.Created);
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
        #endregion
    }
}
