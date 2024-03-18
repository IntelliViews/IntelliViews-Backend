namespace IntelliViews.API.DTOs.Threads
{
    public class OutFeedbackDTO
    {
        public string? UserId {  get; set; }
        public string? FeedBackId { get; set; }
        public DateTime? Created { get; set; }
        public string? Context {  get; set; }
        public int? Score { get; set; }
    }
}
