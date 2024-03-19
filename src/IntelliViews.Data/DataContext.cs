using IntelliViews.Data.DataModels;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Reflection;
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
            base.OnModelCreating(builder);

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

            builder.Entity<ApplicationUser>()
                .Navigation(t => t.Threads)
                .AutoInclude();

            // Relationship one-to - one between Thread and Feedback
            /* builder.Entity<ThreadUser>()
                .HasOne(t => t.Feedback)
                .WithOne(f => f.Thread)
                .HasForeignKey<ThreadUser>(t => t.Id);*/

            /*   // Relationship one-to-many between User and Feedbacks
               builder.Entity<ApplicationUser>()
                   .HasMany(u => u.Feedbacks)    
                   .WithOne(f => f.User)         
                   .HasForeignKey(f => f.UserId);  


               // Relationship one-to-many between User and Threads
               builder.Entity<ThreadUser>()
                   .HasOne(t => t.User)
                   .WithMany(u => u.Threads)
                   .HasForeignKey(t => t.UsereId);

               //*/


            // Testing with seed:
            builder.Entity<ThreadUser>().HasData(
                new ThreadUser() { Id = "1", CreatedAt = DateTime.Now, UserId = "0f058217-784c-4483-ad7d-9d4e01a81933" },
                new ThreadUser() { Id = "2", CreatedAt = DateTime.Now, UserId = "2450d037-6235-498c-b1e7-ceaf8cafb53a" },
                new ThreadUser() { Id = "3", CreatedAt = DateTime.Now, UserId = "cdf987f1-81df-4de6-93eb-6182e8f4031a" }
                );

            builder.Entity<Feedback>().HasData(
               new Feedback() { Id = "1111", Context = "TestFeedback1", CreatedAt = DateTime.Now, ThreadId = "1", UserId = "0f058217-784c-4483-ad7d-9d4e01a81933", Score = 1 },
               new Feedback() { Id = "2222", Context = "TestFeedback2", CreatedAt = DateTime.Now, ThreadId = "2", UserId = "2450d037-6235-498c-b1e7-ceaf8cafb53a", Score = 10 },
               new Feedback() { Id = "3333", Context = "TestFeedback3", CreatedAt = DateTime.Now, ThreadId = "3", UserId = "cdf987f1-81df-4de6-93eb-6182e8f4031a", Score = 9 }
               );

        }

        public DbSet<ThreadUser> Threads { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }

    }
}


