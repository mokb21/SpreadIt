using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpreadIt.IdSrv.Config
{
    public static class Resources
    {
        public static IEnumerable<IdentityResource> Get()
        {
            var resources = new List<IdentityResource>
            {
               //identity resources
               new IdentityResources.OpenId(),
               new IdentityResources.Profile()
            };
            return resources;
        }
    }
}
