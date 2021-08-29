using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherUpdate.Model.User;
using WeatherUpdate.Api.DAL;

namespace WeatherUpdate.Service.Interface
{
    public interface IUserRepository
    {
        Task<bool> FindUserByUserNameAsync(string username);
        
        Task<bool> CreateUserAsync(UserModel userModel);
        

        Task<int> GetUserIdByUsernameAndPasswordAsync(string username, string password);
       

        Task<bool> UpdateTokenAsync(UserModel userModel);
       
    }
}
