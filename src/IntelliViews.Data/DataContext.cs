using IntelliViews.Data.DataModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Reflection;
using System.Reflection.Emit;
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

            //AUTO INCLUDE: (so you dont have to include when calling --> context.Authors.FirstOrDefault(a => a.Id == authorId); )
            // Instead of context.Authors .Include(a => a.Books) // Explicitly include related books.FirstOrDefault(a => a.Id == authorId);
            /*builder.Entity<ThreadUser>()
                .Navigation(t => t.User)
                .AutoInclude();*/

            base.OnModelCreating(builder);
            builder.Entity<ThreadUser>()
                .HasOne(t => t.User)
                .WithMany(u => u.Threads)
                .HasForeignKey(t => t.UsereId);

            // Relationship one-to-many between Thread and Feedback
            builder.Entity<Feedback>()
                .HasOne(f => f.Thread)
                .WithMany(t => t.Feedbacks)
                .HasForeignKey(f => f.ThreadId);

           /* // Relationship one-to-many between user and Feedback (DONT NEED)
           builder.Entity<Feedback>()
                .HasOne(f => f.User)
                .WithMany()
                .HasForeignKey(f => f.UserId);*/
        }
       
        public DbSet<ThreadUser> Threads{ get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }

    }
}


