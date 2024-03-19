
using IntelliViews.Data;
using IntelliViews.Data.DataModels;
using Microsoft.EntityFrameworkCore;

namespace IntelliViews.Repository
{
    public class GenericRepository<T> : IRepository<T> where T : class, DbEntity
    {
        private readonly DataContext _dbContext;
        private DbSet<T> _dbSet = null;
        public GenericRepository(DataContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public async Task<T> Create(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<T> DeleteById(string id)
        {
            T entity = await GetById(id);
            _dbSet.Remove(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<List<T>> GetAll()
        {
            List<T> entities = await _dbSet.ToListAsync();
            return entities;
        }

        public async Task<T> GetById(string id)
        {
            T entity = await _dbSet.FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new Exception($"No {typeof(T).Name.ToLower()} with id: {id}");
            return entity;
        }

        // Have not taken an account for empty or bad request..
        public async Task<T> Update(T entity, string id)
        {
            T source = await GetById(id);
            _dbContext.Attach(source);
            _dbContext.Entry(source).CurrentValues.SetValues(entity);
            //_dbSet.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return entity;
        }


       
    }
}
