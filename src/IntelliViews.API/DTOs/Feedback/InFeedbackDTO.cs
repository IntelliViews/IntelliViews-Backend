namespace IntelliViews.API.DTOs.Feedback
{
    public class InFeedbackDTO
    {
        public required string UserId { get; set; }
        public required string FeedBackId { get; set; }
        public DateTime? Created { get; set; } = DateTime.Now;
        public string? Context { get; set; }
        public int? Score { get; set; }
    }
}
