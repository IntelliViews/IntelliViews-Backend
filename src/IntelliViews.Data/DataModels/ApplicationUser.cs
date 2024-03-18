using IntelliViews.Data.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliViews.Data.DataModels
{
    [Table("application-users")]
    public class ApplicationUser : IdentityUser, DbEntity
    {
        [Required]
        [Column("role")]   
        public Roles Role { get; set; }

        [Column("created_at", TypeName = "Date")]
        public DateTime CreatedAt {  get; set; } = DateTime.Now;

        public string? Password { get; set; }

        // Navigation property for threads owned by this user
        public ICollection<ThreadUser>? Threads { get; set; } = new List<ThreadUser>();

        // Navigation property for feedbacks created by this user
        public ICollection<Feedback> Feedbacks { get; set; }
    }
}
