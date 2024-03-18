using IntelliViews.Data;
using IntelliViews.Data.DataModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliViews.Repository
{
    public class AuthenticationRepository<T> : IRepository<T> where T : ApplicationUser
    {
        
        private readonly DataContext _context;
        private DataContext _db;
        private DbSet<T> _dbSet = null;
        public AuthenticationRepository(DataContext db)
        {
            _db = db;
            _dbSet = _db.Set<T>();
        }


        public Task<T> Create(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<T> DeleteById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<T> GetById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<T> Update(T entity)
        {
            throw new NotImplementedException();
        }

        public async Task<T> CreateUser(T entity, UserManager<ApplicationUser> userManager)
        {
            throw new NotImplementedException();
        }
    }
}
