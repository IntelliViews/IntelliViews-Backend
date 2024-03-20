namespace IntelliViews.Data
{
    public interface DbEntity
    {
        public string Id { get; set; } 
        public DateTime CreatedAt { get; set; } 
    }
}
