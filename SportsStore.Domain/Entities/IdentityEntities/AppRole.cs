using Microsoft.AspNet.Identity.EntityFramework;

namespace SportsStore.Domain.Entities.IdentityEntities
{
    public class AppRole : IdentityRole
    {
        public AppRole() : base ()
        {

        }

        public AppRole(string name) : base(name)
        {

        }
    }
}
