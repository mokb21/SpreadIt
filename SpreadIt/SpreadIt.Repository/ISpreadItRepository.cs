using SpreadIt.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    }
}
