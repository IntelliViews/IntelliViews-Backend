using Microsoft.EntityFrameworkCore;

namespace IntelliViews.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
    }
}
