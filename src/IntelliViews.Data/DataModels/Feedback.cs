using IntelliViews.Data.Enums;
using IntelliViews.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliViews.Data.DataModels
{
    [Table("feedbacks")]
    public class Feedback : DbEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [Column("created_at", TypeName = "Date")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Foreign key to relate feedback to user via thread
        [ForeignKey("thread.user_id")]
        [Column("user_id")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        // Foreign key to relate feedback to thread
        [ForeignKey("thread_id")]
        [Column("thread_id")]
        public string ThreadId { get; set; }
        public ThreadUser Thread { get; set; }

    }
}

