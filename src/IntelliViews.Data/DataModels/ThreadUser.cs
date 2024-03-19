using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IntelliViews.Data.DataModels
{
    [Table("threads")]
    public class ThreadUser : DbEntity
    {
        [Key]
        public string Id { get; set; }

        [Column("created_at", TypeName = "Date")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [Column("content")]
       

        // For many to one:
        [ForeignKey("userId")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }


        // Navigation property for feedback associated with this thread
        public Feedback Feedback { get; set; }
        

    }
}
