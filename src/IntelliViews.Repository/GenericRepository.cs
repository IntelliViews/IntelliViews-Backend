
using IntelliViews.Data;
using Microsoft.EntityFrameworkCore;

namespace IntelliViews.Repository
{
    public class GenericRepository<T> : IRepository<T> where T : DbEntity
    {
        private readonly DataContext _dbContext;
        public GenericRepository(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<T> Create(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<T> DeleteById(string id)
        {
            T entity = await GetById(id);
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<List<T>> GetAll()
        {
            List<T> entities = await _dbContext.Set<T>().ToListAsync();
            return entities;
        }

        public async Task<T> GetById(string id)
        {
            T entity = await _dbContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new Exception($"No {typeof(T).Name.ToLower()} with id: {id}");
            return entity;
        }

        public async Task<T> Update(T entity)
        {
            _dbContext.Set<T>().Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return entity;
        }
    }
}
