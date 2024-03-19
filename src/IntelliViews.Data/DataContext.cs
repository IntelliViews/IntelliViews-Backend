using IntelliViews.Data.DataModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Reflection.Emit;

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
            builder.Entity<ThreadUser>()
                .Navigation(t => t.User)
                .AutoInclude();

            builder.Entity<ThreadUser>()
                .Navigation(t => t.Feedback)
                .AutoInclude();

            builder.Entity<ApplicationUser>()
                .Navigation(t => t.Feedbacks)
                .AutoInclude();

            // Relationship one-to-many between User and Feedbacks
            builder.Entity<ApplicationUser>()
                .HasMany(u => u.Feedbacks)    
                .WithOne(f => f.User)         
                .HasForeignKey(f => f.UserId);  

            // Relationship one-to-many between User and Threads
            builder.Entity<ThreadUser>()
                .HasOne(t => t.User)
                .WithMany(u => u.Threads)
                .HasForeignKey(t => t.UsereId);


            // Relationship one-to-one between Thread and Feedback
            builder.Entity<ThreadUser>()
               .HasOne(t => t.Feedback)
               .WithOne(f => f.Thread)
               .HasForeignKey<ThreadUser>(t => t.Id);


            // Relationship one-to-one between Feedback and Thread
            builder.Entity<Feedback>()
                .HasOne(f => f.Thread)
                .WithOne(t => t.Feedback);
                

            // Relationship many-to-one between Thread and User
            builder.Entity<Feedback>()
                .HasOne(f => f.User)
                .WithMany()
                .HasForeignKey(f => f.UserId);

            
        }

        public DbSet<ThreadUser> Threads { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }

    }
}


