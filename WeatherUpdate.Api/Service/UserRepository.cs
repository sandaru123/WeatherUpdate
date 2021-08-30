using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherUpdate.Api.DAL;
using WeatherUpdate.Model.User;
using WeatherUpdate.Service.Interface;

namespace WeatherUpdate.Service
{
    public class UserRepository : IUserRepository
    {

        private readonly WeatherDbContext context;

        public UserRepository(WeatherDbContext _context)
        {
            context = _context;

        }

        public async Task<bool> FindUserByUserNameAsync(string username)
        {
            var user = await context.User.FirstOrDefaultAsync(u => u.UserName == username);
            if (user != null)
            {
                return false;
            }

            return true;
        }


        public async Task<bool> CreateUserAsync(UserModel userModel)
        {
            try
            {
                if (userModel != null)
                {
                    User user = new User();
                    user.Password = userModel.Password;
                    user.UserName = userModel.UserName;
                    user.Token = null;

                    await context.User.AddAsync(user);
                    await context.SaveChangesAsync();

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return true;
        }


        public async Task<int> GetUserIdByUsernameAndPasswordAsync(string username, string password)
        {
            try
            {
                User user = await context.User.FirstOrDefaultAsync(c => c.UserName == username && c.Password == password);

                if (user != null)
                {
                    return user.UserId;
                }
                return 0;
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        public async Task<bool> UpdateTokenAsync(UserModel userModel)
        {
            try
            {
                if (userModel.UserId != 0 && string.IsNullOrEmpty(userModel.Token))
                {
                    User userObj = await context.User.FirstOrDefaultAsync(c => c.UserId == userModel.UserId);

                    if (userObj != null)
                    {
                        userObj.Token = userModel.Token;

                        await context.SaveChangesAsync();
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<bool> UpdateTokenByIdAsync(int userId, string token)
        {
            try
            {
                if (userId != 0)
                {
                    var user = await context.User.FirstOrDefaultAsync(c=>c.UserId == userId);
                    if (user != null)
                    {
                        user.Token = token;
                        await context.SaveChangesAsync();
                        return true;
                    }

                }
                return false;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
    }
}
