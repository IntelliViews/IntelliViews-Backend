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
    public class AuthenticationRepository<T> : IRepository<T> where T : class, DbEntity
    {
        private readonly UserManager<T> _userManager;
        private readonly DataContext _context;
       

      
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
    }
}
