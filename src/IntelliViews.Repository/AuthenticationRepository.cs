using IntelliViews.Data.DataModels;
using IntelliViews.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace IntelliViews.Repository
{
    public class AuthenticationRepository
    {
        
        private DataContext _db;
        public AuthenticationRepository(DataContext db)
        {
            _db = db;
        }

        public async Task<ApplicationUser> CreateUser(ApplicationUser user, UserManager<ApplicationUser> _userManager)
        {
            var result = await _userManager.CreateAsync(
                   user, user.Password!
             );

            if (result.Errors.Any())
            {
                throw new Exception(result.ToString());
            }
            else
            {
                user.Password = "";
                await _db.SaveChangesAsync();
                return user;
            }

        }

        public async Task LoginUser()
        {
            /*var userInDb = _db.Users.FirstOrDefault(u => u.Email == user.Email);

            if (userInDb is null)
            {
                return Unauthorized();
            }*/

            await _db.SaveChangesAsync();
            
        }
    }
}
