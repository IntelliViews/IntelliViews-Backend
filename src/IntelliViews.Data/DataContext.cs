using IntelliViews.Data.DataModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Reflection;
using System.Xml.Linq;

namespace IntelliViews.Data
{
    public class DataContext : IdentityUserContext<ApplicationUser>
    {
        //In case of docker:
        //public static bool _migrations;
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            /*if (!_migrations) { 
                this.Database.Migrate();
                _migrations = true;
            }*/
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(message => Debug.WriteLine(message)); //see the sql EF using in the console
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
       // public DbSet<ApplicationUser> Users { get; set; }

    }
}


