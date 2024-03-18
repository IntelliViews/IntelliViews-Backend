using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IntelliViews.Data.DataModels
{
    [Table("threads")]
    public class ThreadUser : DbEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [Column("created_at", TypeName = "Date")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // For many to one:
        [ForeignKey("user_id")]
        [Column("user_id")]
        public string UsereId { get; set; }
        public ApplicationUser User { get; set; }

        // Navigation property for feedbacks associated with this thread
        public ICollection<Feedback> Feedbacks { get; set; }
    }
}
