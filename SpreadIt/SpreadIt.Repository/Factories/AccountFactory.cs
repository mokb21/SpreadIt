using SpreadIt.Repository.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SpreadIt.Repository.Factories
{
    public class AccountFactory
    {
        public DTO.Account CreateAccount(ApplicationUser account)
        {
            return new DTO.Account()
            {
                UserName = account.UserName,
                Email = account.Email,
            };
        }


        public ApplicationUser CreateAccount(DTO.Account account)
        {
            return new ApplicationUser()
            {
                 UserName = account.UserName,
                 Email = account.Email,
                 EmailConfirmed = true
            };
        }

        public object CreateDataShapedObject(ApplicationUser account, List<string> lstOfFields)
        {

            return CreateDataShapedObject(CreateAccount(account), lstOfFields);
        }


        public object CreateDataShapedObject(DTO.Account account, List<string> lstOfFields)
        {

            if (!lstOfFields.Any())
            {
                return account;
            }
            else
            {
                // create a new ExpandoObject & dynamically create the properties for this object

                ExpandoObject objectToReturn = new ExpandoObject();
                foreach (var field in lstOfFields)
                {
                    // need to include public and instance, b/c specifying a binding flag overwrites the
                    // already-existing binding flags.

                    var fieldValue = account.GetType()
                        .GetProperty(field, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance)
                        .GetValue(account, null);

                    // add the field to the ExpandoObject
                    ((IDictionary<String, Object>)objectToReturn).Add(field, fieldValue);
                }

                return objectToReturn;
            }
        }
    }
}
