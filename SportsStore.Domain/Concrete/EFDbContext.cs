using SportsStore.Domain.Entities;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SportsStore.Domain.Concrete
{
    public class EFDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
    }

    }
