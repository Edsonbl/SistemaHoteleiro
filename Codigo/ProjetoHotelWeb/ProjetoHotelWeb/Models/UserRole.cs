using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectHotelWeb.Models
{
    public class UserRole : IdentityRole
    {

        public string Description { get; set; }

        public UserRole()
        {

        }

        public UserRole(string roleName, string description) : base(roleName)
        {
            this.Description = description;
        }
    }
}