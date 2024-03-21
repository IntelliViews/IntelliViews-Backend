using IntelliViews.Data;
using IntelliViews.Data.DataModels;
using Microsoft.EntityFrameworkCore;

namespace IntelliViews.Repository
{
    public class ThreadRepository : GenericRepository<ThreadUser>, IRepository<ThreadUser>
    {
        private readonly DataContext _dbContext;
        private DbSet<ThreadUser> _dbSet = null;
        public ThreadRepository(DataContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<ThreadUser>();
        }

        public async Task<ThreadUser> FindByUserIdAndThreadId(string userId, string threadId)
        {
            ThreadUser result = await _dbSet.FirstOrDefaultAsync(x => x.UserId == userId && x.Id == threadId);
            if (result == null)
            {
                throw new Exception($"Not found!");
            }
            return result;
        }

        public async Task<List<Feedback>> FindFeedbacksByIdAndUserId(string feedbackId, string userId)
        {
            List<Feedback> result = await _dbContext.Feedbacks
                .Where(f => f.Id == feedbackId && f.UserId == userId)
                .ToListAsync();

            if (result.Count == 0)
            {
                throw new Exception($"Not found!");
            }
            return result;

        }

        public async Task<List<ThreadUser>> GetAll()
        {
            List<ThreadUser> entities = await _dbSet.ToListAsync();
            if (entities == null || entities.Count == 0)
            {
                throw new Exception("Not Found!");
            }
            else
            {
                return entities;
            }
        }

        public async Task<List<ThreadUser>> GetById(string userId)
        {
            return await _dbContext.Threads.Where(t => t.UserId == userId).ToListAsync();
        }
    }
}

