using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectHotelWeb.Models
{
    public class UserDbContext : IdentityDbContext<User>
    {

        public UserDbContext ()
            : base("IdentityConnection")
        {

        }
    }
}