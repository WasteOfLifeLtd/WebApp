using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SportsStore.Domain.Entities.IdentityEntities;

namespace SportsStore.WebUI.Areas.Identity.Models
{
    public class UserAdminViewModel
    {
        public IQueryable<AppUser> Users { get; set; }
        public string Role { get; set; }
    }
}