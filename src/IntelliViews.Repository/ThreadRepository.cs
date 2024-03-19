using IntelliViews.Data.DataModels;
using IntelliViews.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntelliViews.Repository;

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

            if (result.Count == 0 )
            {
                throw new Exception($"Not found!");
            }
            return result;

        }

        public async Task<List<ThreadUser>> GetAll()
        {
            List<ThreadUser> entities = await _dbSet.ToListAsync();
            if (entities == null || entities.Count == 0) {
                throw new Exception("Not Found!");
            }
            else
            {
                return entities;
            }
        }

        public async Task<List<ThreadUser>> GetById(string userId)
        {
            // Check if any threads exist for the specified userId
            bool userExists = await _dbContext.Threads.AnyAsync(t => t.UserId == userId);
            if (!userExists)
            {
                throw new Exception($"User with ID '{userId}' does not exist or no thread existed!");
            }
            else { return await _dbContext.Threads
            .Where(t => t.UserId == userId)
            .ToListAsync();
            }
        }
    }
}

