using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherUpdate.Api.Model.User.Login;
using WeatherUpdate.Service.Interface;

namespace WeatherUpdate.Api.Helper
{
    public class Authentication
    {
        private IConfiguration _config;

        private readonly IUserRepository userRepository;
        public Authentication(IConfiguration config, IUserRepository _userRepository)
        {
            _config = config;
            userRepository = _userRepository;
        }

        #region GenerateJWT
        /// <summary>
        /// Generate Json Web Token Method
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        private string GenerateJSONWebToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        #endregion

        #region AuthenticateUser

        private async Task<int> AuthenticateUser(LoginModel login)
        {
            //Validate the User Credentials    
            //Demo Purpose, I have Passed HardCoded User Information    
            var id = await userRepository.GetUserIdByUsernameAndPasswordAsync(login.UserName, login.Password);

            if (id != 0)
            {
                return id;
            }

            return 0;
        }
        #endregion
    }
}
