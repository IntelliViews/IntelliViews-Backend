namespace IntelliViews.API.DTOs.Threads
{
    public class InThreadDTO
    {
        public required string UserId { get; set; }
        public required string ThreadId { get; set; }
        public DateTime? Created { get; set; } = DateTime.Now;
        public string? Context { get; set; }
    }
}
