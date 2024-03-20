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
       
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            Database.Migrate();
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

        }

        public DbSet<ThreadUser> Threads { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }

    }
}


