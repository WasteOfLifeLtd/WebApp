using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using SportsStore.Domain.Entities.IdentityEntities;
using SportsStore.Domain.Entities;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace SportsStore.Domain.Concrete
{
    public class AppIdentityDbContext : IdentityDbContext<AppUser>
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }

        public AppIdentityDbContext() : base("ThisOne") { }

        static AppIdentityDbContext()
        {
            Database.SetInitializer<AppIdentityDbContext>(new IdentityDbInit());
        }

        public static AppIdentityDbContext Create()
        {
            return new AppIdentityDbContext();
        }
    }

    public class IdentityDbInit 
    : NullDatabaseInitializer<AppIdentityDbContext>
    {
        
    }
}