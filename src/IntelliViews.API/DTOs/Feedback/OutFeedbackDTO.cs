namespace IntelliViews.API.DTOs.Feedback
{
    public class OutFeedbackDTO
    {
        public string? Id { get; set; }
        public string? UserId { get; set; }
        public string? ThreadId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? Context { get; set; }
        public int? Score { get; set; }
    }
}
