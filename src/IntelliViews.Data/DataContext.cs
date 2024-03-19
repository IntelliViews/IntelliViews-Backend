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

           // Relationship one-to - one between Thread and Feedback
            builder.Entity<ThreadUser>()
               .HasOne(t => t.Feedback)
               .WithOne(f => f.Thread)
               .HasForeignKey<ThreadUser>(t => t.Id);

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
                new ThreadUser() { Id = "1", Context = "Test1", CreatedAt = DateTime.Now, UserId = "466ddc96 - a3e7 - 4025 - 820a - 0fc14c650a4d" },
                new ThreadUser() { Id = "2", Context = "Test2", CreatedAt = DateTime.Now, UserId = "bdad1ff0-cfb4-40fb-b69c-bd64aef0fc0a" },
                new ThreadUser() { Id = "3", Context = "Test3", CreatedAt = DateTime.Now, UserId = "cfced002-b073-4227-a3a7-d1db45d58153" }
                );

            builder.Entity<Feedback>().HasData(
               new Feedback() { Id = "11", Context = "TestFeedback1", CreatedAt = DateTime.Now, ThreadId = "1", UserId = "466ddc96 - a3e7 - 4025 - 820a - 0fc14c650a4d" },
               new Feedback() { Id = "22", Context = "TestFeedback2", CreatedAt = DateTime.Now, ThreadId = "2", UserId = "bdad1ff0-cfb4-40fb-b69c-bd64aef0fc0a" },
               new Feedback() { Id = "22", Context = "TestFeedback3", CreatedAt = DateTime.Now, ThreadId = "3", UserId = "cfced002-b073-4227-a3a7-d1db45d58153" }
               );

        }

        public DbSet<ThreadUser> Threads { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }

    }
}


