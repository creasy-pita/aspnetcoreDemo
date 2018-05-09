using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUserAPI.Data
{
    public class AppUserDbContext : DbContext
    {
        public DbSet<AppUser> User { get; set; }

        public AppUserDbContext(DbContextOptions<AppUserDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<AppUser>(entity =>
            {
                entity.ToTable("AppUser").HasKey(e => e.Id);
            });
        }


    }

    public class AppUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public string Title { get; set; }
    }
}
