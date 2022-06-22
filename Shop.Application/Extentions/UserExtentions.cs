using Shop.Domain.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Extentions
{
    public static  class UserExtentions 
    {
        public static string GetUserName(this User user)
        {
            return $"{user.FirstName} {user.LastName}";
        }
    }
}
