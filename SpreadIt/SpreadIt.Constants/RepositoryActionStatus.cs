using System;
using System.Collections.Generic;
using System.Text;

namespace SpreadIt.Constants
{
    public enum RepositoryActionStatus 
    {
        Ok,
        Created,
        Updated,
        NotFound,
        Deleted,
        NothingModified,
        Error
    }
}
