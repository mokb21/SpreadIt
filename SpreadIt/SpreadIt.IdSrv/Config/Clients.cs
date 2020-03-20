using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpreadIt.IdSrv.Config
{
    public static class Clients
    {
        public static IEnumerable<Client> Get()
        {
            return new[]
            {
                new Client {
                    Enabled = true,
                    ClientName="SpreadIt API Client (Hybrid flow)",
                    ClientId = "api",
                    AllowedGrantTypes = new List<string>{ GrantType.Hybrid },
                    RequireConsent = true
                }
            };
        }
    }
}