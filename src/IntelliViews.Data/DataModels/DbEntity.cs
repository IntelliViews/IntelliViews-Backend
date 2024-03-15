namespace IntelliViews.Data.DataModels
{
    public abstract class DbEntity
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
