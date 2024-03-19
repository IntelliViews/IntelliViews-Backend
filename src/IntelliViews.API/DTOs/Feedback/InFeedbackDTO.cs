namespace IntelliViews.API.DTOs.Feedback
{
    public class InFeedbackDTO
    {
        public required string UserId { get; set; }
        public required string ThreadId { get; set; }
        
        public string? Context { get; set; }
        public int? Score { get; set; }
    }
}

