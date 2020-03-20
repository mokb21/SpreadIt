using IdentityModel;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SpreadIt.IdSrv.Config
{
    //TODO: Get From DB
    public static class Users
    {
        public static List<TestUser> Get()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    Username = "mokb",
                    Password = "secret",
                    SubjectId = "1",
                    Claims = new[]{
                        new Claim(JwtClaimTypes.GivenName, "mokb"),
                        new Claim(JwtClaimTypes.Email, "mokb@mokb.com"),
                    }
                },
                new TestUser
                {
                    Username = "deliz",
                    Password = "secret",
                    SubjectId = "2",
                    Claims = new[]{
                        new Claim(JwtClaimTypes.GivenName, "deliz"),
                        new Claim(JwtClaimTypes.Email, "dliz@dliz.com"),
                    }
                }
            };
        }
    }
}
